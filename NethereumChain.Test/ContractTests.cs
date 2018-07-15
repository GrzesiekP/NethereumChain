using NUnit.Framework;
using NethereumChain.Core;
using NethereumChain.Core.Contracts;
using NethereumChain.Core.Models;

namespace NethereumChain.Test
{
    [TestFixture]
    public class ContractTests
    {
        private const string USER_ADDRESS = "0xD51DD787c49d8f2dC64b91AB12E29ED9d4721b10";
        private const string SUPPLY_CHAIN_CONTRACT_ADDRESS = "0x6ac0f5f416ecb32cd14db9df5f0ca11f41b5c625";
        private const int GAS = 400000;
        private const int VALUE = 0;

        private Location GetSampleLocationModel(int id = 0)
        {
            id = id == 0 ?
                _contract.GetChainCount().Result :
                id;

            return new Location
            {
                LocationName = $"Location{id}"
            };
        }

        private readonly SupplyContractRepository _contract =
            new SupplyBlockchain(AppConfigurationProvider.BlockchainAddress, SUPPLY_CHAIN_CONTRACT_ADDRESS)
            .SupplyChainContract;

        [Test]
        [Category("Blockchain")]
        public void AccessBlockchainSupplyChainContract()
        { 
            var chain = new SupplyBlockchain(AppConfigurationProvider.BlockchainAddress, SUPPLY_CHAIN_CONTRACT_ADDRESS);
            var scContract = chain.SupplyChainContract;
            
            Assert.NotNull(scContract);
        }
    }
}
