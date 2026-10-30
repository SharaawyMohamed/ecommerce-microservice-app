# Ecommerce Microservice App

A modern e-commerce backend built as a microservices-based .NET solution, designed to demonstrate how independent services can work together to power a real-world shopping application.

This project is currently under active development and is meant to showcase clean architecture, service separation, containerized local setup, and production-minded backend design using .NET and Docker.

## Why this project?

This repository was created to explore and practice:
- microservice architecture in .NET
- service isolation and independent data stores
- layered application design
- Docker-based local development
- API-driven communication between services
- real-world e-commerce business flows

It is a learning-focused project, but it is structured in a way that can grow into a real-world backend foundation for a larger platform.

## Project goals

The goal is to build a simple but realistic e-commerce backend with:
- Catalog management
- Basket/cart operations
- Discount/coupon logic
- Order processing
- API documentation via Swagger
- service orchestration through Docker Compose

## Architecture

The solution is designed around a modular monorepo pattern where each domain has its own service, dependencies, and responsibilities.

```text
+-------------------+      +--------------------+
| Catalog Service   |      | Basket Service     |
| MongoDB           |      | Redis              |
+-------------------+      +--------------------+
         |                         |
         |                         |
         v                         v
+-------------------+      +--------------------+
| Discount Service  |      | Ordering Service   |
| PostgreSQL        |      | SQL Server         |
+-------------------+      +--------------------+
         |
         v
+-------------------+
| Shared Contracts  |
| EventBus.Messages |
+-------------------+
```

Each service follows a layered structure:
- Core
- Application
- Infrastructure
- API

This helps keep business logic clean, maintainable, and easier to evolve as the system grows.

## Tech stack

- .NET 8/9+ (depending on project setup)
- ASP.NET Core
- C#
- Docker & Docker Compose
- MongoDB
- Redis
- PostgreSQL
- SQL Server
- gRPC
- MediatR
- FluentValidation
- Mapster
- Dapper

## Repository structure

```text
ecommerce-microservice-app/
├── Microservices-ECommerce/
│   ├── Services/
│   │   ├── Catalog/
│   │   │   ├── Catalog.API
│   │   │   ├── Catalog.Application
│   │   │   ├── Catalog.Core
│   │   │   └── Catalog.Infrustructure
│   │   ├── Basket/
│   │   │   ├── Basket.API
│   │   │   ├── Basket.Application
│   │   │   ├── Basket.Core
│   │   │   └── Basket.Infrustructure
│   │   ├── Discount/
│   │   │   ├── Discount.API
│   │   │   ├── Discount.Application
│   │   │   ├── Discount.Core
│   │   │   └── Discount.Infrustructure
│   │   └── Ordering/
│   │       ├── Ordering.API
│   │       ├── Ordering.Application
│   │       ├── Ordering.Core
│   │       └── Ordering.Infrastructure
│   │
│   ├── Inftastructrue/
│   │   └── EventBus.Messages
│   │
│   ├── docker-compose.yml
│   ├── docker-compose.dev.yml
│   ├── docker-compose.prod.yml
│   ├── Microservices-ECommerce.slnx
│   ├── BUILDING_BLOCKS.md
│   └── launchSettings.json
│
├── README.md
├── .gitignore
└── ...
```

## Current services

### Catalog Service
Responsible for product management and catalog-related operations.

Features:
- product listing
- product details
- product filters and metadata
- MongoDB-backed persistence

### Basket Service
Handles customer cart functionality.

Features:
- add/remove/update cart items
- retrieve cart by username
- shopping cart persistence in Redis

### Discount Service
Manages coupons and discount calculations.

Features:
- get discount for product
- create/update/delete coupon
- PostgreSQL-backed storage

### Ordering Service
The order domain for processing customer purchases.

Features:
- order creation and tracking
- persistence with SQL Server
- future expansion for payment and order workflows

## Getting started

### Prerequisites

Before running the project, make sure you have:
- Docker Desktop or Docker Engine
- .NET SDK
- Git
- A terminal (PowerShell, Bash, or zsh)

### Clone the repository

```bash
git clone https://github.com/SharaawyMohamed/ecommerce-microservice-app.git
cd ecommerce-microservice-app
cd Microservices-ECommerce
```

### Run the full stack

```bash
docker compose up --build
```

This will start the application services and required infrastructure containers.

## Default ports

After startup, the services are exposed locally at:

| Service | Port | Swagger |
|--------|------|---------|
| Basket API | 8080 | http://localhost:8080/swagger |
| Catalog API | 8081 | http://localhost:8081/swagger |
| Discount API | 8082 | http://localhost:8082/swagger |
| Ordering API | 8083 | http://localhost:8083/swagger |

## Sample endpoints

### Catalog
- `GET /api/catalog`
- `GET /api/catalog/{id}`

### Basket
- `GET /api/basket/{userName}`
- `POST /api/basket`
- `DELETE /api/basket/{userName}`

### Discount
- `GET /api/discount/{productName}`
- `POST /api/discount`
- `PUT /api/discount`
- `DELETE /api/discount/{productName}`

## Local development experience

The project is designed for a smooth local development workflow:

- each service can run independently
- infrastructure dependencies are containerized
- no need to install all external systems manually
- Swagger is available for API exploration
- Docker Compose makes service startup fast and repeatable

## Configuration

Services use configuration values from appsettings files and environment variables. Common settings include:
- MongoDB connection string
- Redis connection string
- PostgreSQL connection string
- SQL Server connection string
- gRPC service URLs

These settings are wired through Docker Compose for local development.

## Important note

This project is still under development, so:
- features are evolving
- architecture may be refined
- some components may be incomplete or experimental
- improvements, refactoring, and additional services are planned

This is a strong foundation for learning and building a real microservice-based commerce platform.

## Roadmap

Planned improvements include:
- centralized API gateway
- authentication and authorization
- health checks
- distributed tracing
- CI/CD pipeline
- automated tests
- event-driven messaging improvements
- better order workflows and payment integration
- more production-ready configuration management

## Why this is a good portfolio project

This repository demonstrates:
- backend system design thinking
- real-world application structure
- multi-service architecture
- understanding of distributed systems
- containerized development practices
- service-level separation and maintainability
- practical .NET backend experience

For visitors, it gives a clear message: this project is not just a demo page — it is a backend system built with real engineering principles.

## Contributing

Contributions, suggestions, and ideas are welcome. Since the project is still evolving, collaboration is a great way to improve its architecture and quality.

## License

This project currently does not include a license file. If you plan to share it publicly as an open-source portfolio project, adding an appropriate license such as MIT is recommended.

## Final note

This project reflects a hands-on effort to build an e-commerce backend using modern microservice patterns and .NET tooling. It is a strong example of backend engineering, system design, and practical software architecture.

If you want, I can also generate:
1. a more premium GitHub-style README with badges and polished visuals,
2. a shorter portfolio-optimized version,
3. or a README specifically written for recruiters and hiring managers.
