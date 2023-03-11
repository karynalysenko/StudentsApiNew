using AutoMapper;
using StudentsNew.Dtos.Student;

namespace StudentsNew
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, GetStudentDto>();
            CreateMap<AddStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
