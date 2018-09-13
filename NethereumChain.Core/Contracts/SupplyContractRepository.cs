using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Web3;
using NethereumChain.Core.Logging;
using NethereumChain.Core.Models;

namespace NethereumChain.Core.Contracts
{
    public class SupplyContractRepository : ISupplyContractRepository
    {
        private readonly Contract _contract;
        private readonly Web3 _web3;
        private readonly INethereumLogger _nethereumLogger;

        public SupplyContractRepository(INethereumLogger nethereumLogger)
        {
            _nethereumLogger = nethereumLogger;

            var address = AppConfigProvider.ContractAddress;
            _web3 = new Web3(AppConfigProvider.InfuraApiAddress);
            _contract = new BaseContract(address, _web3).Contract;
        }

        public SupplyContractRepository(string address, Web3 web3, INethereumLogger nethereumLogger)
        {
            _web3 = web3;
            _contract = new BaseContract(address, web3).Contract;
            _nethereumLogger = nethereumLogger;
        }

        public async Task<int> GetChainCount() 
            => await _contract.GetFunction("GetChainCount").CallAsync<int>();

        public async Task<Location> GetLocation(string locationName)
        {
            try
            {
                var getLocationFunction = _contract.GetFunction("GetLocation");
                var location = await getLocationFunction
                    .CallDeserializingToObjectAsync<Location>(locationName)
                    .ConfigureAwait(false);
                
                return location;
            }
            catch (Exception e)
            {
                _nethereumLogger.Error(e.Message);
                return null;
            }
        }

        public async Task<List<Location>> GetAllLocations()
        {
            var locations = new List<Location>();
            var getLocationFunction = _contract.GetFunction("GetLocation");

            for (var i = 0; i <= GetChainCount().Result; i++)
            {
                try
                {
                    var locationName = await _contract.GetFunction("ChainDictionary")
                        .CallAsync<string>(i);

                    var location = await getLocationFunction
                        .CallDeserializingToObjectAsync<Location>(locationName)
                        .ConfigureAwait(false);
                    locations.Add(location);                    
                }
                catch (Exception e)
                {
                    _nethereumLogger.Error(e.Message);
                    return null;
                }
            }

            return locations;
        }

        public async Task<string> AddNewLocation(string userAddress, string privateKey, int gasLimit, int amount, Location location)
        {
            try
            {
                var addLocationFunction = _contract.GetFunction("AddNewLocation");
                var data = addLocationFunction.GetData(location.LocationName);

                var txCount = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(userAddress).ConfigureAwait(false);

                var encoded = Web3.OfflineTransactionSigner.SignTransaction(privateKey, _contract.Address, amount, txCount.Value, 1000000000000L, gasLimit, data);
                var transaction = await _web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(encoded).ConfigureAwait(false);

                return transaction;
            }
            catch (Exception e)
            {
                _nethereumLogger.Error(e.Message);
                return null;
            }
        }
    }
}
