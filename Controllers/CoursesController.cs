using Microsoft.AspNetCore.Mvc;
using UniversityRestApi.Dto;

namespace UniversityRestApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CoursesController: ControllerBase
{

    [HttpPost]
    public async Task CreateCourse(CourseDto courseDto)
    {
        throw new NotImplementedException();
    }
}