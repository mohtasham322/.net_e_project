using AR_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AR_System.Controllers.Admin
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AirlineReservationSystemContext _database;

        public AdminController(AirlineReservationSystemContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            ViewBag.scheduled_flights = _database.FlightSchedules.Count();
            ViewBag.cityways = _database.CityWays.Count();
            ViewBag.flights = _database.Flights.Count();
            ViewBag.bookings = _database.Bookings.Count();
            ViewBag.users = _database.Users.Count();
            return View();
        }
    }

}
