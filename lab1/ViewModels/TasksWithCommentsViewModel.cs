using System;
using System.Collections.Generic;

namespace lab2.ViewModels
{
    public class TasksWithCommentsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        public string Importance { get; set; }
        public string Status { get; set; }
        public DateTime DateTimeClosedAt { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
