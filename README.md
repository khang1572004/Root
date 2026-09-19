# .NET Backend Kit

Personal playbook and reusable starter for backend services built with ASP.NET Core on .NET 10.

## What is here?

- `docs/`: the defaults to follow when building a service.
- `checklists/`: short, repeatable pre-flight lists for a new service, pull request, and release.
- `adr/`: small records that explain important template decisions.
- `template/`: a runnable Clean Architecture starter with PostgreSQL, tests, Docker, and CI.

## Use it for a new project

1. Create a repository from this repository's GitHub template, or clone it.
2. Rename the projects, solution, namespaces, and Docker service from `Service` to the new service name.
3. Copy `template/.env.example` to `template/.env`, set a safe local password, and replace the example generic JWT issuer and audience when authentication is enabled.
4. Start PostgreSQL with `docker compose -f template/docker-compose.yml up -d db`.
5. Follow [the new-service checklist](checklists/new-service.md).

The `template/` directory is deliberately small. Add optional components only when the project needs them; document that choice in `adr/`.

## Conventions

- New architecture or technology decisions get an ADR.
- Every guideline says its default, when to deviate, and a checklist.
- Do not commit secrets, real connection strings, customer data, or production exports.
- Keep package versions centralized in `template/Directory.Packages.props`.
- Treat `template/docker-compose.yml` as a local-development tool, not a production deployment manifest.

## Maintenance cadence

Review the dependency and security guidance monthly, and review all documents when moving to a new .NET major version.
