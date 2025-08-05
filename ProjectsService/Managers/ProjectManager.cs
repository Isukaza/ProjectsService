using DAL.Models;
using DAL.Repositories.Interfaces;
using ProjectsService.Managers.Interfaces;
using ProjectsService.Models.Requests;
using ProjectsService.Models.Responses;

namespace ProjectsService.Managers;

public class ProjectManager(IProjectRepository repo) : IProjectManager
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
    {
        var docs = await repo.GetAllAsync();
        return docs.Select(ToResponse);
    }

    public async Task<ProjectResponse> GetByIdAsync(string id)
    {
        var doc = await repo.GetByIdAsync(id);
        return doc == null ? null : ToResponse(doc);
    }

    public async Task<ProjectResponse> CreateAsync(ProjectCreateRequest projectCreate)
    {
        var entity = new ProjectDocument
        {
            UserId = projectCreate.UserId,
            Name = projectCreate.Name,
            Charts = projectCreate.Charts.Select(c => new ChartDocument
            {
                Symbol = c.Symbol,
                Timeframe = c.Timeframe,
                Indicators = c.Indicators.Select(i => new IndicatorDocument
                {
                    Name = i.Name,
                    Parameters = i.Parameters
                }).ToList()
            }).ToList()
        };

        var created = await repo.CreateAsync(entity);
        return ToResponse(created);
    }

    public async Task<ProjectResponse> UpdateAsync(ProjectUpdateRequest updateRequest)
    {
        var existing = await repo.GetByIdAsync(updateRequest.Id);
        if (existing == null)
            return null;

        if (!string.IsNullOrWhiteSpace(updateRequest.Name))
            existing.Name = updateRequest.Name;

        if (updateRequest.Charts != null)
        {
            existing.Charts = updateRequest.Charts.Select(c => new ChartDocument
            {
                Symbol = c.Symbol,
                Timeframe = c.Timeframe,
                Indicators = c.Indicators.Select(i => new IndicatorDocument
                {
                    Name = i.Name,
                    Parameters = i.Parameters
                }).ToList()
            }).ToList();
        }

        var updated = await repo.UpdateAsync(existing);
        return updated == null ? null : ToResponse(updated);
    }

    public async Task<bool> DeleteAsync(string id) =>
        await repo.DeleteAsync(id);

    private static ProjectResponse ToResponse(ProjectDocument doc) =>
        new()
        {
            Id = doc.Id,
            UserId = doc.UserId,
            Name = doc.Name,
            Charts = doc.Charts.Select(c => new ChartResponse
            {
                Symbol = c.Symbol,
                Timeframe = c.Timeframe,
                Indicators = c.Indicators.Select(i => new IndicatorResponse
                {
                    Name = i.Name,
                    Parameters = i.Parameters
                }).ToList()
            }).ToList()
        };
}