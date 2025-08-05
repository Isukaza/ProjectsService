using ProjectsService.Models.Requests;
using ProjectsService.Models.Responses;

namespace ProjectsService.Managers.Interfaces;

public interface IProjectManager
{
    Task<IEnumerable<ProjectResponse>> GetAllAsync();
    Task<ProjectResponse> GetByIdAsync(string id);
    Task<ProjectResponse> CreateAsync(ProjectCreateRequest projectCreate);
    Task<ProjectResponse> UpdateAsync(ProjectUpdateRequest updateRequest);
    Task<bool> DeleteAsync(string id);
}