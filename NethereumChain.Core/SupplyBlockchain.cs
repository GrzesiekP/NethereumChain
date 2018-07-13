using Nethereum.Web3;
using NethereumChain.Core.Contracts;

namespace NethereumChain.Core
{
    public class SupplyBlockchain
    {
        private readonly string _url;
        private Web3 Web3Api => new Web3(_url);

        public SupplyContractRepository SupplyChainContract { get; private set; }

        public SupplyBlockchain(string url, string address)
        {
            _url = url;
            SupplyChainContract = new SupplyContractRepository(address, Web3Api);
        }
    }
}
