namespace Service.Application.Common;

public interface ITenantContext
{
    Guid TenantId { get; }
}
