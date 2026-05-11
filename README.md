# secure-coding-examples-dotnet

A .NET 10 secure coding portfolio project demonstrating common enterprise AppSec mistakes and safer implementation patterns.

## Purpose

This repository supports a cybersecurity transition profile focused on:

- secure engineering
- Application Security
- OWASP Top 10
- OWASP API Top 10
- secure SDLC
- regulated-system thinking
- enterprise .NET security

## Safety Note

The intentionally insecure examples are educational and isolated. They demonstrate anti-patterns so the secure version can be compared clearly. Do not reuse insecure examples in production.

## What This Project Demonstrates

- input validation
- SQL injection prevention
- secure authentication patterns
- secure password hashing
- secure logging
- safe error handling
- mass assignment prevention
- secure file upload validation
- dependency and CI security awareness

## Tech Stack

- .NET 10
- ASP.NET Core 10 Web API
- SQLite
- Dapper
- FluentValidation
- BCrypt.Net
- xUnit
- GitHub Actions
- Docker

## Run Locally

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/SecureCodingExamples.Api
```

Swagger:

```text
https://localhost:5001/swagger
```

## Portfolio Positioning

This project shows security reasoning from a software engineering perspective: identifying risky implementation patterns and applying secure coding practices in enterprise .NET systems.
