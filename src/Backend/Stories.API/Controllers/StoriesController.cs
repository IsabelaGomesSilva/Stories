using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stories.API.Request;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        
        public StoriesController(){}

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async  Task<ActionResult> Post([FromServices] IMediator mediator, [FromBody] CreateStoryRequest request)
        {
            var  response =  mediator.Send(request);
            if (!response.IsCompletedSuccessfully)
                  return BadRequest();
             else
                //  return Created($"api/Stories/{storyDto.Id}", storyDto);
             return Ok(response);   
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Get([FromServices] IMediator mediator)
        {
            GetAllStoriesRequest request = new();
            var stories = mediator.Send(request);
            if (stories == null )
               return NoContent();
            else
               return Ok(stories.Result);   
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public  ActionResult Get(int id,[FromServices] IMediator mediator )
        {
            GetByIdStoryRequest request = new (){ Id = id};
            var story = mediator.Send(request);
            
            if(story == null ) return NoContent(); 
            else
              return Ok(story.Result); 
            
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id, [FromServices] IMediator mediator)
        {
            DeleteStoryRequest request = new (){ Id = id};
            var response = mediator.Send(request);

            if (response == null)
                return NoContent();
            else if (! response.IsCompletedSuccessfully)
                return BadRequest();
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Put(int id, [FromServices] IMediator mediator, [FromBody] UpdateBodyStoryRequest bodyRequest)
        {
            UpdateStoryRequest request = new()
            {
                Id = id, 
                Body = bodyRequest
            };

            var response = mediator.Send(request);
            if (response == null) return NoContent();
            else
            {
                if (!response.IsCompletedSuccessfully)
                    return BadRequest();
                else
                    return Ok();
            }
        }
    }
}