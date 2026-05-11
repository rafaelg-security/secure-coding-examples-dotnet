# Secure Coding Notes

## Core Principles

- validate input at boundaries
- never trust client-supplied identifiers or roles
- use parameterized queries
- avoid leaking stack traces
- do not log secrets, tokens, passwords, or sensitive medical data
- use DTOs instead of binding directly to domain entities
- use least privilege
- make security controls visible in code and documentation

## AppSec Review Questions

- What input can an attacker control?
- What data is sensitive?
- Where are authorization decisions made?
- Can users access another user's data?
- Are secrets exposed in logs or configuration?
- Is error handling leaking implementation details?
- Are dependencies scanned?
