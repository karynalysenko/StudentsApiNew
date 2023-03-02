namespace StudentsNew.Services.StudentService
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        //Task<Student> GetStudentById(int id);
        //List<Student> AddStudents( Student newStudent);
    }
}
