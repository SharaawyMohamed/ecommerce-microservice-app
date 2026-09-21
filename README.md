# Microservices E-Commerce (Microservices-ECommerce)

One-stop monorepo containing small example microservices implemented with .NET 10.

Core ideas: each service follows a 4-layer pattern (Core / Application / Infrastructure / API). The project uses CQRS (MediatR), Mapster mappings, FluentValidation, and Docker Compose for local development.

Contents (services)
- Services/Catalog — product catalogue (MongoDB)
- Services/Basket — shopping basket (Redis cache)
- Services/Discount — coupon service (Postgres / Dapper)

Quick navigation
- docker-compose.yml — development compose that brings up mongodb, redis, postgres and the APIs
- README.md — this file

Table of contents
- Getting started
- Development (run single service)
- Configuration
- APIs & endpoints
- Implementation notes
- Troubleshooting
- Contributing

## Getting started (docker-compose)

Prerequisites
- Docker Engine (or Docker Desktop)
- .NET 10 SDK (when running services locally)

Bring up everything locally (recommended):

```powershell
# from repository root (Microservices-ECommerce)
docker compose up --build
```

After compose is ready the default local ports are:
- Basket API: http://localhost:8080 (Swagger: /swagger)
- Catalog API: http://localhost:8081 (Swagger: /swagger)
- Discount API: http://localhost:8082 (Swagger: /swagger)

## Run a single service locally (dev)

```powershell
dotnet build
dotnet run --project Services/Catalog/Catalog.API
dotnet run --project Services/Basket/Basket.API
dotnet run --project Services/Discount/Discount.API
```

When running from Visual Studio the API launch profiles open Swagger automatically in Development (launchUrl: swagger).

## Configuration

Set values in appsettings.Development.json or environment variables. Common keys used by services:

- Catalog (Mongo):
  - `MongoDbSettings:ConnectionString` (e.g. mongodb://mongo:27017)
  - `MongoDbSettings:DatabaseName` (e.g. CatalogDb)

- Basket (Redis):
  - `CacheSettings:ConnectionString` (e.g. redis:6379)

- Discount (Postgres):
  - `DatabaseSettings:ConnectionString` (e.g. Host=postgres;Port=5432;Username=postgres;Password=postgres;Database=DiscountDb)

The docker-compose file injects environment variables for these settings when running the compose stack.

## APIs & sample requests

### Catalog
- GET /api/catalog         — list products
- GET /api/catalog/{id}    — get product by id

### Basket
- GET /api/basket/{userName}
- POST /api/basket         — body: shopping cart JSON
- DELETE /api/basket/{userName}

### Discount
- GET /api/discount/{productName}
- POST /api/discount       — create coupon
- PUT /api/discount        — update coupon
- DELETE /api/discount/{productName}

Use Swagger UI on each service to explore request/response shapes.

## Implementation notes

- Project structure per service:
  - Service.Core: entities and repository interfaces
  - Service.Application: MediatR handlers, DTOs, validators, mapping (Mapster)
  - Service.Infrustructure: persistence adapters (Mongo, Redis, Postgres), DI registrations
  - Service.API: controllers, swagger, composition root

- Mapping: Mapster mappings live in the Application layer. API layer performs minimal mapping from API DTOs to domain models for incoming requests.

- Validation: validators (FluentValidation) are implemented per command/query in Application. Consider adding a MediatR pipeline to auto-run validation.

- Persistence:
  - Catalog uses MongoDB and includes a DbInitializer seeder that loads JSON sample data into Products/Brands/Types on first run.
  - Basket uses Redis (IDistributedCache) to store ShoppingCart JSON.
  - Discount uses Postgres + Dapper. Docker compose runs an init SQL (init.sql) to create the Coupon table.

## Troubleshooting (common issues)

- Redis connection error on Basket startup
  - Ensure `CacheSettings:ConnectionString` is set or run Redis (docker compose starts redis:6379).

- No products returned from Catalog
  - Confirm Catalog is pointing to the same Mongo instance as the seeder (collection name: `Products`).

- Postgres init not applied
  - If Postgres volume already exists the init scripts are not re-run. Remove the postgres_data volume and restart compose to apply init.sql.

- Docker compose validation errors
  - Run `docker compose -f docker-compose.yml -f docker-compose.override.yml config` to validate combined configuration and detect YAML issues.

## Development tips

- Keep controllers thin — business logic belongs in Application handlers.
- Add unit tests for handlers and integration tests for API endpoints (use TestServer or testcontainers).
- Add MediatR validation pipeline to run FluentValidation automatically.

## CI / Production notes

- Use a production-grade migration tool for Discount/Postgres (Flyway, DbUp, or EF Core Migrations) instead of relying on docker-entrypoint init scripts.
- Use secrets manager (Vault, Azure Key Vault) for production DB/Redis credentials.
- Add health checks and readiness/liveness probes when moving to Kubernetes.

## Contributing

- Follow repository code style and unit test practices.
- Keep API contracts stable; version APIs when breaking changes are introduced.

## Further help

If you want I can:
- Add a Postman collection and example curl scripts under `/dev`.
- Add GitHub Actions workflow to build, test, and publish container images.
- Add MediatR validation pipeline and Mapster registration in DI for all services.
