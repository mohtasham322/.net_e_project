using Microsoft.AspNetCore.Mvc;
using AR_System.Data;
using AR_System.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;




namespace AR_System.Controllers
{
    public class AuthController : Controller
    {
        public readonly AirlineReservationSystemContext database;

        public AuthController(AirlineReservationSystemContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                database.Users.Add(user);
                database.SaveChanges();
                TempData["toastr_success"] = "your account has been created successfully !";
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var result = database.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (result != null)
            {
                var firstName = result.FirstName;

                if (result.RoleId == 2)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                       new Claim(ClaimTypes.Email, user.Email),
                       new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                       new Claim(ClaimTypes.Role, "Admin"),
                       new Claim(ClaimTypes.Name, firstName)
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Name, firstName)
    },
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }

                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    if (result.RoleId == 1)
                    {
                        TempData["toastr_success"] = "You are login successfully !";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["toastr_success"] = "You are login successfully !";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData["error"] = "email or password is invalid !";
            return View("Login");
        }

        public IActionResult Logout()
        {
            var logout = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["toastr_success"] = "log out successfully !";
            return RedirectToAction("Index", "Home");
        }
    }

}
