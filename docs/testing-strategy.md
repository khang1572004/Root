# Testing strategy

## Test pyramid

- Unit tests: business rules, mapping, and edge cases; fast and isolated.
- Integration tests: endpoint routing, serialization, authentication, and PostgreSQL behavior.
- End-to-end tests: only the highest-value journeys across deployed components.

## Defaults

Every bug fix begins with a regression test when practical. Test observable behavior, not private implementation details. Use a real ephemeral PostgreSQL container for database behavior once the project has persistence-critical features; keep those tests separate from fast unit tests.

Coverage is a signal, not a target. Critical authorization, financial, and destructive paths need explicit tests regardless of percentages.
