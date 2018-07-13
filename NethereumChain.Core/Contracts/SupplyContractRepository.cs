using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using NethereumChain.Core.Logging;
using NethereumChain.Core.Models;

namespace NethereumChain.Core.Contracts
{
    public class SupplyContractRepository
    {
        private readonly Contract _contract;

        internal SupplyContractRepository(string address, Web3 web3)
        {
            _contract = new BaseContract(address, web3).Contract;
        }

        public async Task<int> GetChainCount()
        {
            return await _contract.GetFunction("GetChainCount").CallAsync<int>();
        }

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
                Logger.Error(e.Message);
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
                    Logger.Error(e.Message);
                    return null;
                }
            }

            return locations;
        }

        public string AddNewLocation(string userAddress, int gas, int value, Location location)
        {
            try
            {
                var gasHex = new HexBigInteger(new BigInteger(400000));
                var valueHex = new HexBigInteger(new BigInteger(0));
                var addLocationFunction = _contract.GetFunction("AddNewLocation")
                    .SendTransactionAsync(userAddress, gasHex, valueHex, location.LocationName);
                addLocationFunction.Wait();
                return null;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return e.Message;
            }
        }
    }
}
