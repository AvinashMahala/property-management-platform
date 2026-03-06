# Backend

This document describes the structure and development workflow for the .NET backend of the Property Management Platform.

## 📁 Repository Layout

The backend lives in `backend/` and contains a single solution and multiple projects in a `src` subdirectory:

```
backend/
├─ Directory.Build.props       # Shared build props (target framework, nullable, etc.)
├─ MyApp.sln                   # .NET solution file
└─ src/
   ├─ MyApp.Api               # ASP.NET Core Web API
   ├─ MyApp.Interfaces        # Interfaces and contracts
   ├─ MyApp.Models            # Domain and DTO models
   ├─ MyApp.Services          # Business logic services
   └─ MyApp.Repositories      # Data access layer
```

Each project is a standard .NET 10 class library (or web project in the case of the API).

## 🛠 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (or later) installed on your machine.
- Visual Studio 2022/2023, Visual Studio Code, or another IDE that supports .NET.
- A database engine if you plan to wire up repositories (SQL Server, PostgreSQL, etc.).

## 🚀 Building the Solution

Run the following from the `backend` folder:

```powershell
cd backend
dotnet restore
dotnet build
```

> **Formatting check:** a `Directory.Build.targets` file runs `dotnet format` before every build. Make sure the `dotnet-format` tool is installed globally (`dotnet tool install -g dotnet-format`) so local builds fail when code needs formatting.

The shared `Directory.Build.props` ensures all projects target `net10.0` with nullable reference types and implicit usings enabled.

## ▶ Running the API

The `MyApp.Api` project is the entry point.

```powershell
cd backend/src/MyApp.Api
dotnet run
```

By default the app launches in development mode. A root redirect has been configured so hitting the base address navigates to `/swagger`, which will show a blank Swagger UI until you add controllers/endpoints.

### Launch profiles

- `http` and `https` profiles are defined in `Properties/launchSettings.json` with ports 5291 and 7092 respectively.
- IIS Express profiles are also available when launching from Visual Studio (ssl port 44385). 

### Swagger

Swagger (via Swashbuckle) is enabled only in the Development environment. No endpoints are defined initially, so the UI is empty – this is intentional.

## 🧱 Project Responsibilities

Service registration and dependency injection are centralized within the `MyApp.Api` project via a `DependencyInjection.cs` static class. This extension method (`AddApplicationServices`) is invoked from `Program.cs` and is where you should add scoped/singleton/transient registrations for services and repositories.


- **MyApp.Interfaces** – define service interfaces, DTO contracts, and events. Do not depend on other projects.
- **MyApp.Models** – hold domain entities, view models, and shared data structures.
- **MyApp.Services** – business logic. Implement interfaces from `MyApp.Interfaces`. Can reference `MyApp.Models` and `MyApp.Repositories`.
- **MyApp.Repositories** – data access implementations (EF Core contexts, Dapper, etc.). Keep external dependencies here.
- **MyApp.Api** – presentation layer. Registers services via DI, configures middleware, and exposes HTTP endpoints.

## 📦 Adding New Projects

1. Create a new project in `backend/src`:

   ```powershell
   cd backend/src
   dotnet new classlib -n MyApp.NewFeature
   ```

2. Add it to the solution:

   ```powershell
   cd ../../backend
   dotnet sln add src/MyApp.NewFeature/MyApp.NewFeature.csproj
   ```

3. Reference it from other projects as needed.

## 🧩 Common Configuration

Shared MSBuild properties live in `Directory.Build.props`. Add any additional common settings there (e.g. code analysis rules, versioning, etc.). 

Package versions are managed centrally via [`Directory.Packages.props`](https://learn.microsoft.com/dotnet/core/project-sdk/central-package-management), which is enabled in `Directory.Build.props` (`<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>`). Projects reference packages without specifying a version, ensuring consistency across the solution.

## 🔄 CI/CD

(If applicable) Add notes about pipelines, environment variables, build scripts, etc.

## ✨ Tips

- Keep controllers thin; push logic into services.
- Use `IConfiguration` and options pattern for config.
- Register dependencies in `Program.cs` or via extension methods in `MyApp.Api`.
- Write unit tests against interfaces in the **Interfaces**/ **Services** projects and keep them decoupled from ASP.NET specifics.

### API versioning

- The API uses [Microsoft.AspNetCore.Mvc.Versioning](https://github.com/microsoft/aspnet-api-versioning) to centralise endpoint versioning.
- Controllers should use the `[ApiVersion]` attribute and versioned routes (e.g. `api/v{version:apiVersion}/[controller]`).
- The default version is 1.0 and unspecified requests assume that version.

### Database stack

- The entire data access layer will use **Dapper (lightweight ORM)**.
- **PostgreSQL** is the chosen database engine for all environments.
- Implement repository classes in the `MyApp.Repositories` project using Dapper and Npgsql. Keep SQL queries simple and hand-written; leverage Dapper's parameter binding and mapping.
- Connection strings and any DB-related configuration should live in `appsettings.*.json` and be accessed via `IConfiguration`.

---

This README should serve as a living document; update it as the backend evolves. Feel free to add sections on database migrations, authentication, logging, or other infrastructure details as needed.
