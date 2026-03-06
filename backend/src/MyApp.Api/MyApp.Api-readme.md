# MyApp.Api Project

This document provides detailed information about the `MyApp.Api` project—the ASP.NET Core Web API that serves as the entry point for the backend of the Property Management Platform.

## 🎯 Purpose

`MyApp.Api` exposes HTTP endpoints that clients (web, mobile, other services) can call. It is responsible for:

- Configuring middleware (routing, authentication, CORS, etc.)
- Registering services with the dependency injection (DI) container
- Hosting the generated OpenAPI/Swagger documentation (currently blank)
- Acting as the composition root for the backend

The project itself contains minimal business logic; controllers should delegate to services defined in `MyApp.Services` (via interfaces in `MyApp.Interfaces`).

## 🧱 Project Structure

```
MyApp.Api/
├── Program.cs                  # Web application bootstrap
├── Properties/
│   └── launchSettings.json     # Development launch profiles
├── appsettings.json            # Configuration file
└── appsettings.Development.json
```

At the moment there are no controllers or additional folders. Add them as features are built.

## 🚀 Running Locally

### Prerequisites

Ensure the entire solution has been restored/built:

```powershell
cd backend
dotnet restore
dotnet build
dotnet run --project src/MyApp.Api
```

### Available URLs

Launch profiles configure the following addresses:

- `http://localhost:5291` (http)
- `https://localhost:7092` (https)
- IIS Express may use ports `64105` (http) and `44385` (https)

When running, the app will redirect `/` to `/swagger` where the Swagger UI is available in development.

### Swagger behaviour

- Swagger (via Swashbuckle) is enabled only when `ASPNETCORE_ENVIRONMENT=Development`.
- No endpoints are registered initially; consequently the UI shows "No operations defined in the spec." 
- As controllers are added, they will automatically appear in the Swagger documentation without further configuration.

## 🛠 Key code_snippets

### Program.cs (simplified)

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// add swagger explorer/generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger", permanent: false));

app.Run();
```

Feel free to extend this file with configuration for:

- Authentication/authorization (e.g. `AddAuthentication`, `AddAuthorization`)
- CORS (`AddCors`) for cross-origin requests
- Logging providers or custom middleware

## 📦 Dependencies

Only `Swashbuckle.AspNetCore` is referenced explicitly. All other packages come transitively via the `.NET SDK`.

```xml
<ItemGroup>
  <PackageReference Include="Swashbuckle.AspNetCore" Version="7.0.6" />
</ItemGroup>
```

## 🧩 Adding Functionality

1. **Create a controller:**
   ```powershell
   cd src/MyApp.Api
   dotnet add package Microsoft.AspNetCore.Mvc.Core
   ```
   Then add a `Controllers` folder and classes inheriting from `ControllerBase`.

2. **Register services:**
   Services from `MyApp.Services` should be registered here. Example:
   ```csharp
   builder.Services.AddScoped<IPropertyService, PropertyService>();
   ```

3. **Configuration:**
   Add JSON settings to `appsettings.json` and bind them using the options pattern:
   ```csharp
   builder.Services.Configure<MyOptions>(builder.Configuration.GetSection("MyOptions"));
   ```

4. **Middleware:**
   Add additional middleware calls to the `app` pipeline after `app.UseHttpsRedirection()`.

## ✅ Best Practices

- Keep controllers thin; push business logic into services.
- Use the options pattern for configuration.
- Prefer constructor injection for dependencies.
- Avoid referencing infrastructure concerns (EF Core, HTTP clients) directly in controllers; inject abstractions instead.
- Add unit and integration tests to the `MyApp.Api` test project (if created) to validate endpoint behavior.

## 🧪 Testing

(No test project exists yet.) Create an xUnit/NUnit test project in `backend/src` when you are ready to start verifying API endpoints.

## 🛡 Security

- Secure sensitive configuration with user secrets or environment variables (never store passwords in source control).
- When authentication is added, make sure to configure HTTPS and properly validate tokens/credentials.

## 📘 Additional Notes

- Because the project is so small right now, most guidance lives in the root `backend-readme.md`. This file can grow with API-specific details.
- Remove the redirect in `Program.cs` once you have a landing page or root endpoint.

---

This readme should evolve as the API matures; update it with new conventions, middleware, or operational instructions as needed.
