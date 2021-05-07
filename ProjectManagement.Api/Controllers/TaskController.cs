using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Task")]
    public class TaskController : BaseController<Task>
    {
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult GetAllTasks()
        //{
        //    return Get();
        //}

        //[HttpGet]
        //public IActionResult GetTask(long id)
        //{
        //    return Get(id);
        //}

        //[HttpPost]
        //public async System.Threading.Tasks.Task<IActionResult> CreateTaskAsync([FromBody] Task task)
        //{
        //    return await Post();
        //}

        //[HttpPut]
        //public IActionResult UpdateTask([FromBody] Task task)
        //{
        //    return Put();
        //}

        //[HttpDelete]
        //public IActionResult DeleteTask(long id)
        //{
        //    return Delete(id);
        //}

    }
}
