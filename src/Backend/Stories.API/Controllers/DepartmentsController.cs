using System.Net;
using Microsoft.AspNetCore.Mvc;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartamentService _service;
        public DepartmentsController(DepartamentService service) => _service = service;
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartmentViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Get()
        {
            var  departments = await _service.Get();
            _ = departments.Select(u => new DepartmentViewModel { Id = u.Id, Name = u.Name }).ToList();
            if(!departments.Any())
                 return NoContent();
            else
                 return Ok(departments);
        }
    }
}