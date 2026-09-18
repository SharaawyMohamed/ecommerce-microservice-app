# Microservices E‑Commerce (Monorepo)

High-level, practical README for the Microservices E‑Commerce sample solution. It explains architecture, how to run locally (dotnet / Docker), configuration keys, important implementation patterns (CQRS / MediatR / Mapster / FluentValidation), troubleshooting tips and recommended next steps.

---

## Project summary

- Tech stack: .NET 10, ASP.NET Core Web API, MongoDB (Catalog), Redis (Basket cache), Docker Compose
- Architectural patterns: Clean layered service per microservice (Core / Application / Infrastructure / API), CQRS with MediatR, mapping with Mapster, validation with FluentValidation
- Repo layout (important folders):
  - `Services/Catalog` — Catalog service (MongoDB-backed product catalogue)
  - `Services/Basket` — Basket service (Redis-backed shopping cart)
  - `docker-compose.yml` — development compose file that brings up mongo, redis and services

---

## Quick start (recommended)

Prerequisites

- .NET 10 SDK
- Docker Engine (for docker compose) or local MongoDB/Redis if running services standalone
- Optional: Postman or curl

Run everything with Docker Compose (recommended for development):

```powershell
# from repository root (Microservices-ECommerce)
docker compose up --build
```

After compose finishes:

- Basket API: http://localhost:8080 (Swagger UI: http://localhost:8080/swagger)
- Catalog API: http://localhost:8081 (Swagger UI: http://localhost:8081/swagger)

Run a single service locally (without Docker compose)

```powershell
dotnet build
dotnet run --project Services/Basket/Basket.API
dotnet run --project Services/Catalog/Catalog.API
```

If running a service locally, ensure required environment variables or appsettings are present (see Configuration section).

---

## Configuration keys

Set configuration in appsettings.{Environment}.json or environment variables. Examples:

- Catalog service (MongoDB):
  - `MongoDbSettings:ConnectionString` — e.g. `mongodb://mongo:27017` or `mongodb://127.0.0.1:27017`
  - `MongoDbSettings:DatabaseName` — e.g. `CatalogDB`

- Basket service (Redis/cache):
  - `CacheSettings:ConnectionString` or connection string name `Redis` — e.g. `redis:6379` or `127.0.0.1:6379`

When running with docker-compose the compose file already injects `MongoDbSettings__ConnectionString`, `MongoDbSettings__DatabaseName` and `CacheSettings__ConnectionString` for the containers.

---

## APIs & sample requests

Catalog (sample)

- GET all products
  - GET `/api/catalog`

Basket

- Get basket
  - GET `/api/basket/{userName}`

- Create or update basket
  - POST `/api/basket`
  - Body (JSON):

```json
{
  "userName": "alice",
  "items": [
	{ "productId":"123", "productName":"Widget", "price":9.99, "quantity":2, "imageFile":"/img.png" }
  ]
}
```

- Delete basket
  - DELETE `/api/basket/{userName}`

Use Swagger UI to explore each API: `/swagger` on the service port.

---

## Implementation notes (important building blocks)

- Layering per service
  - `*.Core` — domain entities and repository interfaces
  - `*.Application` — MediatR handlers, DTOs, validators, mapping (this repo uses Mapster), business logic
  - `*.Infrustructure` — concrete adapters (Mongo, Redis), DI registration
  - `*.API` — controllers, routing, Swagger, minimal mapping to application models

- Mapping: Mapster is used. Application layer owns mapping from domain -> response DTOs. The API layer performs minimal DTO -> domain mapping for incoming requests.

- CQRS: Commands and Queries implemented with MediatR. Handlers return a shared `BaseResponse` (Success/Failure + Data).

- Validation: FluentValidation validators live in the Application layer per command/query. Consider registering a MediatR pipeline behavior to execute validators automatically.

---

## Database seeding

Catalog service includes a seeder (`DbInitializer.Seeder`) that inserts data from `Catalog.Infrustructure/DataCollections/*.json` on first run when `UseDatabaseSeeding()` is invoked (Catalog API program calls it in development start-up). If you use Docker Compose, the compose-provided mongo instance will be seeded automatically when the service runs.

---

## Troubleshooting (common issues)

- Redis ArgumentNullException on startup
  - Cause: missing Redis connection configuration. Ensure `CacheSettings:ConnectionString` or `ConnectionStrings:Redis` is set, or run Redis locally / in docker compose.

- No products returned from Catalog
  - Ensure Catalog is connected to the right MongoDB instance and that the seeded collection is named `Products` (the seeder uses that collection name).

- Ports conflict
  - Compose maps Basket to `8080` and Catalog to `8081` by default. Change ports in `docker-compose.yml` or VS launchSettings if needed.

---

## Testing

- Unit tests: look under `Services/*/*.Tests` (Catalog.Application.Tests exists). Run tests with `dotnet test`.

---

## CI/CD recommendations

- Build images and run tests on each PR
- Push service images to a container registry and deploy via Kubernetes/Helm to staging/prod
- Scan images and dependencies for vulnerabilities

---

## Contributing

- Keep controller code thin. Put business logic, validation, mapping, and persistence into the Application & Infrastructure projects.
- Add unit tests for handlers and integration tests for API endpoints.

---

## Suggested next improvements

- Add MediatR validation pipeline to auto-run FluentValidation validators
- Add health checks and readiness endpoints for each service
- Add OpenTelemetry tracing and centralized logging (Serilog + ELK/Seq)
- Add Helm charts and CI pipeline (GitHub Actions) to build/publish images

---

If you want, I can generate a more detailed README with automated run scripts, example requests + Postman collection, and a CI workflow file.
