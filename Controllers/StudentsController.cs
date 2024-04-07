using Microsoft.AspNetCore.Mvc;
using webAPI1.Migrations;
using webAPI1.Models;
using webAPI1.Services;

namespace webAPI1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     
    public class StudentsController : ControllerBase
    {
        private readonly IRegisterService _coursesService;
        private readonly ILogger<StudentsController> _logger; // Inject ILogger

        public StudentsController(IRegisterService coursesService, ILogger<StudentsController> logger)
        {
            _coursesService = coursesService;
            _logger = logger; // Inject ILogger
        }
        [HttpGet]
        //Paging:: phân trang thành  page: số trang, pageSize: mục)
        public async Task<IActionResult> getAllStudent(string filter, string sortBy, int page = 1, int pageSize = 10)
        {
            try
            {
                var students = await _coursesService.getAllStudent(filter, sortBy, page, pageSize); // Thêm các tham số filter, sortBy, page và pageSize
                _logger.LogInformation($"Retrieved {students.Count} students from the database."); // Ghi log
                return StatusCode(StatusCodes.Status200OK, students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving students: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving students.");
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetStudentsAsync(Guid id, bool includeCourses = false)
        {
            try
            {
                Students courses = await _coursesService.GetStudentsAsync(id, includeCourses);
                _logger.LogInformation($"Retrieved Name: {courses.Name} students from the database."); 
                if (courses == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {id}");
                }
                return StatusCode(StatusCodes.Status200OK, courses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving student with ID {id}: {ex.Message}"); // Ghi log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving student.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Students>> AddStudentsAsync(Students courses)
        {
            try
            {
                var dbCourses = await _coursesService.AddStudentsAsync(courses);
                if (dbCourses == null)
                {
                    _logger.LogError($"{courses.Name} could not be added.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{courses.Name} could not be added.");
                }

                _logger.LogInformation($"{courses.Name} added successfully.");
                return CreatedAtAction("GetStudents", new { id = courses.StudentId }, courses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding student.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentsAsync(Guid id, Students courses)
        {
            try
            {
                if (id != courses.StudentId)
                {
                    return BadRequest();
                }

                Students dbCourses = await _coursesService.UpdateStudentsAsync(courses);

                if (dbCourses == null)
                {
                    _logger.LogError($"{courses.Name} could not be updated.");
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{courses.Name} could not be updated.");
                }

                _logger.LogInformation($"{courses.Name} updated successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating student: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating student.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentsAsync(Guid id)
        {
            try
            {
                var courses = await _coursesService.GetStudentsAsync(id, false);
                (bool status, string message) = await _coursesService.DeleteStudentsAsync(courses);

                if (status == false)
                {
                    _logger.LogError($"Error deleting student: {message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }

                _logger.LogInformation($"Student with ID {id} deleted successfully.");
                return StatusCode(StatusCodes.Status200OK, courses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting student with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting student.");
            }
        }
    }
}
