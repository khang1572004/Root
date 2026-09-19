# New service checklist

- [ ] Rename `Service` projects, namespaces, solution, Docker service, and documentation.
- [ ] Set a unique service name, repository description, owner, and license.
- [ ] Copy `.env.example` to `.env` and replace the local password.
- [ ] Start PostgreSQL and verify `/health/live` and `/health/ready`.
- [ ] Treat Compose as local-only; do not reuse its `Development` environment setting or local port publishing in production.
- [ ] Define the first API contract, authorization rule, and failure modes.
- [ ] Configure generic OpenID Connect JWT authority and audience, validate standard token properties, and define required tenant/authorization claims without selecting a provider-specific SDK.
- [ ] Add the initial EF Core migration and review its SQL impact.
- [ ] Build and test the migration bundle; define separate migration and least-privilege runtime database principals.
- [ ] Add unit tests plus at least one endpoint integration test.
- [ ] Confirm no secrets or sample personal data are tracked by Git.
- [ ] Define the trusted TLS edge/proxy, forwarded-header trust boundary, security headers, and production CORS allowlist.
- [ ] Confirm the production container runs as non-root, supports a read-only root filesystem, and is routed only after readiness succeeds.
- [ ] Enable branch protection, CodeQL, Dependabot, and secret scanning in GitHub.
- [ ] Record non-default architecture choices in an ADR.
