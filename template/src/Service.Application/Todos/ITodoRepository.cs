using Service.Domain.Todos;

namespace Service.Application.Todos;

public interface ITodoRepository
{
    Task<IReadOnlyList<TodoItem>> ListAsync(Guid tenantId, int offset, int limit, CancellationToken cancellationToken);

    Task<TodoItem?> FindAsync(Guid tenantId, Guid id, CancellationToken cancellationToken);

    Task AddAsync(TodoItem todoItem, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
