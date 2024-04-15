using AR_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AR_System.Controllers
{
    public class FlightSearchController : Controller
    {
        private readonly AirlineReservationSystemContext database;

        public FlightSearchController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Search(string from_city , string to_city)
        {
            var searched_flights = database.FlightSchedules
            .Include(fs => fs.FromCity)
            .Include(fs => fs.ToCity)
            .Where(fs => fs.FromCity.CityName == from_city && fs.ToCity.CityName == to_city)
            .ToList();

            ViewBag.searched_flights = searched_flights;
            return View("Search_Result");
        }
    }
}
