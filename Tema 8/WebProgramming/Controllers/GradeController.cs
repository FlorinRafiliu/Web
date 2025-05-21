using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramming.Data;
using WebProgramming.Models;
using WebProgramming.ViewModels;

namespace WebProgramming.Controllers
{
    public class GradeController : Controller
    {
        private readonly AppDbContext _context;

        public GradeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Grade/Create/1
        public IActionResult Create(int? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.StudentId = id;
            return View();
        }

        // GET: /Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var grade = await _context.Grades.FindAsync(id);

            if (grade == null)
                return NotFound();

            return View(new GradeViewModel
            {
                Course = grade.Course,
                Sid = grade.Sid,
                GradeValue = grade.GradeValue,
                Gid = grade.Gid,
            });
        }

        // POST: /Grade/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GradeViewModel grade)
        {
            if (id != grade.Gid)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var db_grade = await _context.Grades.FirstOrDefaultAsync(g => g.Gid == id);
                   
                    db_grade.GradeValue = grade.GradeValue;
                    db_grade.Course = grade.Course;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Teacher", new { id = 1 });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Grades.Any(e => e.Gid == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(grade);
        }


        // POST: /Grade/Create
        [HttpPost]
        public async Task<IActionResult> Create(GradeViewModel grade)
        {
            if (ModelState.IsValid)
            {
                _context.Grades.Add(new Grade
                {
                    Sid = grade.Sid,
                    Course = grade.Course,
                    GradeValue = grade.GradeValue,
                });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Teacher", new { id = 1 });
            }

            ViewBag.StudentId = grade.Sid;
            return View(grade);
        }


    }
}
