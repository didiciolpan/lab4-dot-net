using System;

namespace lab2.ViewModels
{
    public class TasksViewModel
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        public string Importance { get; set; }
        public string Status { get; set; }
        public DateTime DateTimeClosedAt { get; set; }

    }
}
