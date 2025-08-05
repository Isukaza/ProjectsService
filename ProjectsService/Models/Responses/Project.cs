namespace ProjectsService.Models.Responses;

public class Project
{
    public string Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<Chart> Charts { get; set; } = [];
}