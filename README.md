# Forum API

## Overview

Forum is a backend REST API built with C# and ASP.NET Core. It provides functionality for user management, authentication, forum posts and comments.

The project follows a layered architecture, separating Controllers, Services, Repositories, Models and other application components.

It also includes a separate NUnit project containing automated unit tests.

## Technologies

- C#
- .NET 10
- ASP.NET Core Web API
- JWT
- Dependency Injection
- Repository Pattern
- NUnit
- Moq

## Features

- User registration and login
- JWT token generation
- Password hashing
- User roles
- User management
- Post CRUD operations
- Comment CRUD operations
- DTOs and object mapping
- Input validation and exception handling
- Unit testing with NUnit and Moq

## Architecture

```text
Forum/
├── Controllers/
├── DTOs/
├── Exceptions/
├── Mappers/
├── Models/
├── Repositories/
├── Security/
├── Services/
└── Forum.Tests/
    ├── ControllersTests/
    ├── MappersTests/
    ├── RepositoriesTests/
    ├── SecurityTests/
    └── ServicesTests/
```

## Automated Testing

The solution includes a separate NUnit test project using Moq for dependency isolation.

Tests cover:

- Controllers
- Services
- Repositories
- Mappers
- Security components

Run the tests:

```bash
dotnet test Forum.Tests/Forum.Tests.csproj
```

Build the solution:

```bash
dotnet build Forum.sln
```

## Current Project Scope

The project currently uses in-memory collections for data storage. Data is not persisted between application restarts.

Database integration, API documentation with Swagger/OpenAPI and expanded API integration testing are potential future improvements.

## Getting Started

Requirements: .NET 10 SDK.

Clone the repository:

```bash
git clone https://github.com/MihailElefterius/Forum.git
cd Forum
```

Restore dependencies:

```bash
dotnet restore
```

Run the application:

```bash
dotnet run --project Forum.csproj
```

Configure the JWT signing key using .NET User Secrets or an environment variable. Do not commit production signing keys to the repository.

## Repository

[GitHub - Forum](https://github.com/MihailElefterius/Forum)
