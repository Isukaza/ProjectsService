using ProjectsService.Models.Responses;

namespace ProjectsService.Managers.Interfaces;

public interface IProjectManager
{
    Task<IEnumerable<Project>> GetAllAsync();
}