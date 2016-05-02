//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using Repository.Repositories;

//namespace CorridorAPI
//{
//    public class CorridorDB : IdentityDbContext<IdentityUser>
//    {
//        public DbSet<Staff> Staffs { get; set; }
//        public DbSet<Staff_Schedule> Staff_Schedules { get; set; }
//        public DbSet<Staff_Corridor> Staffs_Corridors { get; set; }
//        public DbSet<Corridor> Corridors { get; set; }
//        public DbSet<Schedule> Schedule { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//        }

//        public CorridorDB() : base("CorridorDB") //Sätt connectionstring i Web.config
//        {

//        }

//    }
//}