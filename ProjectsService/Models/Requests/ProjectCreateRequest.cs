namespace ProjectsService.Models.Requests;

public class ProjectCreateRequest
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<ChartRequest> Charts { get; set; } = [];
}