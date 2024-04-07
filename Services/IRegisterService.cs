using webAPI1.Models;

namespace webAPI1.Services
{
        public interface IRegisterService
        {
        // Khoa Hoc
        Task<List<Courses>> GetAllCourses();
        Task<Courses> GetIdCourses(Guid id, bool includeCourses = false);
        Task<Courses> AddCourses(Courses courses);
        Task<Courses> UpdateCourses(Courses courses);
        Task<(bool, string)> DeleteCourses(Courses courses);

        // Học Sinh

        Task<List<Students>> getAllStudent(string filter, string sortBy, int page, int pageSize);
        Task<Students> GetStudentsAsync(Guid id, bool includeBooks = false); // GET Single Author
            Task<Students> AddStudentsAsync(Students students); // POST New Author
            Task<Students> UpdateStudentsAsync(Students students); // PUT Author
            Task<(bool, string)> DeleteStudentsAsync(Students students); // DELETE Author

        //Đăng Ký
        Task<List<StudentCourses>> GetAllsc();
        Task<StudentCourses> GetIdsc(Guid id);
        Task<StudentCourses> Addsc(StudentCourses sc);
        Task<StudentCourses> Updatesc(StudentCourses sc);
        Task<(bool, string)> Deletesc(StudentCourses sc);
    }
}

