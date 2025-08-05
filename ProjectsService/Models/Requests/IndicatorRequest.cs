using DAL.Models.Enums;

namespace ProjectsService.Models.Requests;

public class IndicatorRequest
{
    public IndicatorName Name { get; set; }
    public string Parameters { get; set; }
}