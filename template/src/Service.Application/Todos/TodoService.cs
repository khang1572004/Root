using Service.Application.Common;
using Service.Domain.Todos;

namespace Service.Application.Todos;

public sealed class TodoService(ITodoRepository repository, ITenantContext tenantContext, TimeProvider timeProvider)
{
    public async Task<TodoPage> ListAsync(int offset, int pageSize, CancellationToken cancellationToken)
    {
        if (offset < 0)
        {
            throw Validation("offset", "Offset must not be negative.");
        }

        if (pageSize is < 1 or > 100)
        {
            throw Validation("pageSize", "Page size must be between 1 and 100.");
        }

        var todoItems = await repository.ListAsync(tenantContext.TenantId, offset, pageSize + 1, cancellationToken);
        var hasMore = todoItems.Count > pageSize;
        return new TodoPage(todoItems.Take(pageSize).Select(TodoResponse.From).ToList(), offset, pageSize, hasMore);
    }

    public async Task<TodoResponse> CreateAsync(string? title, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw Validation("title", "Title is required.");
        }

        TodoItem todoItem;
        try
        {
            todoItem = TodoItem.Create(tenantContext.TenantId, title, timeProvider.GetUtcNow());
        }
        catch (ArgumentException exception)
        {
            throw Validation("title", exception.Message);
        }

        await repository.AddAsync(todoItem, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return TodoResponse.From(todoItem);
    }

    public async Task<TodoResponse> CompleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var todoItem = await repository.FindAsync(tenantContext.TenantId, id, cancellationToken)
            ?? throw new NotFoundException("Todo item", id);

        todoItem.MarkCompleted(timeProvider.GetUtcNow());
        await repository.SaveChangesAsync(cancellationToken);

        return TodoResponse.From(todoItem);
    }

    private static ApplicationValidationException Validation(string field, string error) => new(new Dictionary<string, string[]>
    {
        [field] = [error],
    });
}
