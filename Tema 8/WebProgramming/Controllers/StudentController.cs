using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebProgramming.Data;

namespace WebProgramming.Controllers
{
    [Authorize]

    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value != "Student")
            {
                return NotFound();
            }

            var id = Int32.Parse(User.FindFirst("Sid")?.Value);

            var student = await _context.Students
                .Include(st => st.Grades)
                .FirstOrDefaultAsync(st => st.Sid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public string Test1(string param1 = "hello", int param2 = 0)
        {
            return "Result: " + param1 + param2.ToString();
        }
    }
}
