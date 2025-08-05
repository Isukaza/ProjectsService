namespace ProjectsService.Models.Requests;

public class ProjectUpdateRequest
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public List<ChartRequest>? Charts { get; set; }
}