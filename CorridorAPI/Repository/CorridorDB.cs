using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Repositories
{
    public class CorridorDB : IdentityDbContext<IdentityUser>
    {
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Staff_Schedule> Staff_Schedules { get; set; }
        public DbSet<Staff_Corridor> Staffs_Corridors { get; set; }
        public DbSet<Corridor> Corridors { get; set; }
        public DbSet<Schedule> Schedule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public CorridorDB() : base("CorridorDB") //Sätt connectionstring i Web.config
        {

        }
    }
}
