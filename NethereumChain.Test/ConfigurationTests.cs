using NUnit.Framework;

namespace NethereumChain.Test
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void ShouldReadUrl()
        {
            var hostFromConfig = AppConfigurationProvider.BlockchainAddress;
            Assert.False(string.IsNullOrWhiteSpace(hostFromConfig));
        }

        [Test]
        public void ShouldReadContractAddress()
        {
            var addressFromConfig = AppConfigurationProvider.ContractAddress("SupplyChain");
            Assert.False(string.IsNullOrWhiteSpace(addressFromConfig));
        }
    }
}
