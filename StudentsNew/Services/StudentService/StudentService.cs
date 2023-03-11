using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentsNew.Data;
using StudentsNew.Dtos.Student;
using StudentsNew.Models;
using StudentsNew.Services.StudentService;

namespace StudentsNew.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StudentService(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }  

        private static List<Student> newStudents = new List<Student>
        {
            new Student()
        };

        public async Task<ServiceResponse<List<GetStudentDto>>> AddStudent(AddStudentDto newStudent)
        {
            var response = new ServiceResponse<List<GetStudentDto>>();
            var course = await _context.Courses
                .FindAsync(newStudent.CourseId);
            var university = await _context.Universitys.FindAsync(newStudent.UniversityId);
            
            if (course == null)
            {
                response.Success = false;
                response.Message = $"Course with ID {newStudent.CourseId} not found";
                return response;
            }
            if (university == null)
            {
                response.Success = false;
                response.Message = $"University with ID {newStudent.UniversityId} not found";
                return response;
            }
            if (Enum.IsDefined(typeof(Genre), newStudent.Genre) == false)
            {
                response.Success = false;
                response.Message = $"Genre '{newStudent.Genre}' don't exist";
                return response;
            }
            try
            {
                //newStudent.Course = null;
                //newStudent.University = null;

                _context.Students.Add(_mapper.Map<Student>(newStudent));
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
                    .Select(s => _mapper.Map<GetStudentDto>(s))
                    .ToListAsync(); 
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents()
        {
            var response = new ServiceResponse<List<GetStudentDto>>();
            var dbStudents = await _context.Students
                .Include(s => s.University)
                .Include(s => s.Course)
                .ToListAsync();
            
            if (dbStudents.Count == 0)
            {
                response.Message = "No Student found.";
            }
            response.Data = dbStudents.Select(s => _mapper.Map<GetStudentDto>(s)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetStudentById(int id)
        {
            var response = new ServiceResponse<GetStudentDto>();
            var dbStudent = await _context.Students
                .Include(s => s.University)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(c => c.StudentId == id);
            if (dbStudent == null)
            {
                response.Success = false;
                response.Message = "No Student found.";
            }
            response.Data = _mapper.Map<GetStudentDto>(dbStudent);
            return response;
        }
        public async Task<ServiceResponse<GetStudentDto>> DeleteStudent(int id)
        {
            var response = new ServiceResponse<GetStudentDto>();
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
                    response.Data = _mapper.Map<GetStudentDto>(dbStudents);
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

        public async Task<ServiceResponse<GetStudentDto>> UpdateStudent(UpdateStudentDto updatedStudent)
        {
            var response = new ServiceResponse<GetStudentDto>();
            try
            {
                var dbStudent = await _context.Students
                    .Include(s => s.University)
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync(c => c.StudentId == updatedStudent.StudentId);
                if (dbStudent != null)
                {
                    var course = await _context.Courses
                        .FirstOrDefaultAsync(c => c.CourseId == updatedStudent.CourseId);
                    var university = await _context.Universitys
                        .FirstOrDefaultAsync(u => u.Id == updatedStudent.UniversityId);

                    dbStudent.FirstName = updatedStudent.FirstName;
                    dbStudent.LastName = updatedStudent.LastName;
                    dbStudent.Phone = updatedStudent.Phone;
                    dbStudent.Email = updatedStudent.Email;
                    if (Enum.IsDefined(typeof(Genre), updatedStudent.Genre) == false)
                    {
                        response.Success = false;
                        response.Message = $"Genre '{updatedStudent.Genre}' don't exist";
                        return response;
                    }
                    dbStudent.Genre = updatedStudent.Genre;
                    dbStudent.CourseId = updatedStudent.CourseId;
                    dbStudent.UniversityId = updatedStudent.UniversityId;
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetStudentDto>(dbStudent);
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
