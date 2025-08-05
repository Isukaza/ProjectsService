using DAL.Models.Enums;

namespace ProjectsService.Models.Responses;

public class IndicatorResponse
{
    public IndicatorName Name { get; set; }
    public string Parameters { get; set; }
}