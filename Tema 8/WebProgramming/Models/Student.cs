using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramming.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sid { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int GroupName { get; set; }
        [Required]
        public string Password { get; set; }

        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
