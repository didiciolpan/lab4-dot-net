using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using lab1.Data;
using lab1.Models;
using lab2.Models;
using lab2.ViewModels.AssignedTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab2.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class AssignedTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignedTasksController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> AssignATask(NewAssignedTask newAssignedTask)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Tasks> assignedTasks = new List<Tasks>();
            newAssignedTask.AssignedTasksId.ForEach(tid =>
            {
                var taskWithId = _context.Tasks.Find(tid);
                if (taskWithId != null)
                {
                    assignedTasks.Add(taskWithId);
                }
            });

            if (assignedTasks.Count == 0)
            {
                return BadRequest();
            }

            var assignedTask = new AssignedTasks
            {
                ApplicationUser = user,
                AssignedDateTime = newAssignedTask.AssignedTaskDateTime.GetValueOrDefault(),
                Tasks = assignedTasks
            };

            _context.AssignedTasks.Add(assignedTask);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _context.AssignedTasks.Where(o => o.ApplicationUser.Id == user.Id).Include(at => at.Tasks).FirstOrDefault();
            var resultViewModel = _mapper.Map<AssignedTasksForUserResponse>(result);

            return Ok(resultViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewAssignedTask>> GetTasks(int id)
        {
            var tasks = await _context.AssignedTasks.FindAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            var assignedTasksViewModel = _mapper.Map<NewAssignedTask>(tasks);

            return assignedTasksViewModel;
        }

        [HttpPut]
        public async Task<ActionResult> PutAssignedTasks(UpdateAssignedTasksForUser updateAssignedTasksForUser)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            AssignedTasks assignedTasks = _context.AssignedTasks.Where(at => at.Id == updateAssignedTasksForUser.Id && at.ApplicationUser.Id == user.Id).Include(at => at.Tasks).FirstOrDefault();

            if (assignedTasks == null)
            {
                return BadRequest("There is no assigned Tasks with this ID.");
            }

            updateAssignedTasksForUser.AssignedTasksId.ForEach(atid =>
            {
                var task = _context.Tasks.Find(atid);
                if (task != null && !assignedTasks.Tasks.Contains(task))
                {
                    assignedTasks.Tasks.ToList().Add(task);
                }
            });

            _context.Entry(assignedTasks).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var assignedTasks = _context.AssignedTasks.Where(at => at.ApplicationUser.Id == user.Id && at.Id == id).FirstOrDefault();

            if (assignedTasks == null)
            {
                return NotFound();
            }

            _context.AssignedTasks.Remove(assignedTasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
