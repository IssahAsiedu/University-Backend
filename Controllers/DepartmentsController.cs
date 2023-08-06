using Microsoft.AspNetCore.Mvc;
using UniversityRestApi.Services;

namespace UniversityRestApi.Controllers;

[ApiController, Route("[controller]")]
public class DepartmentsController: ControllerBase
{
    private readonly DepartmentsService service;

    public DepartmentsController(DepartmentsService service)
    {
        this.service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var result = await service.GetDepartments();
        return Ok(result);
    }
}
