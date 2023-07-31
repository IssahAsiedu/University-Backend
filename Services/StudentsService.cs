using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityRestApi.Data;
using UniversityRestApi.Dto;
using UniversityRestApi.Exceptions;
using UniversityRestApi.Models;

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
            throwErrorForNullStudent(id);
        }

        return mapper.Map<StudentResponseData>(student);
    }

    private static void throwErrorForNullStudent(Guid id)
    {
        var exeption = new UniversityException(StatusCodes.Status400BadRequest);
        exeption.Payload = new Dictionary<string, string>() { { "message", $"student with id {id} not present" } };
        throw exeption;
    }

    public async Task UpdateStudent(Guid id, StudentUpdateData data)
    {
        var student = await repository.FilterForFirst((q) => q.Where(x => x.ID == id));

        if (student == null)
        {
            throwErrorForNullStudent(id);
        }

        mapper.Map(data, student);

        await repository.Save();
    }

    public async Task<object> GetStudents(PaginationFilter filter)
    {
        var paginatedData = await repository.GetAll(filter, AddNecessaryFieldsInQuery);

        List<Student> students = mapper.Map<List<Student>>(paginatedData.Items);

        return new { paginatedData.CurrentIndex, paginatedData.TotalItems, students };
    }

    private static IQueryable<Student> AddNecessaryFieldsInQuery(IQueryable<Student> query)
    {
        return query.Include((e) => e.Enrollments)
                .ThenInclude((e) => e.Course)
                .OrderBy((s) => s.ID)
                .AsNoTracking(); 
    }

    public async Task DeleteStudent(Guid id)
    {
        var student = await repository.FilterForFirst((q) => q.Where(x => x.ID == id));

        if (student == null)
        {
            throwErrorForNullStudent(id);
        }

        await repository.Delete(student!);
    }


}