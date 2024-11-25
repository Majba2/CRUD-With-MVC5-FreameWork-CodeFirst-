using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class Trip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TripID { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleID { get; set; }
        [ForeignKey("Driver")]
        public int DriverID { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Driver Driver { get; set; }  
    }
}