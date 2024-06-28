using LMS.api.Model;
using LMS.api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : GenericController<Activity>
    {
        public ActivityController(IGenericApiService<Activity> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : GenericController<Course>
    {
        public CourseController(IGenericApiService<Course> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : GenericController<Module>
    {
        public ModuleController(IGenericApiService<Module> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : GenericController<Role>
    {
        public RoleController(IGenericApiService<Role> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : GenericController<Student>
    {
        public StudentController(IGenericApiService<Student> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : GenericController<Teacher>
    {
        public TeacherController(IGenericApiService<Teacher> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : GenericController<User>
    {
        public UserController(IGenericApiService<User> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity> : ControllerBase where TEntity : class, IEntity
    {
        private readonly IGenericApiService<TEntity> _service;

        public GenericController(IGenericApiService<TEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
