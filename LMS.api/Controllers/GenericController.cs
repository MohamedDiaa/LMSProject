using LMS.api.Model;

namespace LMS.api.Controllers
{
    using LMS.api.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : GenericController<Activity, int>
    {
        public ActivityController(IGenericResponseService<Activity, int> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : GenericController<Course, int>
    {
        public CourseController(IGenericResponseService<Course, int> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : GenericController<Module, int>
    {
        public ModuleController(IGenericResponseService<Module, int> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : GenericController<Student, string>
    {
        public StudentController(IGenericResponseService<Student, string> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : GenericController<Teacher, string>
    {
        public TeacherController(IGenericResponseService<Teacher, string> service) : base(service) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity, TKey> : ControllerBase, IGenericController<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : notnull
    {
        private readonly IGenericResponseService<TEntity, TKey> _service;

        public GenericController(IGenericResponseService<TEntity, TKey> service)
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
        public async Task<ActionResult<TEntity>> Get(TKey id)
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
        public async Task<IActionResult> Update(TKey id, TEntity entity)
        {
            if (id.Equals(entity.Id))
            {
                return BadRequest();
            }

            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TKey id)
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
