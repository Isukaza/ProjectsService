using DAL.Models.Enums;

namespace ProjectsService.Models.Responses;

public class Chart
{
    public Symbol Symbol { get; set; }
    public Timeframe Timeframe { get; set; }
    public List<Indicator> Indicators { get; set; } = [];
}