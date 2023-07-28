namespace UniversityRestApi.Models;

public class Course
{
    public Guid ID { get; set; }

    public required string Title { get; set; }

    public required int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    public Guid ID { get; set; }

    public Guid CourseID { get; set; }

    public Guid StudentID { get; set; }

    public Grade? Grade { get; set; }

    public Course? Course { get; set; }

    public Student? Student { get; set; }
}

public class Student
{
    public Guid ID { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTimeOffset EnrollmentDate { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
