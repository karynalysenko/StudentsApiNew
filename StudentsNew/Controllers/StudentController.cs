using Microsoft.AspNetCore.Mvc;
using StudentsNew.Dtos.Student;
using StudentsNew.Services.StudentService;

namespace StudentsNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetStudentDto>>> Get()
        {
            return Ok(await _studentService.GetAllStudents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStudentDto>> GetSingle(int id)
        {
            return Ok(await _studentService.GetStudentById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> AddStudent(AddStudentDto newStudent)
        {
            return Ok(await _studentService.AddStudent(newStudent));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> Delete(int id)
        {
            var response = await _studentService.DeleteStudent(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> UpdateStudent(UpdateStudentDto updatedStudent)
        {
            var response = await _studentService.UpdateStudent(updatedStudent);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
