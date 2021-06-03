﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using lab2.Models;

namespace lab1.Models
{
    public class Tasks
    {
        //title, description, dată+timp added, deadline, importance: low, medium, high, stare:open, in progress, closed,dată+timpclosedAt
        public int Id{ get; set;}

     //   [Required]
        public string Title { get; set; }

     //   [MinLength(10)]
        public string Description { get; set; }

    //    [DataType(DataType.DateTime)]
        public DateTime DateTimeAdded { get; set; }

     //   [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }

     //   [RegularExpression("Low|Medium|High|...", ErrorMessage = "Importance can only have values Low, Medium or High")]
        public string Importance { get; set; }

     //   [RegularExpression("open|in progress|closed|...", ErrorMessage = "Status can only have values open, in progress or closed")]
        public string Status { get; set; }

     //   [DataType(DataType.DateTime)]
        public DateTime DateTimeClosedAt { get; set; }

        public List<Comment> Comments { get; set; }

        public List<AssignedTasks> AssignedTasks { get; set; }

    }
}
