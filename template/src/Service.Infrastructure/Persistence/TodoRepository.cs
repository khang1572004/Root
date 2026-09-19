using Microsoft.EntityFrameworkCore;
using Service.Application.Todos;
using Service.Domain.Todos;

namespace Service.Infrastructure.Persistence;

public sealed class TodoRepository(AppDbContext dbContext) : ITodoRepository
{
    public async Task<IReadOnlyList<TodoItem>> ListAsync(Guid tenantId, int offset, int limit, CancellationToken cancellationToken) =>
        await dbContext.TodoItems
            .AsNoTracking()
            .Where(todoItem => todoItem.TenantId == tenantId)
            .OrderByDescending(todoItem => todoItem.CreatedAt)
            .ThenByDescending(todoItem => todoItem.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);

    public Task<TodoItem?> FindAsync(Guid tenantId, Guid id, CancellationToken cancellationToken) =>
        dbContext.TodoItems.FirstOrDefaultAsync(todoItem => todoItem.TenantId == tenantId && todoItem.Id == id, cancellationToken);

    public async Task AddAsync(TodoItem todoItem, CancellationToken cancellationToken) =>
        await dbContext.TodoItems.AddAsync(todoItem, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
