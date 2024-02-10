using System.Net;
using Microsoft.AspNetCore.Mvc;
using Stories.API.ViewModel;
using Stories.Service.Services;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService) =>  _userService = userService;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> Get()
        {
            var users = await _userService.Get();
            _ = users.Select(u => new UserViewModel { Id = u.Id, Name = u.Name }).ToList();
            if(!users.Any())
                 return NoContent();
            else
                 return Ok(users);
        }
    }
}