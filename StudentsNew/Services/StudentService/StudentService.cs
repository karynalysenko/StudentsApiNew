using Microsoft.EntityFrameworkCore;
using StudentsNew.Data;
using StudentsNew.Models;
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
            //new Student {StudentId = 1, FirstName = "Hélder"}
        };

        public async Task<ServiceResponse<List<Student>>> AddStudent(Student newStudent)
        {
            var response = new ServiceResponse<List<Student>>();
            var course = await _context.Courses.FindAsync(newStudent.CourseId);
            
            if (course == null)
            {
                response.Success = false;
                response.Message = $"Course with ID {newStudent.CourseId} not found";
                return response;
            }
            var university = await _context.Universitys.FindAsync(newStudent.UniversityId);
            if (university == null)
            {
                response.Success = false;
                response.Message = $"University with ID {newStudent.UniversityId} not found";
                return response;
            }

            try
            {
                newStudent.Course = null;
                newStudent.University = null;

                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();

                var student = await _context.Students
                    .Include(s => s.Course)
                    .Include(s => s.University)
                    .FirstOrDefaultAsync(s => s.StudentId == newStudent.StudentId);

                if (student != null)
                {
                    student.Course = course;
                    student.University = university;
                    await _context.SaveChangesAsync();
                }

                response.Success = true;
                response.Data = await _context.Students
                    .Include(s => s.Course)
                    .Include(s => s.University)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Student>>> GetAllStudents()
        {
            var response = new ServiceResponse<List<Student>>();
            var dbStudents = await _context.Students
                .Include(s => s.University)
                .Include(s => s.Course)
                .ToListAsync();
            
            if (dbStudents.Count == 0)
            {
                response.Message = "No Student found.";
            }
            response.Data = dbStudents;
            return response;
        }

        public async Task<ServiceResponse<Student>> GetStudentById(int id)
        {
            var response = new ServiceResponse<Student>();
            var dbStudents = await _context.Students
                .Include(s => s.University)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(c => c.StudentId == id);
            if (dbStudents == null)
            {
                response.Success = false;
                response.Message = "No Student found.";
            }
            response.Data = dbStudents;
            return response;
        }
        public async Task<ServiceResponse<Student>> DeleteStudent(int id)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                var dbStudents = await _context.Students
                    .Include(s => s.University)
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync(c => c.StudentId == id);
                if (dbStudents != null)
                {
                    _context.Students.Remove(dbStudents);
                    await _context.SaveChangesAsync();
                    response.Data = dbStudents;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Student not found";
                    return response;
                }
            } 
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Student>> UpdateStudent(Student updatedStudent)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                var dbStudent = await _context.Students
                    .Include(s => s.University)
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync(c => c.StudentId == updatedStudent.StudentId);
                if (dbStudent != null)
                {
                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == updatedStudent.CourseId);
                    var university = await _context.Universitys.FirstOrDefaultAsync(u => u.Id == updatedStudent.UniversityId);

                    dbStudent.FirstName = updatedStudent.FirstName;
                    dbStudent.LastName = updatedStudent.LastName;
                    dbStudent.Phone = updatedStudent.Phone;
                    dbStudent.Email = updatedStudent.Email;
                    dbStudent.Genre = updatedStudent.Genre;
                    dbStudent.CourseId = updatedStudent.CourseId;
                    dbStudent.UniversityId = updatedStudent.UniversityId;
                    await _context.SaveChangesAsync();
                    response.Data = dbStudent;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Student not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
