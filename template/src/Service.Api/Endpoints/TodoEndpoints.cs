using Service.Application.Todos;

namespace Service.Api.Endpoints;

public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/todos")
            .WithTags("Todos")
            .RequireAuthorization()
            .RequireRateLimiting("api");

        group.MapGet("/", async (int? offset, int? pageSize, TodoService todoService, CancellationToken cancellationToken) =>
        {
            var todos = await todoService.ListAsync(offset ?? 0, pageSize ?? 25, cancellationToken);
            return Results.Ok(todos);
        }).Produces<TodoPage>();

        group.MapPost("/", async (
            CreateTodoRequest request,
            TodoService todoService,
            CancellationToken cancellationToken) =>
        {
            var todo = await todoService.CreateAsync(request.Title, cancellationToken);
            return Results.Created($"/api/todos/{todo.Id}", todo);
        }).RequireRateLimiting("write");

        group.MapPatch("/{id:guid}/complete", async (
            Guid id,
            TodoService todoService,
            CancellationToken cancellationToken) =>
        {
            var todo = await todoService.CompleteAsync(id, cancellationToken);
            return Results.Ok(todo);
        });

        return endpoints;
    }

    public sealed record CreateTodoRequest(string? Title);
}
