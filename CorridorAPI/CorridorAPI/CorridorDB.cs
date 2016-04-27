using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using CorridorAPI.Models;

namespace CorridorAPI
{
    public class CorridorDB : IdentityDbContext<IdentityUser>
    {
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Staff_Task> Staffs_Tasks { get; set; }
        public DbSet<Staff_Corridor> Staffs_Corridors { get; set; }
        public DbSet<Corridor> Corridors { get; set; }
        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public CorridorDB() : base("") //Sätt connectionstring i Web.config
        {

        }

    }
}