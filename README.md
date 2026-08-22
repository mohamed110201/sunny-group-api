# Sunny Group Developers — Real Estate Platform Backend API

**Production backend for a live real estate business, built with ASP.NET Core**

This is the backend API powering the Sunny Group Developers platform — a real estate business based in Sharm El Sheikh, Egypt. It handles everything from property listings to authentication to multilingual content, and runs in production serving real users.

## What it does

- Serves property and real estate data through a REST API
- Handles secure user authentication with JWT + refresh token rotation
- Supports multiple languages for content (Arabic/English) so listings and pages can be localized cleanly
- Validates every incoming request before it touches business logic
- Deploys automatically on every push through a CI/CD pipeline straight to the production server

## Architecture

```
Client (Web/Mobile)
        │
        ▼
   Nginx (reverse proxy, SSL termination)
        │
        ▼
  ASP.NET Core Web API
        │
   ┌────┼────┐
   ▼    ▼    ▼
 CQRS  Auth  Validation
(MediatR) (JWT)  (FluentValidation)
        │
        ▼
   EF Core ──► PostgreSQL
```

- **API layer:** ASP.NET Core Web API, structured with Clean Architecture (Domain → Application → Infrastructure → API)
- **CQRS:** Commands and queries separated via MediatR, keeping write and read logic independent
- **Validation:** FluentValidation pipeline behavior validates every command/query before it reaches a handler
- **Auth:** JWT access tokens with httpOnly cookies, plus refresh token rotation for secure, long-lived sessions
- **Multilingual content:** Localized fields modeled as owned value objects (`LocalizedText`), so entities carry translations without extra tables sprawling everywhere
- **Media messaging:** Implemented customized media storage service (simulating something like Cloudinary)
- **Data layer:** EF Core over PostgreSQL
- **Infrastructure:** Deployed on a Linux VPS, reverse-proxied through Nginx, SSL via Certbot, running as a systemd service
- **CI/CD:** GitHub Actions builds, tests, and deploys on push to main

## Tech stack

| Layer | Tech |
|---|---|
| Backend | ASP.NET Core Web API |
| Architecture | Clean Architecture, CQRS |
| Validation | FluentValidation |
| ORM | EF Core |
| Database | PostgreSQL |
| Auth | JWT, httpOnly cookies, refresh token rotation |
| Infrastructure | Linux VPS (Ubuntu), Nginx, Certbot (SSL) |
| CI/CD | GitHub Actions |


## Project structure

```
sunny-group-api/
├── src/
│   ├── Domain/           # Entities, value objects, domain logic
│   ├── Application/      # CQRS commands/queries, validators, interfaces
│   ├── Infrastructure/   # EF Core, external services, implementations
│   └── Api/               # Controllers, middleware, startup config
├── tests/
└── README.md
```

## Deployment

Every push to `main` triggers a GitHub Actions workflow that builds, runs tests, and deploys to the production VPS. Nginx handles SSL termination and reverse-proxies traffic to the app running under systemd, which keeps it alive and auto-restarts on failure.

## Status

Live in production, actively maintained. Sole backend developer on the project — handling feature development, bug fixes, deployments, and infrastructure.

## Try It Yourself 

Link : https://www.sunnygroupdevelopers.com/en 

this link is for the clients to explore and interact with the platform , the dashboard that manages the content is designed only for the website admins
