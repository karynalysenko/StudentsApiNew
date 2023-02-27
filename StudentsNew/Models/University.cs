using System.ComponentModel.DataAnnotations;

namespace StudentsNew.Models
{
    public class University
    {
        //[Key]
        public int Id { get; set; } = 0;
        public string? UniversityName { get; set; } = "IPCA";

        public List<Student> Students { get; set; }
    }
}
