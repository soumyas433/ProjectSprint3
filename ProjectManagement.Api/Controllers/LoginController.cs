using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;

namespace ProjectManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {
        
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            return Ok();
        }
    }
}
