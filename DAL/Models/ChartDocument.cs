using DAL.Models.Enums;

namespace DAL.Models;

public class ChartDocument
{
    public Symbol Symbol { get; set; }
    public Timeframe Timeframe { get; set; }
    public List<IndicatorDocument> Indicators { get; set; } = [];
}