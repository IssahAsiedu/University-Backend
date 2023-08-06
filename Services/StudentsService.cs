using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityDto;
using UniversityRestApi.Data;
using UniversityRestApi.Exceptions;
using UniversityShared.Models;

namespace UniversityRestApi.Services;

public class StudentsService
{
    private readonly Repository<Student> repository;
    private readonly IMapper mapper;

    public StudentsService(Repository<Student> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<StudentRegistrationResponseData> RegisterStudent(StudentRegistrationData registrationData)
    {
        var student = mapper.Map<Student>(registrationData);
        Guid.NewGuid();
        await repository.Create(student);
        return mapper.Map<StudentRegistrationResponseData>(student);
    }

    public async Task<StudentResponseData> GetStudentByID(Guid id)
    {

        IQueryable<Student> func(IQueryable<Student> query)
        {
            return AddNecessaryFieldsInQuery(query.Where(x => x.ID == id));
        }

        var student = await repository.FilterForFirst(func);

        if(student == null)
        {
            ThrowErrorForNullStudent(id);
        }

        return mapper.Map<StudentResponseData>(student);
    }

    private static void ThrowErrorForNullStudent(Guid id)
    {
        var exeption = new UniversityException(StatusCodes.Status400BadRequest);
        exeption.Payload = new Dictionary<string, string>() { { "message", $"student with id {id} not present" } };
        throw exeption;
    }

    public async Task UpdateStudent(Guid id, StudentUpdateData data)
    {
        var student = await repository.FilterForFirst((q) => AddNecessaryFieldsInQuery(q.Where(x => x.ID == id)));

        if (student == null)
        {
            ThrowErrorForNullStudent(id);
        }

        mapper.Map(data, student);

        student!.Enrollments.RemoveAll(el => !data.Courses.Contains(el.CourseID.ToString()));
        var unErolledCourses = data.Courses
            .Where(s => !student.Enrollments.Any(el => el.CourseID.ToString() == s))
            .ToList();

        unErolledCourses.ForEach((c) =>
        {
            var en = new Enrollment()
            {
                StudentID = student.ID,
                CourseID = Guid.Parse(c)
            };
            student.Enrollments.Add(en);
        });

        await repository.Save();
    }

    public async Task<object> GetStudents(PaginationFilter filter)
    {
        var paginatedData = await repository.GetAll(filter, AddNecessaryFieldsInQuery);

        List<StudentResponseData> students = mapper.Map<List<Student>, List<StudentResponseData>>(paginatedData.Items);

        var response = new StudentPaginationData(students)
        {
            CurrentIndex = paginatedData.CurrentIndex,
            Count = paginatedData.TotalItems
        };

        return response;
    }

    private static IQueryable<Student> AddNecessaryFieldsInQuery(IQueryable<Student> query)
    {
        return query.Include((e) => e.Enrollments)
                .ThenInclude((e) => e.Course)
                .OrderBy((s) => s.ID); 
    }

    public async Task DeleteStudent(Guid id)
    {
        var student = await repository.FilterForFirst((q) => q.Where(x => x.ID == id));

        if (student == null)
        {
            ThrowErrorForNullStudent(id);
        }

        await repository.Delete(student!);
    }

    public async Task GradeStudent(StudentGradingData data)
    {
        var student = await repository.FilterForFirst((q) => AddNecessaryFieldsInQuery(q).Where(x => x.ID == data.StudentID));
        if (student == null)
        {
            ThrowErrorForNullStudent(data.StudentID);
        }

        var enrollment = student!.Enrollments.FirstOrDefault((en) => en.CourseID == data.CourseID);

        
        if(enrollment == null)
        {
            var exception = new UniversityException(StatusCodes.Status400BadRequest);
            var payload = new Dictionary<string, object>() { {"message", "Student is not enrolled in that course." } };
            exception.Payload = payload;
            throw exception;
        }

        var graded = Enum.TryParse<Grade>(data.Grade, out Grade gradeEnum);

        if(!graded)
        {
            var exception = new UniversityException(StatusCodes.Status400BadRequest);
            var payload = new Dictionary<string, object>() { { "message", "The grades available are A, B, C, D, F." } };
            exception.Payload = payload;
            throw exception;
        }

        enrollment.Grade = gradeEnum;

        await repository.Save();
    }
}