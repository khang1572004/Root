# Data access and migrations

## PostgreSQL defaults

- Use EF Core migrations; commit every migration with the code that requires it.
- Store timestamps as `DateTimeOffset` in UTC.
- Use database constraints for important invariants, not only application validation.
- Add indexes based on real query paths and verify them with query plans.
- Do not run destructive migrations automatically without a reviewed deployment plan.

## Commands

From `template/`:

```powershell
dotnet ef migrations add MeaningfulMigrationName --project src/Service.Infrastructure --startup-project src/Service.Api
dotnet ef database update --project src/Service.Infrastructure --startup-project src/Service.Api
```

Review generated migrations before committing them. Test an upgrade from a representative previous schema before production.
