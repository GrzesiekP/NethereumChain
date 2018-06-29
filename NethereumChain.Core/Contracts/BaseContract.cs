using Nethereum.Contracts;
using Nethereum.Web3;

namespace NethereumChain.Core.Contracts
{
    public class BaseContract
    {
        private string _address;
        private string _ABI;
        public Contract Contract { get; }

        public BaseContract(string address, string contractLocation, Web3 web3)
        {
            const string abiFile = @"solc\ABI\SupplyChainAbi.txt";

            _ABI = GetAbiFromFile(abiFile);

            _address = address;

            //_ABI = GetAbiFromFile(contractLocation);

            Contract = web3.Eth.GetContract(_ABI, _address);
        }

        private string GetAbiFromFile(string abiFileLocation)
        {
            var contractFile = System.IO.File.ReadAllText(abiFileLocation);
            var abi = contractFile.Substring(contractFile.IndexOf('['));

            return abi;
        }
    }
}
