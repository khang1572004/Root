# Start here

## Default stack

- .NET 10 / ASP.NET Core
- PostgreSQL with EF Core and Npgsql
- Modular monolith with Clean Architecture boundaries
- REST API with OpenAPI and RFC 7807-style problem responses
- Docker Compose for local dependencies
- GitHub Actions for build, test, formatting, dependency updates, and CodeQL

## First hour of a new service

1. Use the starter and rename `Service` consistently.
2. Complete [the new-service checklist](../checklists/new-service.md).
3. Write the first ADR if a default no longer fits.
4. Define API contracts and failure cases before implementing endpoints.
5. Add the smallest vertical slice plus its tests.

## Non-goals of the baseline

It does not mandate microservices, CQRS libraries, a message broker, cache, job runner, or a specific cloud. Those are earned by a concrete requirement.
