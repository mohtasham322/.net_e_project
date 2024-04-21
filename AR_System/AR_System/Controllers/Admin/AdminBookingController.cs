using AR_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AR_System.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminBookingController : Controller
    {
        private readonly AirlineReservationSystemContext database;

        public AdminBookingController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {

            var bookingsForCurrentUser = database.Bookings
            .Include(b => b.Schedule)
            .ThenInclude(s => s.FromCity)
            .Include(b => b.Schedule)
            .ThenInclude(s => s.ToCity)
            .ToList();

            ViewBag.bookings = bookingsForCurrentUser;
            return View();
        }
    }
}