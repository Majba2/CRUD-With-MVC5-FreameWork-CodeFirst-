using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirst.Models.ViewModels
{
    public class DriverVM
    {
        public DriverVM()
        {
            this.Trip = new List<Trip>();
        }
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsAvailable { get; set; }
        public string Picture { get; set; }
        public HttpPostedFileBase PictureFile { get; set; }
        public virtual List<Trip> Trip { get; set; }
    }
}