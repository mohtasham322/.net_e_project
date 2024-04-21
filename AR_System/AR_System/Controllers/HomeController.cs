using AR_System.Data;
using AR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Diagnostics;
using System.Security.Claims;

namespace AR_System.Controllers
{
    public class HomeController : Controller
    {

        private readonly AirlineReservationSystemContext database;

        public HomeController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Flight_Booking()
        {
            var userEmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var userEmail = userEmailClaim?.Value;

            if (userEmail == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var bookingsForCurrentUser = database.Bookings
                .Where(b => b.UserEmail == userEmail)
                .Include(b => b.Schedule)
                    .ThenInclude(s => s.FromCity)
                .Include(b => b.Schedule)
                    .ThenInclude(s => s.ToCity)
                .ToList();

            ViewBag.bookings = bookingsForCurrentUser;
            return View();
        }

        public IActionResult Flights()
        {
            ViewBag.flights = database.FlightSchedules.Include(fs => fs.FromCity).Include(fs => fs.ToCity).ToList();
            ViewBag.ticketClass = database.TicketClasses.ToList();
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
