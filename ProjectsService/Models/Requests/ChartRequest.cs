using DAL.Models.Enums;

namespace ProjectsService.Models.Requests;

public class ChartRequest
{
    public Symbol Symbol { get; set; }
    public Timeframe Timeframe { get; set; }
    public List<IndicatorRequest> Indicators { get; set; } = [];
}