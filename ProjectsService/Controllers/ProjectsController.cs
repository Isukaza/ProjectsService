using Microsoft.AspNetCore.Mvc;
using ProjectsService.Managers.Interfaces;
using ProjectsService.Models.Requests;
using ProjectsService.Models.Responses;

namespace ProjectsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectManager manager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAll()
    {
        var projects = await manager.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> GetById(string id)
    {
        var project = await manager.GetByIdAsync(id);
        return project == null ? NotFound() : Ok(project);
    }

    [HttpPost("create")]
    public async Task<ActionResult<ProjectResponse>> Create([FromBody] ProjectCreateRequest projectCreate)
    {
        var created = await manager.CreateAsync(projectCreate);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("update")]
    public async Task<ActionResult<ProjectResponse>> Update([FromBody] ProjectUpdateRequest projectUpdate)
    {
        var updated = await manager.UpdateAsync(projectUpdate);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await manager.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}