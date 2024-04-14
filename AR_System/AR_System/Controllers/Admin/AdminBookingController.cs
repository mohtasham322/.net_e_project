using AR_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.bookings = database.Bookings.Include(fs => fs.User).Include(fs => fs.Schedule).ThenInclude(schedule => schedule.FromCity).Include(fs => fs.Schedule)
        .ThenInclude(schedule => schedule.ToCity).ToList();
            return View();
        }
    }
}
