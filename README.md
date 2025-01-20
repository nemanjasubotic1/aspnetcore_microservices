# .NET 8 Microservices

This repository demonstrates a .NET 8 microservices architecture application showcasing various practices and technologies.

## Overview

This project implements a simplified e-commerce-like scenario. It demonstrates how to build and integrate multiple microservices using .NET 8, communicating via HTTP and gRPC, and leveraging various architectural patterns and infrastructure components.

## Architecture

The application follows a microservices architecture with the following key features:

*   **Communication:**
    *   **HTTP:** Standard HTTP calls for synchronous communication between services.
    *   **gRPC:** High-performance gRPC.
*   **Data Persistence:**
    *   **MS SQL Server:** Used for relational data storage in some services.
    *   **PostgreSQL with JSON:** Utilizing PostgreSQL's JSON capabilities for flexible data storage in other services.
    *   **SQLite:** Used for lightweight local data storage.
*   **Architectural Patterns:**
    *   **Repository Pattern:** Abstraction layer for data access.
    *   **CQRS (Command Query Responsibility Segregation):** Separating read and write operations for improved performance and scalability.
    *   **MediatR:** Implementing CQRS and in-process messaging.
    *   **Mediator Pipeline Behaviors:** Implementing cross-cutting concerns (e.g., validation, logging).
    *   **Decorator Pattern (with Scrutor):** Applying decorators for enhanced functionality.
*   **Infrastructure:**
    *   **Caching (Redis):** Distributed caching for improved performance.
    *   **Messaging:**
        *   **Azure Service Bus:** Cloud-based messaging for reliable asynchronous communication.
        *   **RabbitMQ:** On-premise or containerized messaging solution.
    *   **API Gateway (Ocelot):** Centralized entry point for external requests, handling routing, authentication, and other cross-cutting concerns.
*   **Security:**
    *   **Authentication (Microsoft Identity Library):** Secure authentication using standard protocols.
    *   **JWT (JSON Web Tokens):** Token-based authentication for API access.
*   **Frontend:**
    *   **Simple MVC Application:** Demonstrates how to consume the microservices using Refit (for HTTP) and gRPC.
*   **Exception Handling:** Centralized and consistent exception handling across services.


## Technologies Used

*   .NET 8
*   ASP.NET Core Minimal APIs
*   Entity Framework Core 
*   SQLite
*   Marten
*   Npgsql
*   Refit
*   gRPC
*   MediatR
*   Scrutor
*   Redis
*   Azure Service Bus
*   RabbitMQ
*   Ocelot
*   Microsoft Identity Library
*   JWT
