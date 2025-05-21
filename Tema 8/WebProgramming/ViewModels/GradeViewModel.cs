using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramming.ViewModels
{
    public class GradeViewModel
    {
        [Required]
        public string Course { get; set; }

        [Required]
        public decimal GradeValue { get; set; }

        public int Sid { get; set; }

        public int Gid { get; set; }
    }

}
