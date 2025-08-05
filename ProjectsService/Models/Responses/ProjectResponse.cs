namespace ProjectsService.Models.Responses;

public class ProjectResponse
{
    public string Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<ChartResponse> Charts { get; set; } = [];
}