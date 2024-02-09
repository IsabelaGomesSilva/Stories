using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public DepartmentsController(DepartamentService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartmentViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Get()
        {
            var  departments = await _service.Get();
            departments.Select(u => new UserViewModel {Id = u.Id, Name = u.Name});
            if(!departments.Any())
                 return NoContent();
            else
                 return Ok(departments);

        }

    }
}