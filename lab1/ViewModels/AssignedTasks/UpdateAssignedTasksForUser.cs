using System;
using System.Collections.Generic;

namespace lab2.ViewModels.AssignedTasks
{
    public class UpdateAssignedTasksForUser
    {
        public int Id { get; set; }
        public List<int> AssignedTasksId { get; set; }
    }
}
