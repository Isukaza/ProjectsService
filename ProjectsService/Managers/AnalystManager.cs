using System.Text.Json;
using System.Text.Json.Serialization;
using FluentRest;
using DAL.Repositories.Interfaces;
using ProjectsService.Managers.Interfaces;
using ProjectsService.Models.Responses;
using ProjectsService.Models.Enums;

namespace ProjectsService.Managers;

public class AnalystManager(HttpClient httpClient, IProjectRepository repo) : IAnalystManager
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<List<int>> GetUserIdsBySubscriptionAsync(SubscriptionType subType)
    {
        var response = await httpClient.GetAsync(b => b
            .AppendPath("api")
            .AppendPath("Users")
            .AppendPath("by-subscription")
            .AppendPath(subType.ToString())
            .Header("accept", "application/json")
        );

        var users = await response.Content.ReadFromJsonAsync<List<UserInfo>>(JsonSerializerOptions);
        return users?.Select(u => u.Id).ToList() ?? [];
    }

    public async Task<IEnumerable<IndicatorUsage>> GetTopIndicatorsBySubscriptionAsync(List<int> userIds)
    {
        if (userIds == null || userIds.Count == 0)
            return [];

        var userProjects = await repo.GetByUserIdsAsync(userIds);
        var indicatorCounts = userProjects
            .SelectMany(p => p.Charts)
            .SelectMany(c => c.Indicators)
            .GroupBy(i => i.Name)
            .Select(g => new IndicatorUsage
            {
                Name = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(3)
            .ToList();

        return indicatorCounts;
    }
}