using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsNew.Models
{
    public class University
    {
        //[Key]
        public int Id { get; set; }
        public string? UniversityName { get; set; }
        [JsonIgnore]
        public List<Student>? StudentsId { get; set; }
    }
}
