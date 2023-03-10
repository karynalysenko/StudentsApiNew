namespace StudentsNew.Dtos.Student
{
    public class UpdateStudentDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = "Nome";
        public string LastName { get; set; } = "Apelido";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Genre Genre { get; set; }

        public int UniversityId { get; set; }
        //public University? University { get; set; }
        public int CourseId { get; set; }
        //public Course? Course { get; set; }
    }
}
