using NethereumChain;
using NUnit.Framework;

namespace NethereumChain.Test
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void ShouldReadUrl()
        {
            var hostFromConfig = ConfigurationProvider.BlockchainAddress;
            Assert.False(string.IsNullOrWhiteSpace(hostFromConfig));
        }
    }
}
