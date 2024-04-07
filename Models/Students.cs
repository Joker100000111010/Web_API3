using System.ComponentModel.DataAnnotations;

namespace webAPI1.Models
{
    public class Students
    {
        [Key]
        public Guid? StudentId { get; set; }
        [Required(ErrorMessage = " Tên học sinh không được trống.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Tên học sinh có từ 5 đến 50 ký tự.")]
        public string? Name { get; set; }
        public List<StudentCourses>? StudentCourses { get; set; }
    }
}
