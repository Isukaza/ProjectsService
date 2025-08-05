namespace ProjectsService.Models.Requests;

public class ProjectUpdate
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public List<Chart>? Charts { get; set; }
}