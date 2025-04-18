﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<Employee>? Employee { get; set; }
        public DbSet<EmployeePosition>? EmployeePosition { get; set; }
        public DbSet<OfficeBranch>? OfficeBranch { get; set; }
        public DbSet<UploadFile>? UploadFile { get; set; }
    }
}