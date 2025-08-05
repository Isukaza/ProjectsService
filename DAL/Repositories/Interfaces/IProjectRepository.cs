using DAL.Models;

namespace DAL.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectDocument>> GetAllAsync();
    Task<ProjectDocument> GetByIdAsync(string id);
    Task<IEnumerable<ProjectDocument>> GetByUserIdsAsync(IEnumerable<int> userIds);
    Task<ProjectDocument> CreateAsync(ProjectDocument project);
    Task<ProjectDocument> UpdateAsync(ProjectDocument project);
    Task<bool> DeleteAsync(string id);
}