using Microsoft.AspNetCore.Mvc;

namespace ProjectsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok("all");
}