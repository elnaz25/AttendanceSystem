using AttendanceSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AttendanceSystem.Data
{
    public class AttendanceDb :DbContext
    {
        public AttendanceDb() : base("AttendanceDB") { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Attendances> Attendances { get; set; }
    }
}