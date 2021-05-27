using System;
namespace lab2.ViewModels
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public DateTime DateTime { get; set; }
    }
}
