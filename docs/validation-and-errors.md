# Validation and errors

## Default flow

1. Reject malformed request data at the endpoint.
2. Enforce business invariants in the domain/application layer.
3. Map known errors to stable HTTP responses.
4. Map unknown errors to a generic `500` response with a trace ID.

Return field errors in a predictable `errors` extension. Log unexpected exceptions with the trace ID, but never return implementation details to callers.

## Checklist

- Is every external input length-limited and type-checked?
- Does the error response tell a client what it can safely fix?
- Could the response or log expose a secret or personal data?
