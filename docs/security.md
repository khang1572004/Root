# Security baseline

## Required baseline

- Keep the .NET SDK and NuGet dependencies patched.
- Use CodeQL and dependency updates in GitHub.
- Do not commit secrets; enable repository secret scanning where available.
- Validate all external input and encode output for its destination.
- Apply least privilege to database users, service identities, and CI tokens.
- Require TLS outside local development and set security headers appropriate to the API.
- Use parameterized access through EF Core/Npgsql; never concatenate untrusted SQL.
- Configure bearer-token validation against a generic OpenID Connect issuer and API audience. Validate issuer, audience, signature, lifetime, and clock skew; do not embed provider-specific assumptions in the service.
- Give migration tooling a separate database principal with only the schema privileges it needs. The running API must use a least-privilege principal.
- Run containers as non-root, use a read-only root filesystem where supported, and provide only the writable mounts the runtime requires.

## Before release

Review authorization, exposed endpoints, configuration, dependency advisories, logs, database permissions, trusted-proxy boundaries, and TLS termination. Threat-model changes involving payments, personal data, file upload, webhooks, or admin actions.
