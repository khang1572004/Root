using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Service.Infrastructure.Persistence.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder.HasAnnotation("ProductVersion", "10.0.0");

        modelBuilder.Entity("Service.Domain.Todos.TodoItem", b =>
        {
            b.Property<Guid>("Id").HasColumnType("uuid");
            b.Property<DateTimeOffset?>("CompletedAt").HasColumnType("timestamp with time zone");
            b.Property<DateTimeOffset>("CreatedAt").HasColumnType("timestamp with time zone");
            b.Property<bool>("IsCompleted").HasColumnType("boolean");
            b.Property<Guid>("TenantId").HasColumnType("uuid");
            b.Property<string>("Title").IsRequired().HasMaxLength(200).HasColumnType("character varying(200)");
            b.HasKey("Id");
            b.HasIndex("TenantId", "CreatedAt", "Id");
            b.ToTable("todo_items", (string)null);
        });
#pragma warning restore 612, 618
    }
}
