using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramming.Data;
using WebProgramming.Models;
using WebProgramming.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace WebProgramming.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? selectedGroup, int? studentId, int? pageSize, int page = 1)
        {
            if (User.FindFirst("Role").Value != "Teacher")
            {
                return NotFound();
            }

            var id = Int32.Parse(User.FindFirst("Tid")?.Value);

            Teacher? teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Tid == id);

            if (teacher == null)
                return NotFound();

            List <Student>? students = null;
            List <Grade> grades = null;
            int totalPages = 1;

            if (pageSize == null)
                pageSize = 4;

            if (selectedGroup.HasValue)
            {
                students = _context.Students
                            .Where(st => st.GroupName == selectedGroup)
                            .OrderBy(st => st.Sid)
                            .Skip((page - 1) * pageSize.Value)
                            .Take(pageSize.Value)
                            .ToList();
                totalPages = (int) Math.Ceiling(
                    (double)_context.Students.Count(s => s.GroupName == selectedGroup) / pageSize.Value
                );
            }

            if (studentId.HasValue)
            {
                grades = _context.Grades.Where(g => g.Sid == studentId).ToList();
            }

            List <int> groups = _context.Students.Select(st => st.GroupName).Distinct().ToList();

            var g = new List<SelectListItem>();
            foreach (int group in groups)
            {
                g.Add(new SelectListItem { Text = group.ToString(), Value = group.ToString() });
            }

            return View(new TeacherViewModel
            {
                Username = teacher.Username,
                students = students,
                groups = g,
                selectedGroup = selectedGroup,
                grades = grades,
                totalPages = totalPages,
                currentPage = page,
                pageSize = pageSize.Value
            });
        }
    }
}
