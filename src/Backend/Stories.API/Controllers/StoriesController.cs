using System.Net;
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
        private readonly StoryService _service;
        public StoriesController(StoryService storyService) => _service = storyService;

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(StoryRequest storyRequest)
        {
             StoryDto  storyDto = new()
             {
                 Title = storyRequest.Title, DepartmentId = storyRequest.DepartmentId,
                 Description = storyRequest.Description
             };
             
            var (isCreated, isCreatedId) = await _service.Add(storyDto);
         
            storyDto.Id = isCreatedId;
            if (!isCreated)
                 return BadRequest();
            else
                 return Created($"api/Stories/{storyDto.Id}", storyDto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Get()
        {
            var stories = await _service.Get();
            if(!stories.Any()) return NoContent();
            else
            {
                _ = stories.Select(s => new StoryViewModel
                {
                    Id = s.Id,
                    DepartmentId = s.DepartmentId,
                    Description = s.Description,
                    Title = s.Title
                }).ToList();
             return Ok(stories); 
            } 
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Get(int id)
        {
            var story =  _service.Get(id);
            if(story == null ) return NoContent();
            else
            {
                StoryViewModel storyViewModel = new ()
                {
                    Id = story.Id,
                    DepartmentId = story.DepartmentId,
                    Description = story.Description,
                    Title = story.Title
                };
             return Ok(storyViewModel); 
            } 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            if (_service.Get(id) == null)
                return NoContent();
            else if (! await _service.Delete(id))
                return BadRequest();
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Put(int id, StoryRequest storyRequest)
        {
            if (_service.Get(id) == null)
                return NoContent();
            else
            {
                StoryDto storyDto = new()
                {
                    Id = id,
                    Title = storyRequest.Title,
                    DepartmentId = storyRequest.DepartmentId,
                    Description = storyRequest.Description
                };

                if (!_service.Update(storyDto).Result)
                    return BadRequest();
                else
                    return Ok();
            }
        }
    }
}