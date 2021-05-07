using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Implementation;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : BaseController<User>
    {

        public UserController() : base()
        {

        }
        //private readonly IBaseRepository<User> baseRepository1;
        //public UserController(IBaseRepository<User> baseRepository)
        //{
        //    baseRepository1 = baseRepository;
        //}
        ////[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult GetAllUsers()
        //{
        //    return (IActionResult)baseRepository1.Get();
        //}

        //[HttpGet]
        //public IActionResult GetUser(long id)
        //{
        //    return Get(id);
        //}

        //[HttpPost]
        //public IActionResult CreateUser([FromBody] User user)
        //{
        //    return Post();
        //}

        //[HttpPut]
        //public IActionResult UpdateUser([FromBody] User user)
        //{
        //    return Put();
        //}

        //[HttpDelete]
        //public IActionResult DeleteUser(long id)
        //{
        //    return Delete(id);
        //}

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            return Ok();
        }
    }
}
