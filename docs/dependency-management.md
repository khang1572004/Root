# Dependency management

## Defaults

- Use framework features before adding a library.
- Put NuGet versions in `Directory.Packages.props`.
- Add a package only with a clear owner, purpose, and removal condition.
- Review transitive dependencies and security advisories.
- Let Dependabot open updates, then verify build, tests, and release notes.

## Optional recipes, not defaults

Redis, message queues, Hangfire, MediatR, FluentValidation, OpenTelemetry exporters, and cloud SDKs are useful only for specific requirements. Introduce one with an ADR and a focused integration test.
