using DAL.Models.Enums;

namespace ProjectsService.Models.Requests;

public class Indicator
{
    public IndicatorName Name { get; set; }
    public string Parameters { get; set; }
}