using System.IO;
using Microsoft.Extensions.Configuration;

namespace NethereumChain
{
    public static class AppConfigurationProvider
    {
        private static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                return builder.Build();
            }
        }

        public static string ContractAddress(string contractName) => 
            Configuration[$"Blockchain:Contracts:{contractName}"];

        private static string GetBlockchainAddress()
        {
            var port = Configuration["Blockchain:Network:Port"].ToString();
            var host = Configuration["Blockchain:Network:Host"];
            var protocol = Configuration["Blockchain:Network:Protocol"];

            return string.IsNullOrWhiteSpace(port) ?
                $"{protocol}://{host}" :
                $"{protocol}://{host}:{port}";
        }

        public static string BlockchainAddress => GetBlockchainAddress();

        public static string InfuraApiAddress 
            => $"{Configuration["Blockchain:Infura:NetworkAddress"]}/{Configuration["Blockchain:Infura:ApiKey"]}";
    }
}
