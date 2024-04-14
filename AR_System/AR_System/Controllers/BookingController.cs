using AR_System.Data;
using AR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AR_System.Controllers
{
    public class BookingController : Controller
    {
        private readonly AirlineReservationSystemContext database;

        public BookingController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Book_Flight(int? id)
        {
            if (id == null)
            {
                // Handle the case when id is null
                return NotFound();
            }

            var ticketClasses = database.TicketClasses.Where(tc => tc.ScheduleId == id).ToList();

            ViewBag.ticketclasses = ticketClasses;
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Book_Flight(Booking booking, int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                        if (int.TryParse(userIdClaim.Value, out int userId))
                        {
                        booking.UserId = userId;
                        database.Bookings.Add(booking);
                        database.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }



    }
}
