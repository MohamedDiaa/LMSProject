using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.api.Model;
using LMS.api.Extensions;
using LMS.api.DTO;
using System.Globalization;

namespace LMS.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly LMSContext _context;

        public CoursesController(LMSContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, UpdateCourse updateCourse)
        {
            if (id != updateCourse.Id)
            {
                return BadRequest();
            }

            var course = await _context.Course.FindAsync(id);

            if (!String.IsNullOrEmpty(updateCourse.Title))
            {
                course.Title = updateCourse.Title;
            }
            if (!String.IsNullOrEmpty(updateCourse.Description))
            {
                course.Description = updateCourse.Description;
            }
            if (updateCourse.MaxCapcity is int maxCapcity)
            {
                course.MaxCapcity = maxCapcity;
            }
            if (updateCourse.Start is DateTime start)
            {
                course.Start = start;
            }
            if (updateCourse.End is DateTime end)
            {
                course.End = end;
            }


            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /* [HttpPost]
         public async Task<ActionResult<CourseDTO>> PostCourse(CourseDTO course)
         {
             _context.Course.Add(course);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetCourse", new { id = course.Id }, course);
         }
        */

        //New POST method because module and student was required in the above. 
        [HttpPost]
        public async Task<ActionResult<CourseDTO>> PostCourse(CourseDTO courseDto)
        {

            
            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                MaxCapcity = courseDto.MaxCapcity,
                Start = courseDto.Start,
                End = courseDto.End,
         
            };

            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            // Map back to CourseDTO if needed
            var createdCourseDto = new CourseDTO
            {
                Title = course.Title,
                Description = course.Description,
                MaxCapcity = course.MaxCapcity,
                Start = course.Start,
                End = course.End
            };

            return CreatedAtAction("GetCourse", new { id = course.Id }, createdCourseDto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        // GET: api/Users/5
        [HttpGet("User/{id}")]
        public async Task<ActionResult<Course>> GetCourseForUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(user.CourseID);

            if (course == null)
            {
                return NotFound();
            }
            return course;
        }
    }
}
