using Microsoft.EntityFrameworkCore;

namespace StudentsNew.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universitys { get; set; }
        public DbSet<Course> Courses { get; set; }


    }
}
