using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using CodeFirst.Models.ViewModels;

namespace CodeFirst.Controllers
{
    public class DriverController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Driver
        public ActionResult Index()
        {
            var drivers = db.Driver.Include(e => e.Trip.Select(c => c.Vehicle)).OrderByDescending(s => s.DriverID).ToList();
            return View(drivers);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AddNewVehicle(int? id)
        {
            ViewBag.vehicle = new SelectList(db.Vehicle.ToList(), "VehicleID", "VehicleName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewVehicle");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverVM driverVM, int[] vehicleID)
        {
            if (ModelState.IsValid)
            {
                Driver driver = new Driver()
                {
                    DriverName = driverVM.DriverName,
                    Age = driverVM.Age,
                    DateOfBirth = driverVM.DateOfBirth,
                    IsAvailable = driverVM.IsAvailable,
                };
                HttpPostedFileBase file = driverVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/" + DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    driver.Picture = filePath;

                }
                foreach (var item in vehicleID)
                {
                    Trip trip = new Trip()
                    {
                        Driver = driver,
                        DriverID = driverVM.DriverID,
                        VehicleID = item,
                    };
                    db.Trip.Add(trip);
                }
                db.Driver.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            Driver driver = db.Driver.FirstOrDefault(s => s.DriverID == id);
            var driverVM = new DriverVM()
            {
                DriverID = driver.DriverID,
                DriverName = driver.DriverName,
                Picture = driver.Picture,
                DateOfBirth = driver.DateOfBirth,
                IsAvailable = driver.IsAvailable,
                Age = driver.Age,
                Trip = db.Trip.Where(e => e.DriverID == driver.DriverID).ToList()
            };
            return View(driverVM);
        }
        [HttpPost]
        public ActionResult Edit(DriverVM driverVM, int[] vehicleID)
        {
            if (ModelState.IsValid)
            {

                var drivObj = db.Driver.Find(driverVM.DriverID);
                if (drivObj != null)
                {
                    drivObj.DriverID = driverVM.DriverID;
                    drivObj.DriverName = driverVM.DriverName;
                    drivObj.Age = driverVM.Age;
                    drivObj.IsAvailable = driverVM.IsAvailable;
                    drivObj.DateOfBirth = driverVM.DateOfBirth;
                }
                HttpPostedFileBase file = driverVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/" + DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    drivObj.Picture = filePath;

                }
                else
                {
                    drivObj.Picture = driverVM.Picture;
                }
                var existsVehicle = db.Trip.Where(s => s.DriverID == drivObj.DriverID).ToList();
                foreach (var vehicle in existsVehicle)
                {

                    db.Trip.Remove(vehicle);
                }
                foreach (var item in vehicleID)
                {

                    Trip trip = new Trip()
                    {
                        Driver = drivObj,
                        DriverID = driverVM.DriverID,
                        VehicleID = item,
                    };
                    db.Trip.Add(trip);
                }
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {

                var drivObj = db.Driver.FirstOrDefault(s => s.DriverID == id);
                var tripInfo = db.Trip.Where(s => s.DriverID == id).ToList();
                foreach (var trip in tripInfo)
                {
                    db.Trip.Remove(trip);
                }
                db.Driver.Remove(drivObj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}