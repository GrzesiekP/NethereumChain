using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace NethereumChain.Core.Contracts
{
    public class VoteContractApi : BaseContract
    {
        private const string ABI = @"[{ 'constant':true,'inputs':[],'name':'candidate1','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'candidate','type':'uint256'}],'name':'castVote','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'},{'constant':true,'inputs':[],'name':'candidate2','outputs':[{'name':'','type':'uint256'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':true,'inputs':[{'name':'','type':'address'}],'name':'voted','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'}]";

        public int Candidate1 => GetVotes(1);
        public int Candidate2 => GetVotes(2);

        public VoteContractApi(string address, Web3 web3) : base(address, ABI, web3)
        {

        }

        private int GetVotes(int candidateNumber)
        {
            if (!(candidateNumber == 1 || candidateNumber == 2))
                throw new ArgumentOutOfRangeException();

            try
            {   
                Task<BigInteger> function = Contract.GetFunction($"candidate{candidateNumber}").CallAsync<BigInteger>();
                function.Wait();

                int votes = (int)function.Result;
                return votes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            
        }

        public void CastVote(int candidateNumber, string voterAddress)
        {
            if (!(candidateNumber == 1 || candidateNumber == 2))
                throw new ArgumentOutOfRangeException();

            var gas = new HexBigInteger(new BigInteger(400000));
            var value = new HexBigInteger(new BigInteger(0));
            Task<string> castVoteFunction = Contract.GetFunction("castVote").SendTransactionAsync(voterAddress, gas, value, candidateNumber);
            castVoteFunction.Wait();
            Console.WriteLine("Vote Cast!");
        }
    }
}
