using DAL.Models.Enums;

namespace DAL.Models;

public class IndicatorDocument
{
    public IndicatorName Name { get; set; }
    public string Parameters { get; set; }
}