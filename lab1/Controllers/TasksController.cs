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
using AutoMapper;

namespace lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper; 

        public TasksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }


        /// <summary>
        /// Returns all existing tasks
        /// </summary>
        /// <returns>A list with all tasks</returns>
        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// Returns a task with by gived id
        /// </summary>
        /// <param name="id">The id of the task</param>
        /// <returns>A task for a valid given id/returns>
        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksViewModel>> GetTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            var tasksViewModel = _mapper.Map<TasksViewModel>(tasks);

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

        /// <summary>
        /// Returns a Task with comments for a given id
        /// </summary>
        /// <param name="id">Id of the taks</param>
        /// <returns>A task with comments by given id</returns>
        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<TasksWithCommentsViewModel>> GetCommentsForTasks(int id)
        {
            var query_v1 = _context.Comments.Where(c => c.Tasks.Id == id).Include(c => c.Tasks).Select(c => new TasksWithCommentsViewModel
            {
                Id = c.Tasks.Id,
                Title = c.Tasks.Title,
                Description = c.Tasks.Description,
                DateTimeAdded = c.Tasks.DateTimeAdded,
                Deadline = c.Tasks.Deadline,
                Importance = c.Tasks.Importance,
                Status = c.Tasks.Status,
                DateTimeClosedAt = c.Tasks.DateTimeClosedAt,
                Comments = c.Tasks.Comments.Select(tc => new CommentViewModel
                {
                    Id = tc.Id,
                    Text = tc.Text,
                    Important = tc.Important,
                    DateTime = tc.DateTime

                })
            });

            var query_v2 = _context.Tasks.Where(t => t.Id == id).Include(t => t.Comments).Select(t => new TasksWithCommentsViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DateTimeAdded = t.DateTimeAdded,
                Deadline = t.Deadline,
                Importance = t.Importance,
                Status = t.Status,
                DateTimeClosedAt = t.DateTimeClosedAt,
                Comments = t.Comments.Select(tc => new CommentViewModel
                {
                    Id = tc.Id,
                    Text = tc.Text,
                    Important = tc.Important,
                    DateTime = tc.DateTime

                })
            });

            var query_v3 = _context.Tasks.Where(t => t.Id == id).Include(t => t.Comments).Select(t => _mapper.Map<TasksWithCommentsViewModel>(t));

         //   return query_v1.ToList();
         //   return query_v2.ToList();
            return query_v3.ToList();
        }



        /// <summary>
        /// Add a comment to a task
        /// </summary>
        /// <param name="id">Id of the task</param>
        /// <param name="comment">Comment for the task</param>
        /// <returns>returns ok if comment was added and error if not</returns>
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


        /// <summary>
        /// Updates an existing task
        /// </summary>
        /// <param name="id">Id of the task</param>
        /// <param name="tasks">The updated task</param>
        /// <returns>Returns ok if update was successfully otherwise error</returns>
        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, TasksViewModel tasksViewModel)
        {

            var tasks = _mapper.Map<Tasks>(tasksViewModel);

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

        /// <summary>
        /// Adds a task
        /// </summary>
        /// <param name="tasks">The task that will be added</param>
        /// <returns>The added task</returns>
        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(TasksViewModel tasksViewModel)
        {
            var tasks = _mapper.Map<Tasks>(tasksViewModel);

            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasks", new { id = tasks.Id }, tasks);
        }

        /// <summary>
        /// Delete a specific task
        /// </summary>
        /// <param name="id">Id of the task that is going to be deleted</param>
        /// <returns>Returns ok if deleted else not found</returns>
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
