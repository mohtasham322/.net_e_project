using AR_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AR_System.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly AirlineReservationSystemContext database;

        public CustomerController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }
        public IActionResult Index()
        {
            ViewBag.customers = database.Users.Include(fu => fu.Role).ToList();
            return View();
        }
    }
}
