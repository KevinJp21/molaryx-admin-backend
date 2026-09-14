# Molaryx Admin API

A production-oriented full-stack SaaS project built from scratch, covering frontend, backend, database, authentication, real-time notifications, containerization and cloud deployment.

This repository is the **backend**: a multi-tenant API for specialty clinics. One platform for patients, scheduling, appointments, clinical records, payments, procedures, treatments, and team, scoped per clinic (**tenant**).

ASP.NET Core (`molaryx-admin`, .NET 10) with **Clean Architecture**, a **custom CQRS mediator** (not MediatR), PostgreSQL, and EF Core.

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-EF%20Core-4169E1?logo=postgresql&logoColor=white)](https://www.postgresql.org/)

## Stack

| | |
|---|---|
| Runtime | ASP.NET Core · .NET 10 |
| Data | PostgreSQL · EF Core · Npgsql |
| Architecture | Clean Architecture · CQRS (custom mediator) |
| Auth | JWT + refresh · permission policies |
| Realtime | SignalR |
| Docs | OpenAPI · Scalar · Swagger |
| Email / files | Resend · PDFsharp/MigraDoc · ClosedXML |

## Product (API surface)

The API covers the clinic workflow from patient intake to billing:

- **Patients:** profile, identification, history linked to appointments and treatments
- **Appointments:** lifecycle statuses and overlap detection
- **Clinical records:** reason, diagnosis, evolution; PDF export
- **Procedures & treatments:** clinic catalogs, prices, duration, per-patient treatment plans
- **Payments:** charges tied to care, outstanding balances, Excel reports
- **Team:** owner, professional, assistant; granular permissions
- **Platform:** superadmin tenants, plans, and subscriptions
- **Notifications:** push via SignalR

REST is versioned as `api/v1/[controller]/[action]` (~14 controllers, CQRS features per module).

## Architecture

Single project, unidirectional dependencies:

```
Presentation → Application → Domain ← Infrastructure
Shared (ApiResponse, utils)
```

| Layer | Path | Responsibility |
|---|---|---|
| **Domain** | `Domain/` | Entities, enums, specifications, contracts, status rules, `PermissionCodes` |
| **Application** | `Application/` | CQRS features, FluentValidation, custom mediator, pagination, behaviors |
| **Infrastructure** | `Infrastructure/` | Services, repos, EF + Npgsql, email, PDF, SignalR, DI |
| **Presentation** | `Presentation/` | Controllers, JWT, exception handler, hubs |
| **Shared** | `Shared/` | `ApiResponse<T>` |

Infrastructure implements Domain contracts. Controllers have **no** business logic.

```
molaryx-admin/
├── Application/
│   ├── Features/                     # CQRS per business module
│   │   ├── Appointment/
│   │   ├── Auth/
│   │   ├── ClinicalRecord/
│   │   ├── Dashboard/
│   │   ├── Payment/
│   │   ├── Patients/
│   │   ├── PatientTreatment/
│   │   ├── Procedure/
│   │   ├── Treatment/
│   │   ├── Users/
│   │   └── Platform/Tenant/
│   ├── Common/Mediator/              # IRequest, IMediator, IPipelineBehavior
│   └── Behaviors/ValidationBehavior.cs
├── Domain/
│   ├── Entities/
│   ├── Specifications/
│   ├── Contracts/                    # IServices, IRepositories
│   ├── Common/                       # StatusRules (e.g. appointments)
│   ├── Constants/                    # PermissionCodes
│   └── Exceptions/
├── Infrastructure/
│   ├── Services/
│   ├── Persistence/                  # Configuration, Repositories, Seeds
│   ├── Pdf/
│   └── Email/
├── Migrations/                       # EF Core
├── Presentation/
│   ├── Controllers/                  # + Platform/
│   ├── Hubs/                         # SignalR
│   └── Handlers/
└── Shared/
```

Each feature follows the same convention: **Command or Query + Handler + Validator**. Commands delegate to `IXxxService`; queries use `IUnitOfWork` + specifications.

### Custom CQRS mediator

There is no MediatR. The mediator (`IRequest`, `IRequestHandler`, `IMediator`) is small, registers handlers by reflection, and owns the pipeline. `ValidationBehavior` runs FluentValidation before the handler and throws `CustomValidationException` with structured errors for `ApiResponse<T>`. Extra behaviors (logging, tenant) are just more `IPipelineBehavior` implementations.

```
Controller → IMediator.Send
  → ValidationBehavior (FluentValidation)
  → Handler
      Command: IXxxService (Infrastructure)
      Query:   ITenantAccessService + IUnitOfWork + Specification
  → ApiResponse<T>
```

| Type | Handler | Tenant | Persistence |
|---|---|---|---|
| **Command** | Thin → `IXxxService` | In the **service** (`RequireActiveAsync`) | Service + UoW / repos |
| **Query** | Logic in the handler | At the start of the **handler** | UoW + Spec (no write service) |

## Multi-tenant and permissions

Each clinic is a tenant with an active subscription. Business commands go through `ITenantAccessService.RequireActiveAsync` (tenant + subscription) before persist. Entities carry `IdTenant`; specifications filter by clinic.

Permissions are seeded per module (patients, appointments, payments, clinical records, team, platform, …) and assigned by role. Each code (`PermissionCodes.*`) maps to an authorization policy. Endpoints use `[Authorize(Policy = PermissionCodes.*)]`. Missing permission → **403**; the handler never runs. Authorization is server-side, not client-side.

## Lifecycle rules

Aggregates with status (appointments, patient treatments) expose **StatusRules** in Domain: transition graphs, `EnsureCanEdit`, `EnsureCanTransition`. Validators only check that the enum is defined; business rules stay in Domain.

## Persistence

- PostgreSQL, snake_case columns, soft-delete (`DeletedAt`), filtered uniques
- Specifications in Domain; thin repos on `BaseRepository`
- Central `IUnitOfWork` with one repo per aggregate

## Cross-cutting

- JWT auth with refresh, tenant registration, password reset, transactional email (Resend)
- SignalR hub: `/api/v1/hub/notifications` (`mark_as_viewed`, `mark_all_as_viewed`)
- Clinical-record PDF (PDFsharp/MigraDoc) and payment Excel (ClosedXML)
- OpenAPI + Scalar (development)

## Getting started

### Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [EF Core tools](https://learn.microsoft.com/ef/core/cli/dotnet) (`dotnet tool install --global dotnet-ef`)

### Configuration

Copy the development example and fill in secrets:

```bash
cp appsettings.Development.example.json appsettings.Development.json
```

| Key | Purpose |
|---|---|
| `ConnectionStrings:DefaultConnection` | PostgreSQL |
| `Jwt:Key` / `Issuer` / `Audience` | JWT |
| `Resend:ApiKey` | Transactional email |
| `App:FrontendUrl` | CORS origin |
| `App:ResetPasswordUrl` | Password-reset path on the frontend |

### Database (EF Core)

Create a migration (when the model changed):

```bash
dotnet ef migrations add <DescriptiveName>
```

Apply pending migrations:

```bash
dotnet ef database update
```

Production deploys can also run:

```bash
dotnet molaryx-admin.dll --migrate
```

That applies migrations and exits (used by the EC2 pipeline).

### Run

```bash
dotnet restore
dotnet run
```

Health check: `GET /` → `MolaryxApiAdmin Activo`.

In Development:

- Swagger UI: `/swagger`
- Scalar: `/scalar`

## CI/CD

Production deploys only on Git tags (`v*`) that point to a commit on `main`.

```
push tag v* → Docker image → GHCR → deploy via SSM on EC2
```

On the server (`/opt/molaryx`):

1. Backup PostgreSQL (`pg_dump`) and keep the previous `IMAGE_TAG`
2. Pull the new image, stop the API
3. Run EF migrations (`dotnet molaryx-admin.dll --migrate`)
4. Start the API and health-check `localhost:3000`

If migrate or health check fails: restore the dump, roll back the previous tag, restart. Deploy is all-or-nothing.

| | |
|---|---|
| Trigger | Tag `v*` on `main` |
| Image | Docker → GHCR |
| Host | EC2 · Docker Compose |
| Deploy | AWS SSM · IAM role |

## HTTP errors

| Exception | Status |
|---|---|
| `CustomValidationException` | 400 + errors |
| `InvalidOperationException` | 400 |
| `NotFoundException` | 404 |
| `AlreadyExistException` | 409 |

Validation and business messages are in Spanish.

## License

**All Rights Reserved.**

This project is published for portfolio and technical review purposes only.
See the [LICENSE](LICENSE) file for the full terms.
