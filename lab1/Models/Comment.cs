using System;
using lab1.Models;

namespace lab2.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public DateTime DateTime { get; set; }
        public Tasks Tasks { get; set; }

    }
}

