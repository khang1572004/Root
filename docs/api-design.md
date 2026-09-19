# API design

## Defaults

- Use resource-oriented paths: `/api/todos`, not `/api/getTodos`.
- Use plural nouns, lower-case kebab-case paths, and JSON camelCase payloads.
- Validate at the HTTP boundary and again protect domain invariants in the application/domain layer.
- Return `201 Created` with a location for successful creation, `204 No Content` for successful deletion, and `404` for absent resources.
- Use `ProblemDetails` for errors. Do not leak stack traces, SQL, tokens, or internal hostnames.
- Publish OpenAPI in development and keep breaking contract changes deliberate.

## Pagination and filtering

Use explicit query parameters such as `offset`, `pageSize`, `sort`, and allowed filters. Cap `pageSize` at a server-enforced maximum (default 100). The template uses offset-based pagination with deterministic sorting by `CreatedAt DESC, Id DESC`. The response includes `offset`, `pageSize`, and `hasMore` to support stable page traversal. Return `ValidationProblemDetails` (`400`) for invalid pagination parameters.

For cursor-based pagination on high-volume endpoints, switch to keyset/cursor paging based on `(CreatedAt, Id)` and document the cursor format.

## Versioning

Avoid versioning an internal API before it has consumers. For public or long-lived APIs, pick one versioning strategy and document its deprecation window.
