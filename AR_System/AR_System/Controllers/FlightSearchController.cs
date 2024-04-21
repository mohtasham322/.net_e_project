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
        public IActionResult Search(string from_city, string to_city)
        {
            var allFlights = database.FlightSchedules
                .Include(fs => fs.FromCity)
                .Include(fs => fs.ToCity)
                .ToList();

            var searched_flights = allFlights.Where(fs =>
                fs.FromCity.CityName.Equals(from_city, StringComparison.OrdinalIgnoreCase) &&
                fs.ToCity.CityName.Equals(to_city, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (searched_flights.Count == 0)
            {
                searched_flights = allFlights.Where(fs =>
                    fs.FromCity.CityName.Equals(from_city, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            if (searched_flights.Count == 0)
            {
                searched_flights = allFlights.Where(fs =>
                    fs.ToCity.CityName.Equals(to_city, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            ViewBag.searched_flights = searched_flights;
            ViewBag.ticketClass = database.TicketClasses.ToList();
            return View("Search_Result");
        }




    }
}
