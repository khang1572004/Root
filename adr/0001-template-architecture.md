# ADR 0001: Start with a modular monolith and Clean Architecture boundaries

**Status:** Accepted  
**Date:** 2026-09-19

## Context

New backend ideas need a reusable baseline without assuming distributed systems, a queue, cache, or a cloud provider.

## Decision

The starter uses four projects: API, Application, Domain, and Infrastructure. It starts as a modular monolith and uses PostgreSQL through EF Core/Npgsql.

## Consequences

Business code remains testable without HTTP or database infrastructure. The project has a small up-front boundary cost. New deployment units or technologies require an ADR rather than becoming accidental dependencies.
