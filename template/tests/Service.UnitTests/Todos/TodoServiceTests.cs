using Service.Application.Common;
using Service.Application.Todos;
using Service.Domain.Todos;

namespace Service.UnitTests.Todos;

public sealed class TodoServiceTests
{
    private readonly FakeTenantContext _tenantContext = new();
    private readonly FakeTimeProvider _timeProvider = new();
    private readonly FakeTodoRepository _repository = new();

    private TodoService CreateService() => new(_repository, _tenantContext, _timeProvider);

    [Fact]
    public async Task ListAsync_rejects_negative_offset()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ApplicationValidationException>(() =>
            service.ListAsync(-1, 25, CancellationToken.None));
    }

    [Fact]
    public async Task ListAsync_rejects_page_size_zero()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ApplicationValidationException>(() =>
            service.ListAsync(0, 0, CancellationToken.None));
    }

    [Fact]
    public async Task ListAsync_rejects_page_size_over_100()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ApplicationValidationException>(() =>
            service.ListAsync(0, 101, CancellationToken.None));
    }

    [Fact]
    public async Task ListAsync_returns_bounded_page()
    {
        var service = CreateService();

        var page = await service.ListAsync(0, 25, CancellationToken.None);

        Assert.NotNull(page);
        Assert.Equal(0, page.Offset);
        Assert.Equal(25, page.PageSize);
        Assert.False(page.HasMore);
    }

    [Fact]
    public async Task CreateAsync_rejects_blank_title()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<ApplicationValidationException>(() =>
            service.CreateAsync("  ", CancellationToken.None));
    }

    [Fact]
    public async Task CreateAsync_assigns_tenant_from_context()
    {
        var service = CreateService();

        var response = await service.CreateAsync("Buy groceries", CancellationToken.None);

        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal("Buy groceries", response.Title);

        var stored = _repository.Items.Single();
        Assert.Equal(_tenantContext.TenantId, stored.TenantId);
    }

    [Fact]
    public async Task CompleteAsync_throws_not_found_for_missing_item()
    {
        var service = CreateService();

        await Assert.ThrowsAsync<NotFoundException>(() =>
            service.CompleteAsync(Guid.NewGuid(), CancellationToken.None));
    }

    private sealed class FakeTenantContext : ITenantContext
    {
        public Guid TenantId { get; } = Guid.NewGuid();
    }

    private sealed class FakeTimeProvider : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => new(2026, 9, 19, 12, 0, 0, TimeSpan.Zero);
    }

    private sealed class FakeTodoRepository : ITodoRepository
    {
        public List<TodoItem> Items { get; } = [];

        public Task<IReadOnlyList<TodoItem>> ListAsync(Guid tenantId, int offset, int limit, CancellationToken cancellationToken)
        {
            IReadOnlyList<TodoItem> result = Items
                .Where(i => i.TenantId == tenantId)
                .OrderByDescending(i => i.CreatedAt)
                .ThenByDescending(i => i.Id)
                .Skip(offset)
                .Take(limit)
                .ToList();
            return Task.FromResult(result);
        }

        public Task<TodoItem?> FindAsync(Guid tenantId, Guid id, CancellationToken cancellationToken)
        {
            var item = Items.FirstOrDefault(i => i.TenantId == tenantId && i.Id == id);
            return Task.FromResult(item);
        }

        public Task AddAsync(TodoItem todoItem, CancellationToken cancellationToken)
        {
            Items.Add(todoItem);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
