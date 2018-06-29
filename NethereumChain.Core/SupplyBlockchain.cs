using Nethereum.Web3;
using NethereumChain.Core.Contracts;

namespace NethereumChain.Core
{
    public class SupplyBlockchain
    {
        private readonly string _url;
        private Web3 Web3Api => new Web3(_url);

        public VoteContractApi VoteContract { get; private set; }
        public SupplyContractApi SupplyChainContract { get; private set; }

        public SupplyBlockchain(string url)
        {
            _url = url;

            //VoteContract = new VoteContractApi("0x468b1e3597f77f9ff3239c0071495671ecec3ade", web3);
            var address = "0x525591d2811b1cef59e05943e82444da57bcd977";
            SupplyChainContract = new SupplyContractApi(address, @"E:\work\inne\Blockchain\nethereum\contracts\SupplyChain.sol", Web3Api);
        }
    }
}
