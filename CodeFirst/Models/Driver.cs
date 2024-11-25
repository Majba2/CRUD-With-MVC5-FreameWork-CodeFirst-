using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class Driver
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverID { get; set; }
        public string DriverName { get; set; }
            public int Age { get; set; }
            public DateTime DateOfBirth     { get; set; }
        public bool IsAvailable { get; set; }
        public string Picture { get; set; }
        public virtual IList<Trip> Trip { get; set; }
    }
}