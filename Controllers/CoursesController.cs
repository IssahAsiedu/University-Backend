using Microsoft.AspNetCore.Mvc;
using UniversityDto;
using UniversityRestApi.Services;

namespace UniversityRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController: ControllerBase
{
    private readonly CoursesService service;

    public CoursesController(CoursesService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CourseCreationData courseDto)
    {
        var course = await service.CreateCourse(courseDto);
        return CreatedAtAction(nameof(GetCourseById),new { course.ID }, course);
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses([FromQuery] PaginationFilter filter)
    {
        CoursePaginationData response = await service.GetCourses(filter);
        return Ok(response);
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetCourseById(Guid id)
    {
       var course = await service.GetCourseById(id);
        return Ok(course);
    }

    [HttpPut, Route("{id}")]
    public async Task<IActionResult> UpdateCourse(Guid id, CourseUpdateDto dto)
    {
        await service.UpdateCourse(id, dto);
        return NoContent();
    }

    [HttpDelete,  Route("{id}")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        await service.Delete(id);
        return Ok();
    }
}