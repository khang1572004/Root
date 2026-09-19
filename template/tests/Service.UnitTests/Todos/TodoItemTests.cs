using Service.Domain.Todos;

namespace Service.UnitTests.Todos;

public sealed class TodoItemTests
{
    [Fact]
    public void Create_trims_a_valid_title()
    {
        var createdAt = new DateTimeOffset(2026, 9, 19, 0, 0, 0, TimeSpan.Zero);

        var tenantId = Guid.NewGuid();
        var todoItem = TodoItem.Create(tenantId, "  Learn .NET  ", createdAt);

        Assert.Equal("Learn .NET", todoItem.Title);
        Assert.Equal(tenantId, todoItem.TenantId);
        Assert.False(todoItem.IsCompleted);
        Assert.Equal(createdAt, todoItem.CreatedAt);
    }

    [Fact]
    public void MarkCompleted_sets_completion_state_once()
    {
        var todoItem = TodoItem.Create(Guid.NewGuid(), "Write a test", DateTimeOffset.UtcNow);
        var completedAt = DateTimeOffset.UtcNow;

        todoItem.MarkCompleted(completedAt);
        todoItem.MarkCompleted(completedAt.AddMinutes(1));

        Assert.True(todoItem.IsCompleted);
        Assert.Equal(completedAt, todoItem.CompletedAt);
    }

    [Fact]
    public void Create_rejects_empty_tenant()
    {
        Assert.Throws<ArgumentException>(() => TodoItem.Create(Guid.Empty, "Valid title", DateTimeOffset.UtcNow));
    }

    [Fact]
    public void Create_rejects_blank_title()
    {
        Assert.Throws<ArgumentException>(() => TodoItem.Create(Guid.NewGuid(), "   ", DateTimeOffset.UtcNow));
    }

    [Fact]
    public void Create_rejects_title_exceeding_200_characters()
    {
        var longTitle = new string('A', 201);
        Assert.Throws<ArgumentException>(() => TodoItem.Create(Guid.NewGuid(), longTitle, DateTimeOffset.UtcNow));
    }

    [Fact]
    public void Create_accepts_title_at_exactly_200_characters()
    {
        var title = new string('B', 200);
        var todoItem = TodoItem.Create(Guid.NewGuid(), title, DateTimeOffset.UtcNow);
        Assert.Equal(title, todoItem.Title);
    }
}
