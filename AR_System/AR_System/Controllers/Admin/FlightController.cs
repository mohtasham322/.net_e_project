using AR_System.Data;
using AR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AR_System.Controllers.Admin
{
    [Authorize(Roles = "Admin")]

    public class FlightController : Controller
    {
        private readonly AirlineReservationSystemContext database;
        public FlightController(AirlineReservationSystemContext database)
        { 
            this.database = database;
        }
        public IActionResult Index()
        {
            ViewBag.flights = database.Flights.ToList();
            return View();
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Flight flight)
        {
            if (flight == null) {
                return NotFound();
            }
            else
            {
                if(ModelState.IsValid)
                {
                    database.Flights.Add(flight);
                    database.SaveChanges();
                    TempData["toastr_success"] = "Flight added successfully";
                    return RedirectToAction("Index");

                }
            }
            return View();
        }
        public IActionResult Edit(int ? id)
        {
            var data = database.Flights.Find(id);
            return View(data);
        }

        public IActionResult Update(Flight flight)
        {
            if(ModelState.IsValid)
            {
                database.Update(flight);
                database.SaveChanges();
                TempData["toastr_success"] = "flight updated successfully";
                return RedirectToAction("Index");
            }
            return View("Edit", flight);
        }

        public IActionResult Delete(int? id)
        {
            var data = database.Flights.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(Flight flight)
        {
            database.Remove(flight);
            database.SaveChanges();
            TempData["toastr_danger"] = "flight deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
