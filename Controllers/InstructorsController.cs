using Microsoft.AspNetCore.Mvc;
using UniversityDto;
using UniversityRestApi.Services;

namespace UniversityRestApi.Controllers;

[ApiController, Route("/[controller]")]
public class InstructorsController: ControllerBase
{
    private readonly InstructorsService service;

    public InstructorsController(InstructorsService service)
    {
        this.service = service;
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetInstructor(Guid id)
    {
        InstructorDto dto = await service.GetInstructor(id);
        return Ok(dto);
    }

    [HttpPut, Route("{id}")]
    public async Task<IActionResult> UpdateInstructor(Guid id, InstructorUpdateData data)
    {
        await service.UpdateInstructor(id, data);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterInstructor(InstructorRegistrationData data)
    {
        InstructorDto instructor  = await service.Register(data);
        return CreatedAtAction(nameof(GetInstructor), new {instructor.ID}, instructor);
    }


    [HttpGet]
    public async Task<IActionResult> GetInstructors([FromQuery] PaginationFilter filter)
    {
        InstructorPaginationData instructors = await service.GetInstructors(filter);
        return Ok(instructors);
    }
}
