using Microsoft.AspNetCore.Mvc;
using ProjectsService.Managers.Interfaces;
using ProjectsService.Models.Responses;

namespace ProjectsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectManager manager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll()
    {
        var projects = await manager.GetAllAsync();
        return Ok(projects);
    }
}