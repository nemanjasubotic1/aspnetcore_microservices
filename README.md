# .NET 8 Microservices

This repository demonstrates a .NET 8 microservices architecture application showcasing various practices and technologies.

## Overview

This project implements a simplified e-commerce-like scenario. It demonstrates how to build and integrate multiple microservices using .NET 8, communicating via HTTP and gRPC, and leveraging various architectural patterns and infrastructure components. The project also includes Docker Compose configuration for easier deployment and development.

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
    *   **Mediator Pipeline Behaviors:** Implementing cross-cutting concerns ("Fluent validation").
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
    *   **Simple MVC Application:** Demonstrates how to consume the microservices using Refit (for HTTP) and gRPC. Uses Data Annotations for basic frontend validation and Fluent Validation implemented via MediatR pipeline behaviors for more complex validation scenarios.
*   **Exception Handling:** Centralized and consistent exception handling across services.


## Technologies Used

*   .NET 8
*   ASP.NET Core Minimal APIs
*   ASP.NET Core MVC
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


## Getting Started

This application can be run directly (outside Docker) or using Docker Compose.

**1. Configuration:**

Configuration for database connections, messaging services, Redis, and authentication is handled as follows:

*   **Directly (Outside Docker):** Use `appsettings.json` files for configuration. Feature Management is also available via configuration.
*   **Docker Compose:** Use `.env` files to set environment variables within the containers. Create the following `.env` files (matching the names used in `docker-compose.override.yml`) in the same directory as `docker-compose.yml`:
    *   `.env.authapi`
    *   `.env.couponapi`
    *   `.env.emailapi`
    *   `.env.orderingapi`
    *   `.env.productapi`
    *   `.env.shoppingcartapi`
    *   `.env.webapp`

    Populate these files with your values for variables found in the `appsettings.json` file for each corresponding project.
    When running with Docker Compose, the Ocelot API Gateway uses `ocelot_docker.json`. You will need to manually switch between `ocelot.json` and `ocelot_docker.json` depending on your chosen running environment.

**2. Running the Application:**

*   **Directly (Outside Docker):**
    1.  Install the .NET 8 SDK.
    2.  Configure `appsettings.json`.
    3.  Ensure `ocelot.json` is in use.
    4.  Build and run the projects

*   **Using Docker Compose:**
    1.  Install Docker and Docker Compose.
    2.  Configure the `.env` files.
    3.  Ensure `ocelot_docker.json` is in use.
    4.  Navigate to the directory containing `docker-compose.yml`.
    5.  Build: `docker-compose build`
    6.  Run: `docker-compose up -d` (detached mode)
    7.  Stop: `docker-compose down`
