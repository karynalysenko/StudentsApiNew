using System.Text.Json.Serialization;

namespace StudentsNew.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        [JsonIgnore]
        public List<Student>? StudentsId { get; set; }


    }
}
