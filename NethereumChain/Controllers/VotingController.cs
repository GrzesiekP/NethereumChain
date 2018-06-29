using System;
using Microsoft.AspNetCore.Mvc;
using NethereumChain.Core;
using NethereumChain.Core.Contracts;
using NethereumChain.Core.Models;

namespace NethereumChain.Controllers
{
    [Route("api/vote")]
    public class VotingController : Controller
    {
        private VoteContractApi VotingContract => new SupplyBlockchain(ConfigurationProvider.BlockchainAddress).VoteContract;

        [HttpGet]
        public IActionResult GetScores()
        {
            var candidate1Score = VotingContract.Candidate1;
            var candidate2Score = VotingContract.Candidate2;

            return new JsonResult(
                new { cand1Score = candidate1Score, cand2Score = candidate2Score}
            );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Vote([FromBody] VoteModel voteModel)
        {
            if (voteModel.CandidateNumber != 1 && voteModel.CandidateNumber != 2)
                return BadRequest();

            try
            {
                VotingContract.CastVote(voteModel.CandidateNumber, voteModel.VoterAddress);
                return RedirectToAction("GetScores");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
    }
}
