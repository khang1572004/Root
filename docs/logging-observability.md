# Logging and observability

## Defaults

- Emit structured JSON logs in deployed environments.
- Include trace/correlation IDs in logs and safe problem responses.
- Log events and identifiers, not full request bodies, credentials, access tokens, or sensitive personal data.
- Expose liveness and readiness checks separately.
- Start with application logs and health checks; add metrics/traces via OpenTelemetry when operating the service requires them.

## Useful events

Log service start/stop, unhandled exceptions, dependency failures, authorization denials when relevant, and completed background work. Keep log levels intentional so alerts are meaningful.
