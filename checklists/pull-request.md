# Pull request checklist

- [ ] The change has a clear, narrow purpose.
- [ ] API, configuration, and migration changes are documented.
- [ ] Tests cover normal behavior, failure behavior, and security-sensitive paths.
- [ ] `dotnet format --verify-no-changes`, build, and tests pass.
- [ ] Logs and errors do not expose secrets or sensitive data.
- [ ] Authorization is checked for every changed protected action.
- [ ] Database migrations are reversible or have an explicit rollback/restore plan.
- [ ] An ADR exists if a project default changes.
