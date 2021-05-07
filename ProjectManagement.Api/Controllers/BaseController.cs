using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    //public class BaseController<T, TRepository> : ControllerBase
    //     where T :  BaseEntity
    //    where TRepository:IBaseRepository<T>
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : BaseEntity
    
    {
        private readonly IBaseRepository<T> Repository;

        public BaseController()
        {
            Repository = DependencyResolver.Current.GetService<IBaseRepository<T>>();
        }

        public BaseController(IBaseRepository<T> baseRepository)
        {
            Repository = baseRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(Repository.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            T result = Repository.Get(id);
            if (result is null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            T result = await Repository.Add(entity);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            T result = await Repository.Update(entity);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            T existing = Repository.Get(id);
            if (existing is null)
            {
                return BadRequest();
            }

            await Repository.Delete(id);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            return Ok();
        }
    }
}

