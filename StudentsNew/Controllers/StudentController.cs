using Microsoft.AspNetCore.Mvc;
using StudentsNew.Services.StudentService;

namespace StudentsNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) {
            _studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Student>>> Get() 
        {
            return Ok(await _studentService.GetAllStudents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetSingle(int id)
        {
           return Ok(await _studentService.GetStudentById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student newStudent)
        {
            return Ok(await _studentService.AddStudent(newStudent));
        }
    }
}
