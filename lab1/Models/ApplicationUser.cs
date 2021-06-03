﻿using lab2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<AssignedTasks> AssignedTasks { get; set; }
    }
}
