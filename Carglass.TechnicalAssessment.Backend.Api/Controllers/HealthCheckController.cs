using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    public HealthCheckController()
    {
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult KeepAlive()
    {
        return Ok();
    }
}