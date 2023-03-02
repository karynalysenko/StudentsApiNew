using Microsoft.EntityFrameworkCore;
using StudentsNew.Data;
using StudentsNew.Services.StudentService;

namespace StudentsNew.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;

        public StudentService(DataContext context) 
        {
            _context = context;
        }  

        private static List<Student> newStudents = new List<Student>
        {
            new Student(),
            new Student {StudentId = 1, FirstName = "Hélder"}
        };

        //public async Task<List<Student>> AddStudent(Student newStudent)
        //{
        //    newStudents.Add(newStudent);
        //    return newStudents;
        //}

        public async Task<List<Student>> GetAllStudents()
        {
            var dbStudents = await _context.Students.ToListAsync();
            return dbStudents;
        }

        //public async Task<Student> GetStudentById(int id)
        //{
        //    return newStudents.FirstOrDefault(c => c.StudentId == id);
        //}
    }
}
