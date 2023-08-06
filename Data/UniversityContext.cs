using Microsoft.EntityFrameworkCore;
using UniversityShared.Models;

namespace UniversityRestApi.Data;

public class UniversityContext : DbContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasMany(c => c.Instructors).WithMany(i => i.Courses);
        modelBuilder.Entity<Department>().HasOne(d => d.Administrator).WithMany().OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Instructor> Instructors { get; set; }
}