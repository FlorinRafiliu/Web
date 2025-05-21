using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebProgramming.Data;

namespace WebProgramming.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            // Replace with your user validation logic
            var student = _context.Students.FirstOrDefault(st => st.Username == username && st.Password == password);
            var teacher = _context.Teachers.FirstOrDefault(th => th.Username == username && th.Password == password);

            if (student == null && teacher == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                };

            if (student != null)
            {
                claims.Add(new Claim("Role", "Student"));
                claims.Add(new Claim("Sid", student.Sid.ToString()));
            }
            else if (teacher != null)
            {
                claims.Add(new Claim("Role", "Teacher"));
                claims.Add(new Claim("Tid", teacher.Tid.ToString()));
            }

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            if (student != null)
            {
                return RedirectToAction("Index", "Student"); // You can customize this
            }
            else
            {
                return RedirectToAction("Index", "Teacher"); // You can customize this

            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index");
        }
    }
}
