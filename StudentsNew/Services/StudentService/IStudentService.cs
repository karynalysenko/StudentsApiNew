using StudentsNew.Dtos.Student;

namespace StudentsNew.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<GetStudentDto>>> GetAllStudents();
        Task<ServiceResponse<GetStudentDto>> GetStudentById(int id);
        Task<ServiceResponse<List<GetStudentDto>>> AddStudent(AddStudentDto newStudent);
        Task<ServiceResponse<GetStudentDto>> DeleteStudent(int id);
        Task<ServiceResponse<GetStudentDto>> UpdateStudent(UpdateStudentDto updatedStudent);
    }
}
