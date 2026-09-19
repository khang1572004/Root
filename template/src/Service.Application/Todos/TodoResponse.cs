using Service.Domain.Todos;

namespace Service.Application.Todos;

public sealed record TodoResponse(
    Guid Id,
    string Title,
    bool IsCompleted,
    DateTimeOffset CreatedAt,
    DateTimeOffset? CompletedAt)
{
    public static TodoResponse From(TodoItem todoItem) => new(
        todoItem.Id,
        todoItem.Title,
        todoItem.IsCompleted,
        todoItem.CreatedAt,
        todoItem.CompletedAt);
}
