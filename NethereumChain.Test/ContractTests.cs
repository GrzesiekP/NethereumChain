using System;
using NUnit.Framework;
using NethereumChain;
using NethereumChain.Core;
using NethereumChain.Core.Models;

namespace NethereumChain.Test
{
    [TestFixture]
    public class ContractTests
    {
        private const string USER_ADDRESS = "0xD51DD787c49d8f2dC64b91AB12E29ED9d4721b10";

        [Test]
        public void AccessBlockchainSupplyChainContract()
        { 
            var chain = new SupplyBlockchain(ConfigurationProvider.BlockchainAddress);
            var scContract = chain.SupplyChainContract;
            
            Assert.NotNull(scContract);
        }

        [Test]
        public void AddNetLocationToSuppluChain()
        {
            var chain = new SupplyBlockchain(ConfigurationProvider.BlockchainAddress);
            var scContract = chain.SupplyChainContract;

            var location = new LocationModel
            {
                LocationId = 12,
                LocationName = "Location12",
                PreviousLocationId = 11,
                Timestamp = DateTime.Now.Ticks
            };

            var errorMessage = scContract.AddNewLocation(USER_ADDRESS, location);

            Assert.Null(errorMessage);
        }

        [Test]
        public void AddAndGetLocationToSupplyChain()
        {
            var chain = new SupplyBlockchain(ConfigurationProvider.BlockchainAddress);
            var scContract = chain.SupplyChainContract;

            var location = new LocationModel
            {
                LocationId = 12,
                LocationName = "Location12",
                PreviousLocationId = 11,
                Timestamp = DateTime.Now.Ticks
            };

            var errorMessage = scContract.AddNewLocation(USER_ADDRESS, location);

            Assert.Null(errorMessage);

            var locationFromBlockchain = scContract.GetLocation(USER_ADDRESS, 12);


        }
    }
}
