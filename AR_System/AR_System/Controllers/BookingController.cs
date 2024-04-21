using AR_System.Data;
using AR_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AR_System.Controllers
{
    [Authorize(Roles = "User")]
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

            var ticketClasses = database.TicketClasses.ToList();

            ViewBag.ticketclasses = ticketClasses;
            return View();
        }
        [HttpPost]
        public IActionResult Book_Flight(Booking booking, int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmailClaim = User.FindFirst(ClaimTypes.Email);
                if (userEmailClaim != null)
                {
                    string userEmail = userEmailClaim.Value;
                    booking.UserEmail = userEmail;
                    int ticketClass;
                    if (!int.TryParse(booking.TicketClass, out ticketClass))
                    {
                        ticketClass = 0; 
                    }
                    int? noOfTickets = booking.NoOfTickets ?? 0; 

                    booking.TotalPrice = ticketClass * noOfTickets;

                    booking.ScheduleId = id;
                    database.Bookings.Add(booking);
                    database.SaveChanges();
                    TempData["toastr_success"] = "Your flight has been booked successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Booking failed !";
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return View();
            }
        }






    }
}
