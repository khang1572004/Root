# Authentication and authorization

## Defaults

- Authentication proves identity; authorization determines access. Keep them separate.
- Protect endpoints by default: the template uses a fallback authorization policy requiring an authenticated user. Health checks are explicitly anonymous.
- Use generic OpenID Connect JWT bearer authentication (`Jwt:Authority`, `Jwt:Audience`). Do not embed provider-specific SDKs.
- Validate issuer, audience, signature, lifetime, and clock skew for bearer tokens.
- Use policies/permissions for business actions. Roles are useful inputs, not a complete permission model.
- Do authorization inside the use case when access depends on the target resource.

## Tenant isolation

- The template identifies the current tenant from the required `tenant_id` JWT claim (GUID format).
- Tokens without a valid `tenant_id` claim receive `403 Forbidden`.
- All data access is scoped by tenant ID at the application and repository layers. Cross-tenant IDs return `404` to prevent resource enumeration.
- Tenant scope is never accepted from request body, route, or query parameters.
- When adding new entities, always include `TenantId` as a required, immutable, indexed column and filter all queries by it.

## Secrets

Local secrets belong in user secrets or `.env`, never in `appsettings*.json` committed to Git. Production secrets belong in the platform's secret store. The committed `appsettings.json` contains only non-secret defaults and placeholder JWT values.
