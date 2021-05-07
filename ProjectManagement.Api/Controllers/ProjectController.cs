using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Project")]
    public class ProjectController : BaseController<Project>
    {
        public ProjectController() :base()
        {

        }
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult GetAllProjects()
        //{
        //    return Get();
        //}

        //[HttpGet]
        //public IActionResult GetProject(long id)
        //{
        //    return Get(id);
        //}

        //[HttpPost]
        //public IActionResult CreateProject([FromBody] Project project)
        //{
        //    return Post();
        //}

        //[HttpPut]
        //public IActionResult UpdateProject([FromBody] Project project)
        //{
        //    return Put();
        //}

        //[HttpDelete]
        //public IActionResult DeleteProject(long id)
        //{
        //    return Delete(id);
        //}

    }
}
