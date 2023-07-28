using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityRestApi.Data;
using UniversityRestApi.Dto;
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
        student.ID = Guid.NewGuid();
        await repository.Create(student);
        return mapper.Map<StudentRegistrationResponseData>(student);
    }

    public async Task<StudentResponseData> GetStudentByID(Guid id)
    {
        Func<IQueryable<Student>, IQueryable<Student>> func = query => query.Where(x => x.ID == id);

        func += (query) => query.Include((e) => e.Enrollments).ThenInclude((e) => e.Course).AsNoTracking();
        
        var student = await repository.FilterForFirst(func);

        return mapper.Map<StudentResponseData>(student);

    }
}