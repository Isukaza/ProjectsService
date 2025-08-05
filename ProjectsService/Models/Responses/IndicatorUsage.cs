using DAL.Models.Enums;

namespace ProjectsService.Models.Responses;

public class IndicatorUsage
{
    public IndicatorName Name { get; set; }
    public int Count { get; set; }
}