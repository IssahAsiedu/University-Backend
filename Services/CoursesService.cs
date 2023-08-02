using AutoMapper;
using UniversityDto;
using UniversityRestApi.Data;
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
        var course = await coursesRepo.FilterForFirst((query) => query.Where(c => c.ID == id));

        return mapper.Map<CourseResponseData>(course);
    }

    public async Task<CoursePaginationData> GetCourses(PaginationFilter filter)
    {
        var paginatedData = await coursesRepo.GetAll(filter);
        var courses = mapper.Map<List<CourseResponseData>>(paginatedData.Items);
        var response = new CoursePaginationData(courses);
        response.CurrentIndex = paginatedData.CurrentIndex;
        response.Count = paginatedData.TotalItems;
        return response;
    }
}
