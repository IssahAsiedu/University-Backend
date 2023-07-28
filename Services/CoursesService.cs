using AutoMapper;
using UniversityRestApi.Data;
using UniversityRestApi.Dto;
using UniversityRestApi.Models;

namespace UniversityRestApi.Services;

public class CoursesService
{
    private readonly IMapper mapper;

    private readonly Repository<Course> coursesRepo;

    public CoursesService(IMapper mapper, Repository<Course> coursesRepo)
    {
        this.mapper = mapper;
        this.coursesRepo = coursesRepo;
    }

    public async Task<CourseResponseData> CreateCourse(CourseCreationData dto)
    {
        var course = mapper.Map<Course>(dto);
        course.ID = Guid.NewGuid();
        await coursesRepo.Create(course);
        return mapper.Map<CourseResponseData>(course);
    }

    public async Task<CourseResponseData> GetCourseById(Guid id)
    {
        var course = await coursesRepo.FilterForFirst((query) => query.Where(c => c.ID == id));

        return mapper.Map<CourseResponseData>(course);
    }
}
