using System.ComponentModel.DataAnnotations;

namespace UniversityRestApi.Dto;

public record CourseCreationData(string Title, int Credits);

public record CourseResponseData(string ID, string Title, string Credits);

public record CourseUpdateData(string? Title, int? Credits, int ID)
{
    public bool UpdateProvided()
    {
        return Title != null || Credits != null;
    }
}


public record StudentRegistrationData(
    [Required, MinLength(1)]
    string FirstName,
    [Required, MinLength(1)]
    string LastName,
    [Required, RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    string EnrollmentDate
);

public record StudentUpdateData(
    [MinLength(1)]
    string? FirstName,
    [MinLength(1)]
    string? LastName,
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    string? EnrollmentDate
)
{
    public bool UpdateProvided()
    {
        return FirstName != null || LastName != null || EnrollmentDate != null;
    }
}

public record StudentRegistrationResponseData(
    string ID,
    string FirstName,
    string LastName,
    string EnrollmentDate
);

public record StudentResponseData(
    string ID,
    string FirstName,
    string LastName,
    string EnrollmentDate,
    ICollection<EnrollmentResponseData> Enrollments
);

public record EnrollmentCreationData(
    string CourseID,
    string StudentID,
    string? Grade
);


public record EnrollmentResponseData(
    string ID,
    string? Grade,
    CourseResponseData? Course
);


public record PaginationFilter(int CurrentIndex = 0, int PageSize = 10);