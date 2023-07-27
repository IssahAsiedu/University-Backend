using Microsoft.AspNetCore.Mvc;
using UniversityRestApi.Dto;
using UniversityRestApi.Services;

namespace UniversityRestApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CoursesController: ControllerBase
{
    private readonly CoursesService service;


    public CoursesController(CoursesService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CourseDto courseDto)
    {
        var course = await service.CreateCourse(courseDto);
        return CreatedAtAction(nameof(GetCourseById),new { course.ID }, course);
    }

    [HttpGet, Route("/{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        throw new NotImplementedException();
    }
}