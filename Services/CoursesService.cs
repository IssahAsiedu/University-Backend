using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityDto;
using UniversityRestApi.Data;
using UniversityRestApi.Exceptions;
using UniversityShared.Models;

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
        var course = await coursesRepo.FilterForFirst((query) => query.Include(c => c.Department).Where(c => c.ID == id));

        return mapper.Map<CourseResponseData>(course);
    }

    public async Task<CoursePaginationData> GetCourses(PaginationFilter filter)
    {
        var paginatedData = await coursesRepo.GetAll(filter, (query) => query.Include(c => c.Department));
        var courses = mapper.Map<List<CourseResponseData>>(paginatedData.Items);
        var response = new CoursePaginationData(courses);
        response.CurrentIndex = paginatedData.CurrentIndex;
        response.Count = paginatedData.TotalItems;
        return response;
    }

    public async Task Delete(Guid id)
    {
        var course = await coursesRepo.FilterForFirst((q) => q.Where(x => x.ID == id));

        if(course == null)
        {
            ThrowNotFoundException(id);
        }

        await coursesRepo.Delete(course!);
    }

    private static void ThrowNotFoundException(Guid id)
    {
        Dictionary<string, object> payload = new() {
                {"message", $"student with id <{id}> does not exist"}
            };

        throw new UniversityException(StatusCodes.Status400BadRequest, payload);
    }

    public async Task UpdateCourse(Guid id, CourseUpdateDto dto)
    {
        var course = await coursesRepo.FilterForFirst((q) => q.Where(x => x.ID == id));
        if (course == null)
        {
            ThrowNotFoundException(id);
        }
        mapper.Map(dto, course);
        await coursesRepo.Save();
    }
}
