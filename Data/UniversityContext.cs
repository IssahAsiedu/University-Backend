using Microsoft.EntityFrameworkCore;
using UniversityRestApi.Models;

namespace UniversityRestApi.Data;

public class UniversityContext:DbContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options): base(options)
    {

    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }
}
