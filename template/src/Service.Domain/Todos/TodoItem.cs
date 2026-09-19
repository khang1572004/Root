namespace Service.Domain.Todos;

public sealed class TodoItem
{
    private TodoItem()
    {
        Title = string.Empty;
    }

    private TodoItem(Guid id, Guid tenantId, string title, DateTimeOffset createdAt)
    {
        Id = id;
        TenantId = tenantId;
        Title = title;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }

    public Guid TenantId { get; private set; }

    public string Title { get; private set; }

    public bool IsCompleted { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? CompletedAt { get; private set; }

    public static TodoItem Create(Guid tenantId, string title, DateTimeOffset createdAt)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("A tenant is required.", nameof(tenantId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("A title is required.", nameof(title));
        }

        var normalizedTitle = title.Trim();
        if (normalizedTitle.Length > 200)
        {
            throw new ArgumentException("A title cannot exceed 200 characters.", nameof(title));
        }

        return new TodoItem(Guid.NewGuid(), tenantId, normalizedTitle, createdAt);
    }

    public void MarkCompleted(DateTimeOffset completedAt)
    {
        if (IsCompleted)
        {
            return;
        }

        IsCompleted = true;
        CompletedAt = completedAt;
    }
}
