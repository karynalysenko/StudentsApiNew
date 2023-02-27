using Microsoft.AspNetCore.Mvc;

namespace StudentsNew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private static List<Student> newStudents = new List<Student>
        {
            new Student(),
            new Student {StudentId = 1, FirstName = "Hélder"}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Student>> Get() 
        {
            return Ok(newStudents);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetSingle(int id)
        {
            return Ok(newStudents.FirstOrDefault(c => c.StudentId == id));
        }

    }
}
