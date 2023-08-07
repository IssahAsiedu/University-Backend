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

    [HttpGet]
    public async Task<IActionResult> GetInstructors([FromQuery] PaginationFilter filter)
    {
        InstructorPaginationData instructors = await service.GetInstructors(filter);
        return Ok(instructors);
    }
}
