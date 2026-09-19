using System.Net;

namespace Service.IntegrationTests;

public sealed class HealthEndpointTests(TestApplicationFactory factory)
    : IClassFixture<TestApplicationFactory>
{
    [Fact]
    public async Task Liveness_endpoint_returns_ok_without_a_database_connection()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/health/live");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Todo_endpoint_requires_an_access_token()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/todos");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
