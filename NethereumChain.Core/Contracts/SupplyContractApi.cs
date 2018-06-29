using System;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using NethereumChain.Core.Models;

namespace NethereumChain.Core.Contracts
{
    public class SupplyContractApi : BaseContract
    {
        public SupplyContractApi(string address, string contractLocation, Web3 web3) : base(address, contractLocation, web3)
        {

        }

        public (string, LocationModel) GetLocation(string userAddress, int locationId)
        {
            try
            {
                var gas = new HexBigInteger(new BigInteger(400000));
                var value = new HexBigInteger(new BigInteger(0));
                var getLocationFunction = Contract.GetFunction("GetLocation").SendTransactionAsync(userAddress, gas, value, locationId);
                getLocationFunction.Wait();

                var location = getLocationFunction.Result;

                var lm = new LocationModel();
                
                return (null, lm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (e.Message, null);
            }
        }

        public string AddNewLocation(string userAddress, LocationModel location)
        {
            try
            {
                var gas = new HexBigInteger(new BigInteger(400000));
                var value = new HexBigInteger(new BigInteger(0));
                var addLocationFunction = Contract.GetFunction("AddNewLocation").SendTransactionAsync(userAddress, gas, value, location.LocationId, location.LocationName, "noSecret");
                addLocationFunction.Wait();
                Console.WriteLine($"Added {location.LocationName}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.Message;
            }
        }
    }
}
