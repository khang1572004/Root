# Engineering principles

## Defaults

1. Prefer a simple, observable modular monolith over premature distributed systems.
2. Make invalid states difficult to represent in the domain.
3. Treat API contracts, database migrations, and authorization rules as public changes.
4. Make failures actionable: return safe problem details and log enough context to investigate.
5. Automate repeatable quality checks in CI.
6. Keep credentials and customer data out of source control, logs, and error responses.

## When to deviate

Deviation is valid when a measurable requirement conflicts with a default. Record the requirement, alternatives, and consequence in an ADR.
