using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(): base("VehicleDB") { }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}