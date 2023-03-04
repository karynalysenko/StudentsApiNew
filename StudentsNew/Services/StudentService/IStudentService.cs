﻿namespace StudentsNew.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<Student>>> GetAllStudents();
        Task<ServiceResponse<Student>> GetStudentById(int id);
        Task<ServiceResponse<List<Student>>> AddStudent(Student newStudent);
        Task<ServiceResponse<Student>> DeleteStudent(int id);
        Task<ServiceResponse<Student>> UpdateStudent(Student updatedStudent);
    }
}
