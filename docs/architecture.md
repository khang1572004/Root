# Architecture

## Default: modular monolith with clean boundaries

`Api` owns HTTP concerns. `Application` owns use cases and ports. `Domain` owns business rules. `Infrastructure` owns PostgreSQL and external implementations.

Dependencies point inward:

```text
Api -> Application -> Domain
Api -> Infrastructure -> Application + Domain
```

The API must not query `DbContext` directly. Infrastructure must not contain business decisions. Keep each feature together by folder as the codebase grows; do not create layers only to move DTOs around.

## When to deviate

Use a separate deployable service only when independent deployment, ownership, security isolation, or scaling is demonstrably needed. Record the integration contract and operational cost first.
