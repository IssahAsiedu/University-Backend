namespace UniversityRestApi.Models;


public class Course
{
    public int ID { get; set; }

    public required string Title { get; set; }

    public required int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public override int GetHashCode() => ID.GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj != null && (obj is Course course) && course.ID == ID;
    }

}

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    public int ID { get; set; }

    public int CourseID { get; set; }

    public int StudentID { get; set; }

    public Grade? Grade { get; set; }

    public Course Course { get; set; }

    public Student Student { get; set; }

    public override int GetHashCode() => ID.GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj != null && (obj is Enrollment enrollment) && enrollment.ID == ID;
    }
}

public class Student
{
    public int ID { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTimeOffset EnrollmentDate { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public override int GetHashCode() => ID.GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj != null && (obj is Student student) && student.ID == ID;
    }

}
