using ProjectsService.Models.Enums;
using ProjectsService.Models.Responses;

namespace ProjectsService.Managers.Interfaces;

public interface IAnalystManager
{
    Task<List<int>> GetUserIdsBySubscriptionAsync(SubscriptionType subType);
    Task<IEnumerable<IndicatorUsage>> GetTopIndicatorsBySubscriptionAsync(List<int> userIds);
}