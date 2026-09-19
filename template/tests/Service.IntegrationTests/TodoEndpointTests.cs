using System.Net;
using System.Net.Http.Json;

namespace Service.IntegrationTests;

public sealed class TodoEndpointTests(TestApplicationFactory factory)
    : IClassFixture<TestApplicationFactory>
{
    [Fact]
    public async Task Get_todos_without_token_returns_401()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/todos");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_todo_without_token_returns_401()
    {
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/todos", new { Title = "task" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Patch_complete_without_token_returns_401()
    {
        var client = factory.CreateClient();

        var response = await client.PatchAsync($"/api/todos/{Guid.NewGuid()}/complete", null);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Auth_is_enforced_before_query_validation()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/todos?offset=-1");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Health_live_is_anonymous_and_succeeds()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/health/live");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Security_headers_are_present_on_responses()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/health/live");

        Assert.True(response.Headers.Contains("X-Content-Type-Options"));
        Assert.Equal("nosniff", response.Headers.GetValues("X-Content-Type-Options").First());
        Assert.True(response.Headers.Contains("X-Frame-Options"));
        Assert.Equal("DENY", response.Headers.GetValues("X-Frame-Options").First());
    }
}
