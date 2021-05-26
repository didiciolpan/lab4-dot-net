 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab1.Data;
using lab1.Models;
using lab2.Models;
using lab2.ViewModels;

namespace lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksViewModel>> GetTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);

            var tasksViewModel = new TasksViewModel
            {
                Title = tasks.Title,
                Description = tasks.Description,
                DateTimeAdded = tasks.DateTimeAdded,
                Deadline = tasks.Deadline,
                Importance = tasks.Importance,
                Status = tasks.Status,
                DateTimeClosedAt = tasks.DateTimeClosedAt
              

            };

            if (tasks == null)
            {
                return NotFound();
            }

            return tasksViewModel;
        }


        /// <summary>
        /// Returns tasks between selected dates
        /// </summary>
        /// <param name="startDate">The Start Date </param>
        /// <param name="endDate">The End Date </param>
        /// <returns>A list of tasks with from-to filter on date</returns>
        // GET: api/Tasks/filter
        [HttpGet]
        [Route("filter/{startDate & endDate}")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks(DateTime startDate, DateTime endDate)
        {
            var query = _context.Tasks.Where(t => t.Deadline >= startDate && t.Deadline <= endDate);
            Console.WriteLine(query.ToQueryString());

            return await query.ToListAsync();

        }

        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<Object>> GetCommentsForTasks(int id)
        {
            var query = _context.Comments.Where(c => c.Tasks.Id == id).Include(c => c.Tasks).Select(c => new
            {
                Task = c.Tasks.Title,
                Comment = c.Text
            });

            return query.ToList();
        }

        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentsForTasks(int id, Comment comment)
        {
            comment.Tasks = _context.Tasks.Find(id);
            if (comment.Tasks == null)
            {
                return NotFound();
            }
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }



        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return BadRequest();
            }

            _context.Entry(tasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks tasks)
        {
            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.Id }, tasks);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
