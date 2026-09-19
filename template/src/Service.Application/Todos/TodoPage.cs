namespace Service.Application.Todos;

public sealed record TodoPage(IReadOnlyList<TodoResponse> Items, int Offset, int PageSize, bool HasMore);
