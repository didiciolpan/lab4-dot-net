using System;
using System.Collections.Generic;
using lab1.Models;

namespace lab2.Models
{
    public class AssignedTasks
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Tasks> Tasks { get; set; }
        public DateTime AssignedDateTime { get; set; }

    }
}
