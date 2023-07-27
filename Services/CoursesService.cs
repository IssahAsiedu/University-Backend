using AutoMapper;
using UniversityRestApi.Data;
using UniversityRestApi.Dto;
using UniversityRestApi.Models;

namespace UniversityRestApi.Services;

public class CoursesService
{
    private readonly IMapper mapper;

    private readonly Repository<Course, Guid> coursesRepo;

    public CoursesService(IMapper mapper, Repository<Course, Guid> coursesRepo)
    {
        this.mapper = mapper;
        this.coursesRepo = coursesRepo;
    }

    public async Task<CourseCreatedDto> CreateCourse(CourseDto dto)
    {
        var course = mapper.Map<Course>(dto);
        course.ID = Guid.NewGuid();
        await coursesRepo.Create(course);
        return mapper.Map<CourseCreatedDto>(course);
    }
}
