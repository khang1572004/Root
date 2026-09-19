# Release checklist

- [ ] CI, CodeQL, and dependency advisories are reviewed.
- [ ] The deployment image is built from the reviewed commit.
- [ ] Runtime configuration and secret references are present in the target environment.
- [ ] Database migration impact and rollback/restore plan are reviewed.
- [ ] Readiness, liveness, logs, dashboards, and alerts are verified.
- [ ] API compatibility and consumer communication are handled.
- [ ] A smoke test is ready for immediately after deployment.
