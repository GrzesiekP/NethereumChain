using Nethereum.Web3;
using NUnit.Framework;
using NethereumChain.Core.Contracts;
using NethereumChain.Core.Logging;
using NethereumChain.Core.Models;

namespace NethereumChain.Test
{
    [TestFixture]
    public class ContractTests
    {
        private const string USER_ADDRESS = "0xD51DD787c49d8f2dC64b91AB12E29ED9d4721b10";
        private const string SUPPLY_CHAIN_CONTRACT_ADDRESS = "0x6ac0f5f416ecb32cd14db9df5f0ca11f41b5c625";
        private const string INFURA_API_ADDRESS = "https://ropsten.infura.io/4cOog5xqs8S0jblMEOaA";
        private const int GAS = 400000;
        private const int VALUE = 0;

        private SupplyContractRepository _repository;

        private void Setup()
        {
            var web3 = new Web3(INFURA_API_ADDRESS);
            _repository = new SupplyContractRepository(SUPPLY_CHAIN_CONTRACT_ADDRESS, web3, new NethereumLogger());
        }

        private Location GetSampleLocationModel(int id = 0)
        {
            id = id == 0 ?
                _repository.GetChainCount().Result :
                id;

            return new Location
            {
                LocationName = $"Location{id}"
            };
        }



        [Test]
        [Category("Blockchain")]
        public void Test1()
        { 

        }
    }
}
