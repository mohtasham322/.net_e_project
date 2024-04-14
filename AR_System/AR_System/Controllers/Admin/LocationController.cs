using AR_System.Data;
using AR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AR_System.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly AirlineReservationSystemContext database;

        public LocationController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            ViewBag.locations = database.CityWays.ToList();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CityWay cityway)
        {
            if (ModelState.IsValid)
            {
                database.CityWays.Add(cityway);
                database.SaveChanges();
                TempData["toastr_success"] = "new location has been added !";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");


        }
        public IActionResult Edit(int id)
        {
            var data = database.CityWays.SingleOrDefault(fs => fs.Id == id);
            return View(data);
        }
        public IActionResult Update(CityWay cityway)
        {
            if(ModelState.IsValid) { 
            database.Update(cityway);
            database.SaveChanges();
            TempData["toastr_success"] = "location updated successfully";
            return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        public IActionResult Delete(int? id)
        {
            var data = database.CityWays.SingleOrDefault(fs => fs.Id == id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(CityWay cityway)
        {
            database.Remove(cityway);
            database.SaveChanges();
            TempData["toastr_danger"] = "location deleted !";
            return RedirectToAction("Index");
        }
    }
}
