using System.ComponentModel.DataAnnotations;

namespace webAPI1.Models
{
    public class StudentCourses { 

           [Key]
        public Students? Student { get; set; }
        public Guid? StudentId { get; set; }
        public Courses? Courses { get; set; }
        public Guid? CoursesId { get; set; }
    }
}
