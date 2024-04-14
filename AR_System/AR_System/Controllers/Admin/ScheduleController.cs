using AR_System.Data;
using AR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AR_System.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        private readonly AirlineReservationSystemContext database;

        public ScheduleController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            ViewBag.flightschedule = database.FlightSchedules.Include(fc => fc.FromCity).Include(tc => tc.ToCity).Include(fi => fi.Flight).ToList();
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.flights = database.Flights.ToList();
            ViewBag.locations = database.CityWays.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(FlightSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                database.FlightSchedules.Add(schedule);
                database.SaveChanges();
                TempData["toastr_success"] = "schedule added successfully !";
                return RedirectToAction("Index");
            }
            ViewBag.flights = database.Flights.ToList();
            ViewBag.locations = database.CityWays.ToList();
            return View(schedule);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.flights = database.Flights.ToList();
            ViewBag.locations = database.CityWays.ToList();
            var data = database.FlightSchedules.SingleOrDefault(fs => fs.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(FlightSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                database.Update(schedule);
                database.SaveChanges();
                TempData["toastr_success"] = "Schedule updated successfully!";
                return RedirectToAction("Index");
            }

           
            ViewBag.flights = database.Flights.ToList();
            ViewBag.locations = database.CityWays.ToList();
            return View("Edit", schedule);
        }


        public IActionResult Delete(int? id)
        {
            ViewBag.flights = database.Flights.ToList();
            ViewBag.locations = database.CityWays.ToList();
            var data = database.FlightSchedules.SingleOrDefault(fs => fs.Id == id);
            if (data == null)
            {
                return NotFound(); // Optionally handle case where data is not found
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Add this attribute for security
        public IActionResult ConfirmDelete(int id)
        {
            var schedule = database.FlightSchedules.Find(id);
            if (schedule == null)
            {
                return NotFound(); // Optionally handle case where data is not found
            }
            database.FlightSchedules.Remove(schedule);
            database.SaveChanges();
            TempData["toastr_danger"] = "Flight schedule deleted!";
            return RedirectToAction("Index");
        }

    }
}
