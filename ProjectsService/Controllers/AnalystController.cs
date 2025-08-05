using Microsoft.AspNetCore.Mvc;
using ProjectsService.Managers.Interfaces;
using ProjectsService.Models.Enums;
using ProjectsService.Models.Responses;

namespace ProjectsService.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalystManager analystManager) : ControllerBase
{
    [HttpGet("top-indicators/{subscriptionType}")]
    public async Task<ActionResult<IEnumerable<IndicatorUsage>>> GetTopIndicators(SubscriptionType subscriptionType)
    {
        var userIds = await analystManager.GetUserIdsBySubscriptionAsync(subscriptionType);
        if (userIds == null || userIds.Count == 0)
            return Ok(new {});

        var indicators = await analystManager.GetTopIndicatorsBySubscriptionAsync(userIds);
        return Ok(indicators);
    }
}