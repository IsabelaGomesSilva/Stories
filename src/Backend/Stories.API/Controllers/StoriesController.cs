using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public StoriesController(StoryService storyService)
        {
            _service = storyService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Post(StoryRequest storyRequest)
        {
       
             StoryDto  storyDto = new StoryDto 
             {
                 Title = storyRequest.Title, DepartmentId = storyRequest.DepartmentId
                 ,Description = storyRequest.Description
             };
            
            if(!await _service.Add(storyDto))
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
              stories.Select(s => new StoryViewModel { Id = s.Id, DepartmentId = s.DepartmentId
                             ,Description = s.Description, Title = s.Title}).ToList();
             return Ok(stories); 
            } 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var t = await _service.Delete(id);
           
           if(! t)  return BadRequest();
            else
            {
              
             return Ok(); 
            } 
           
        }


    }
}