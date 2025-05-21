using Microsoft.AspNetCore.Mvc.Rendering;
using WebProgramming.Models;

namespace WebProgramming.ViewModels
{
    public class TeacherViewModel
    {
        public string Username { get; set; }
        public List<Student>? students { get; set; }
        public List<Grade>? grades { get; set; }
        public List<SelectListItem> groups { get; set; }
        public int? selectedGroup { get; set; } = null;
        public int? studentId { get; set; } = null;

        public int currentPage { get; set; } = 1;
        public int totalPages { get; set; }

        public int pageSize { get; set; } = 4;
    }
}
