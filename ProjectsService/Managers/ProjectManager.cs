using DAL.Repositories.Interfaces;
using ProjectsService.Models.Responses;
using ProjectsService.Managers.Interfaces;

namespace ProjectsService.Managers;

public class ProjectManager(IProjectRepository repo) : IProjectManager
{
    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var docs = await repo.GetAllAsync();

        return docs.Select(d => new Project
        {
            Id = d.Id,
            UserId = d.UserId,
            Name = d.Name,
            Charts = d.Charts.Select(c => new Chart
            {
                Symbol = c.Symbol,
                Timeframe = c.Timeframe,
                Indicators = c.Indicators.Select(i => new Indicator
                {
                    Name = i.Name,
                    Parameters = i.Parameters
                }).ToList()
            }).ToList()
        });
    }
}