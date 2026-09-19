using System.Security.Claims;
using Service.Application.Common;

namespace Service.Api.Authentication;

public sealed class HttpTenantContext(IHttpContextAccessor httpContextAccessor) : ITenantContext
{
    public Guid TenantId
    {
        get
        {
            var tenantValue = httpContextAccessor.HttpContext?.User.FindFirstValue("tenant_id");
            return Guid.TryParse(tenantValue, out var tenantId)
                ? tenantId
                : throw new TenantNotAvailableException();
        }
    }
}
