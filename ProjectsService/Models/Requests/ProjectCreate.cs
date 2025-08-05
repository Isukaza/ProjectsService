namespace ProjectsService.Models.Requests;

public class ProjectCreate
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<Chart> Charts { get; set; } = [];
}