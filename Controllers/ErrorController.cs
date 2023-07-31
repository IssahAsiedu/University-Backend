using Microsoft.AspNetCore.Mvc;

namespace UniversityRestApi.Controllers;

[ApiController, ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController: ControllerBase
{
    [Route("Error")]
    public IActionResult HandleError() => Problem();
}
