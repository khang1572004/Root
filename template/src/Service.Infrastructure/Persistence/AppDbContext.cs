using Microsoft.EntityFrameworkCore;
using Service.Domain.Todos;

namespace Service.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>(builder =>
        {
            builder.ToTable("todo_items");
            builder.HasKey(todoItem => todoItem.Id);
            builder.Property(todoItem => todoItem.TenantId).IsRequired();
            builder.Property(todoItem => todoItem.Title).HasMaxLength(200).IsRequired();
            builder.Property(todoItem => todoItem.CreatedAt).HasColumnType("timestamp with time zone");
            builder.Property(todoItem => todoItem.CompletedAt).HasColumnType("timestamp with time zone");
            builder.HasIndex(todoItem => new { todoItem.TenantId, todoItem.CreatedAt, todoItem.Id });
        });
    }
}
