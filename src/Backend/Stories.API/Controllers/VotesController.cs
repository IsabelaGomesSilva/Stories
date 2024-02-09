using Microsoft.AspNetCore.Mvc;
using Stories.API.Request;
using Stories.API.ViewModel;
using Stories.Service.Services;
using System.Net;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly StoryService _service;

        public VotesController(StoryService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<VoteViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> PostVote(VoteRequest voteRequest)
        {

            VoteDto voteDto = new()
            {
                Voted = voteRequest.Voted,
                UserId = voteRequest.UserId,
                StoryId = voteRequest.StoryId,
            };
            var result = await _service.AddVoted(voteDto);
            voteDto.Id = result.isCreatedId;
            
            if (!result.isCreated)
                return BadRequest();
            else
                return Created($"api/Votes/{voteDto.Id}", voteDto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VoteViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> GetVotes()
        {
            var votes = await _service.GetVotes();
            if (!votes.Any()) return NoContent();
            else
            {
                votes.Select(s => new VoteViewModel
                {
                    Id = s.Id,
                    Voted = s.Voted,
                    UserId = s.UserId,
                    StoryId = s.StoryId
                }).ToList();
                return Ok(votes);
            }
        }
    }
}
