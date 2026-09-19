# Docker and deployment

## Local development

`template/docker-compose.yml` is local-development-only. It runs PostgreSQL and, optionally, the API; its published ports bind to loopback. Copy `.env.example` to `.env`; do not commit the resulting file. Its `Development` setting must not be copied into a production deployment.

## Deployment defaults

- Build an immutable image in CI.
- Update the centrally declared Docker base-image arguments and PostgreSQL reference deliberately; use immutable digests in the production build/release configuration and record the reviewed version or digest in the release evidence.
- Supply configuration and secrets through the runtime platform, never image layers or Compose files.
- Run the Dockerfile's `migration-bundle` target as a deliberate deployment step using a migration database principal. Do not run migrations when the API starts.
- Route traffic only after readiness succeeds.
- The API image runs as a dedicated non-root user. Use a read-only root filesystem where the deployment environment supports it and mount writable storage only when necessary.

Production configuration must be owned by the environment, not baked into the image.

## Network and TLS boundary

Terminate TLS at a trusted edge or proxy. The container serves HTTP only on its internal port; expose it publicly only through the TLS-terminating edge. Configure forwarded headers only for explicitly trusted proxies or networks. Enable HTTPS redirects and HSTS only when that topology is correctly configured, so internal HTTP traffic cannot spoof client scheme or address information.

## Runtime configuration

Provide the PostgreSQL connection string, generic JWT authority and audience, CORS allowlist, trusted-proxy settings, and rate limits as environment-specific configuration. Store credentials and other sensitive values in the platform secret store. Production database connections should require TLS with certificate validation appropriate to the organization's trust model.

## Release order

1. Provision secret/configuration values, the trusted TLS edge, and distinct migration and runtime database principals.
2. Build and scan the API image and migration bundle from the reviewed commit.
3. Run the reviewed migration bundle with the migration principal.
4. Deploy the API with the least-privilege runtime principal and route traffic only after `/health/ready` succeeds.

`/health/live` reports only whether the process is running. `/health/ready` verifies required readiness dependencies and should be the routing gate; neither endpoint should disclose secrets or detailed infrastructure state.
