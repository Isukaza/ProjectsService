using System.Net;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Moq;
using Moq.Protected;
using ProjectsService.Managers;
using ProjectsService.Models.Enums;

namespace Tests;

public class AnalystManagerTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly AnalystManager _manager;

    public AnalystManagerTests()
    {
        _repoMock = new Mock<IProjectRepository>();

        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("""
                                            [
                                                {
                                                    "Id": 1,
                                                    "Name": "John Doe",
                                                    "Email": "john@example.com",
                                                    "SubscriptionType": "Super"
                                                }
                                            ]
                                            """)
            });

        var httpClient = new HttpClient(handler.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };

        _manager = new AnalystManager(httpClient, _repoMock.Object);
    }

    [Fact]
    public async Task GetUserIdsBySubscriptionAsync_ShouldReturnIds()
    {
        var result = await _manager.GetUserIdsBySubscriptionAsync(SubscriptionType.Free);

        Assert.NotNull(result);
        var list = result.ToList();
        Assert.Single(list);
        Assert.Contains(1, list);
    }

    [Fact]
    public async Task GetTopIndicatorsBySubscriptionAsync_ShouldReturnTop3()
    {
        var projects = new List<ProjectDocument>
        {
            new()
            {
                UserId = 1,
                Name = "P1",
                Charts =
                [
                    new ChartDocument
                    {
                        Indicators =
                        [
                            new IndicatorDocument
                            {
                                Name = DAL.Models.Enums.IndicatorName.MA,
                                Parameters = "p=1"
                            },
                            new IndicatorDocument
                            {
                                Name = DAL.Models.Enums.IndicatorName.RSI,
                                Parameters = "p=2"
                            }
                        ]
                    }
                ]
            }
        };

        _repoMock.Setup(r => r.GetByUserIdsAsync(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync(projects);

        var result = await _manager.GetTopIndicatorsBySubscriptionAsync([1]);

        Assert.NotNull(result);
        var list = result.ToList();
        Assert.NotEmpty(list);
        Assert.True(list.Count <= 3);
        Assert.Contains(list, i => i.Name == DAL.Models.Enums.IndicatorName.MA);
    }
}