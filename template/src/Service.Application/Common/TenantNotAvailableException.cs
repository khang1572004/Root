namespace Service.Application.Common;

public sealed class TenantNotAvailableException
    : Exception("The authenticated request does not contain a valid tenant_id claim.")
{
}
