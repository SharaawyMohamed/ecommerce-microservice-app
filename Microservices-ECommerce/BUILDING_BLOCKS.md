Developer ergonomics, best practices and building blocks for this microservices solution

1) DTOs and Mapping (API vs Domain)
- Keep API surface models (DTOs) inside the API project (Services/*/*API/Models). Do not expose domain entities across service boundaries.
- Use application-layer response/request models for use-cases (Services/*/*Application/Features/**/Responses and Requests).
- Map between DTOs, application models and domain entities explicitly. Prefer Mapster/AutoMapper in the Application layer for queries/commands and small manual mapping in controllers for simple cases.

2) Testing & Contracts
- Add unit tests for business logic in the Application project.
- Add integration tests that exercise the API endpoints using TestServer and an in-memory or test container (mongo, redis) in CI.
- Add contract tests for async messages (if using events) so consumers and producers agree on shape and versioning.

3) Seeders and Dev data
- Provide seeders in the Infrustructure project (for Catalog we already have DbInitializer). Use environment-driven seeding for dev only.
- Compose adds mongo and redis volumes. Add optional init scripts (mongo-init) or use a small seeder task at app startup in Development.

4) CI, Code Style and Pre-commit
- Add a CI pipeline to build, run tests, and publish images.
- Enforce code style and analyzers in csproj. Use formatters in dev workflows.

5) Building blocks (recommended project structure per service)
- ServiceName.Core (domain entities, interfaces)
- ServiceName.Application (use-cases, DTOs, MediatR handlers, mapping)
- ServiceName.Infrustructure (persistence, third-party adapters, DI)
- ServiceName.API (controllers, API DTOs, swagger, composition root)

6) Examples in this repo
- Basket.Application contains response DTOs and mapping definitions.
- Basket.Infrustructure implements IBasketRepository against Redis.
- docker-compose.yml contains dev dependencies (mongo, redis) and service definitions.

Checklist to apply point 10 now
- Created API DTOs for Basket (ShoppingCartDto and ShoppingCartItemDto).
- Controller maps between API DTOs and domain/application models.
- Added this BUILDING_BLOCKS.md file documenting recommended patterns and next steps.

If you want, I can scaffold the same API DTOs and controllers for other services, add unit test project templates, and add a simple CI workflow file.
