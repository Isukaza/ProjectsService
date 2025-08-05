using DAL.Models;
using DAL.Repositories.Interfaces;
using Moq;
using ProjectsService.Managers;
using ProjectsService.Models.Requests;
using ProjectsService.Models.Responses;

namespace Tests;

public class ProjectManagerTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly ProjectManager _manager;

    private readonly List<ProjectDocument> _projects =
    [
        new()
        {
            Id = "1",
            UserId = 1,
            Name = "Project 1",
            Charts =
            [
                new ChartDocument
                {
                    Symbol = DAL.Models.Enums.Symbol.EURUSD,
                    Timeframe = DAL.Models.Enums.Timeframe.H1,
                    Indicators =
                    [
                        new IndicatorDocument
                        {
                            Name = DAL.Models.Enums.IndicatorName.MA,
                            Parameters = "p=1"
                        }
                    ]
                }
            ]
        }
    ];

    public ProjectManagerTests()
    {
        _repoMock = new Mock<IProjectRepository>();
        _manager = new ProjectManager(_repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProjects()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(_projects);

        var result = await _manager.GetAllAsync();

        Assert.NotNull(result);
        var list = new List<ProjectResponse>(result);
        Assert.Single(list);
        Assert.Equal("Project 1", list[0].Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProject()
    {
        _repoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(_projects[0]);

        var result = await _manager.GetByIdAsync("1");

        Assert.NotNull(result);
        Assert.Equal("Project 1", result.Name);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedProject()
    {
        var request = new ProjectCreateRequest
        {
            UserId = 1,
            Name = "Created Project",
            Charts =
            [
                new ChartRequest
                {
                    Symbol = DAL.Models.Enums.Symbol.EURUSD,
                    Timeframe = DAL.Models.Enums.Timeframe.H1,
                    Indicators =
                    [
                        new IndicatorRequest
                        {
                            Name = DAL.Models.Enums.IndicatorName.MA,
                            Parameters = "p=1"
                        }
                    ]
                }
            ]
        };

        _repoMock.Setup(r => r.CreateAsync(It.IsAny<ProjectDocument>()))
            .ReturnsAsync(new ProjectDocument { Id = "2", UserId = 1, Name = "Created Project" });

        var result = await _manager.CreateAsync(request);

        Assert.NotNull(result);
        Assert.Equal("Created Project", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedProject()
    {
        var existing = _projects[0];
        _repoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(existing);
        _repoMock.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(existing);

        var update = new ProjectUpdateRequest
        {
            Id = "1",
            Name = "Updated"
        };

        var result = await _manager.UpdateAsync(update);

        Assert.NotNull(result);
        Assert.Equal("Updated", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue()
    {
        _repoMock.Setup(r => r.DeleteAsync("1")).ReturnsAsync(true);

        var result = await _manager.DeleteAsync("1");

        Assert.True(result);
    }
}