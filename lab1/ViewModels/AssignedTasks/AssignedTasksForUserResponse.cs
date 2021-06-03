using System;
using System.Collections.Generic;

namespace lab2.ViewModels.AssignedTasks
{
    public class AssignedTasksForUserResponse
    {
        public ApplicationUserViewModel ApplicationUser { get; set; }
        public List<TasksViewModel> Tasks { get; set; }
        public DateTime AssignedTasksDateTime { get; set; }
    }
}
