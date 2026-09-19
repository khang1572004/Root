# Service starter

## Prerequisites

- .NET 10 SDK
- Docker Desktop, for local PostgreSQL
- The EF CLI tool (`dotnet tool install --global dotnet-ef`), only when creating or applying migrations locally

## Run locally

```powershell
Copy-Item .env.example .env
# Edit .env: set a local-only password and configure Jwt:Authority/Jwt:Audience
docker compose up -d db
dotnet restore Service.sln
dotnet ef database update --project src/Service.Infrastructure --startup-project src/Service.Api
dotnet run --project src/Service.Api
```

The API runs at the URL printed by `dotnet run`. In Development, OpenAPI is exposed at `/openapi/v1.json`. All business endpoints require a valid JWT bearer token with a `tenant_id` claim. Health checks (`/health/live`, `/health/ready`) are anonymous.

The initial migration is included in the template. When adding features, create new migrations with:

```powershell
dotnet ef migrations add MigrationName --project src/Service.Infrastructure --startup-project src/Service.Api
```

## Test and format

```powershell
dotnet format Service.sln --verify-no-changes
dotnet test Service.sln
```

`/health/live` confirms the process is running without querying PostgreSQL. `/health/ready` also verifies database connectivity.

## Run with Docker

After adding and applying the first migration, start the complete local stack:

```powershell
docker compose up --build
```

The API is then available on `http://localhost:8080`. Compose is for local development only: both published ports bind to loopback, and its `Development` environment setting is not a production default. The starter intentionally does not apply migrations at API startup; production deployments should run a reviewed migration bundle explicitly.

## Production image and migrations

The runtime image runs as the dedicated unprivileged `app` user and includes a liveness health check. Deploy it with a read-only root filesystem when the platform supports it; provide a writable temporary mount only when the runtime requires one.

Build the API image and extract a reviewed migration bundle as separate release artifacts:

```powershell
docker build --target final --tag service-api:release .
docker build --target migration-bundle --output type=local,dest=./artifacts/migrations .
```

Run the bundle before deploying the API, using a database principal that can perform schema changes. Configure the API with a separate least-privilege runtime database principal. Supply the connection string, generic JWT issuer (`Jwt__Authority`), JWT audience (`Jwt__Audience`), and other environment-specific settings through the deployment platform's secret/configuration facilities. See [Docker and deployment](../docs/docker-deployment.md).

## Rename for a real service

Replace `Service` in project names, namespaces, solution, Docker labels, and documentation before creating domain features. Then complete the repository-level [new-service checklist](../checklists/new-service.md).
