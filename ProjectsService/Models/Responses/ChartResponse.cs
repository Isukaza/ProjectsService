using DAL.Models.Enums;

namespace ProjectsService.Models.Responses;

public class ChartResponse
{
    public Symbol Symbol { get; set; }
    public Timeframe Timeframe { get; set; }
    public List<IndicatorResponse> Indicators { get; set; } = [];
}