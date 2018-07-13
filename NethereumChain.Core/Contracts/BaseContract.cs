using Nethereum.Contracts;
using Nethereum.Web3;

namespace NethereumChain.Core.Contracts
{
    public class BaseContract
    {
        public Contract Contract { get; }

        public BaseContract(string address, Web3 web3)
        {
            const string abiFile = @"solc\ABI\SupplyChainAbi.txt";

            var abi = GetAbiFromFile(abiFile);

            Contract = web3.Eth.GetContract(abi, address);
        }

        private string GetAbiFromFile(string abiFileLocation)
        {
            var contractFile = System.IO.File.ReadAllText(abiFileLocation);
            var abi = contractFile.Substring(contractFile.IndexOf('['));

            return abi;
        }
    }
}
