using System.IO;
using Microsoft.Extensions.Configuration;

namespace NethereumChain
{
    public static class AppConfigProvider
    {
        public static void Initialize(IConfiguration configuration)
        {
            ContractAddress = configuration["Blockchain:Contracts:SupplyChain"];

            var port = configuration["Blockchain:Network:Port"].ToString();
            var host = configuration["Blockchain:Network:Host"];
            var protocol = configuration["Blockchain:Network:Protocol"];

            BlockChainAddress = string.IsNullOrWhiteSpace(port) ?
                $"{protocol}://{host}" :
                $"{protocol}://{host}:{port}";

            InfuraApiAddress = $"{configuration["Blockchain:Infura:NetworkAddress"]}/{configuration["Blockchain:Infura:ApiKey"]}";
        }

        public static string ContractAddress { get; set; }
        public static string BlockChainAddress { get; set; }
        public static string InfuraApiAddress { get; set; }            
    }
}
