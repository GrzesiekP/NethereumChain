using Microsoft.Extensions.Configuration;

namespace NethereumChain.Core.Configuration
{
    public class AppSettingsProvider
    {
        public static void GetConfigurationFrom(IConfiguration configuration)
        {
            ContractAddress = configuration[ConfigurationKeys.ContractAddress];

            var port = configuration[ConfigurationKeys.BlockChainPort];
            var host = configuration[ConfigurationKeys.BlockChainPort];
            var protocol = configuration[ConfigurationKeys.BlockChainProtocol];

            BlockChainAddress = string.IsNullOrWhiteSpace(port) ?
                $"{protocol}://{host}" :
                $"{protocol}://{host}:{port}";

            InfuraApiAddress = $"{configuration[ConfigurationKeys.InfuraApiAddress]}/{configuration[ConfigurationKeys.InfuraApiKey]}";
        }

        public static string ContractAddress { get; private set; }
        public static string BlockChainAddress { get; private set; }
        public static string InfuraApiAddress { get; private set; }
    }
}
