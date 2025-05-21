using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramming.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Gid { get; set; }
        [ForeignKey("student")]
        public int Sid { get; set; }
        [Required]
        public string Course { get; set; }
        [Required]
        public decimal GradeValue { get; set; }

        public Student student { get; set; }
    }
}
