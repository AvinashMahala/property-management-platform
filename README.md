# Property Management Platform

This repository contains the full-stack codebase for the Property Management Platform, including backend services, database scripts, frontend code, and documentation.

## 🚀 Overview

- **backend/** – .NET solution with API, services, models, interfaces, and repositories. Detailed backend-specific guidance lives in `backend/backend-readme.md`.
- **frontend/** – client application (React, Angular, or similar), manage UI/UX for property management.
- **db/** – database migration and seed scripts, Postgres-specific SQL.
- **docs/** – architectural diagrams, specifications, and design documents.
- **postman-scripts/** – collections and helper scripts for API testing.
- **.github/workflows/** – CI/CD yaml files for automated builds/tests.
- **.gitignore** – excluded files and folders from source control.

The backend uses Dapper with PostgreSQL for data access; the API exposes HTTP endpoints and publishes Swagger documentation in development. The frontend consumes the API and handles authentication, forms, and user interactions.

## 📁 Repository Structure

```
├── backend/
│   ├── MyApp.sln
│   ├── Directory.Build.props
│   ├── backend-readme.md       # Backend-specific instructions
│   └── src/
│       ├── MyApp.Api
│       ├── MyApp.Interfaces
│       ├── MyApp.Models
│       ├── MyApp.Services
│       └── MyApp.Repositories
├── frontend/                  # UI project (see frontend README)
├── db/                        # Postgres scripts, migrations
├── docs/                      # Architecture/design docs
└── postman-scripts/           # API testing collections
```

## 🔧 Getting Started

1. **Prerequisites**
   - [.NET 10 SDK](https://dotnet.microsoft.com/download) or later
   - Node.js and npm/yarn for frontend
   - PostgreSQL server (local or remote)
   - Optional: Visual Studio / VS Code for development

2. **Clone the repo**
   ```bash
   git clone <repo-url>
   cd PropertyManagementPlatform
   ```

3. **Setup database**
   - Create a Postgres database (e.g. `property_management`).
   - Run SQL migration scripts from `db/`.
   - Update connection strings in `backend/src/MyApp.Api/appsettings.Development.json`.

4. **Backend**
   ```powershell
   cd backend
dotnet restore
dotnet build
dotnet run --project src/MyApp.Api
   ```
   - API will start on configured ports; visit `/swagger` for the (initially blank) UI.

5. **Frontend**
   ```bash
   cd frontend
yarn install
yarn start
   ```
   - The client app will run on `http://localhost:3000` (or similar) and communicate with the backend.

6. **API Testing**
   - Import collections from `postman-scripts/` and point to local endpoints.

## 📘 Documentation

- Backend-specific instructions: `backend/backend-readme.md`.
- API-specific details: `backend/src/MyApp.Api/MyApp.Api-readme.md`.
- Database documentation and ER diagrams in `docs/`.

## 🧩 Architecture Notes

- Monorepo with loose coupling between layers
- Backend implements clean architecture principles; interfaces separate contracts from implementations
- Services and repositories are injected into API project via dependency injection
- Shared model types live in `MyApp.Models`

## 🧪 Testing

- Unit tests should be added under respective projects (not yet present).
- Integration tests may spin up in-memory instances or connect to a test database.

## 🤝 Contributing

This project includes a `CONTRIBUTING.md` with detailed guidelines.

1. Create an issue or discuss feature/bug.
2. Fork repo and create a branch (`feature/xyz` or `bugfix/abc`).
3. Follow coding standards and update documentation.
4. Submit a pull request; ensure builds pass.

## 🔐 Security

- Use environment variables or user secrets for sensitive settings (connection strings, API keys).
- Do not commit credentials.

## 📜 License

Specify license if applicable.

---

This README is intended to be the entry point for new developers. See sub‑folders for additional context and instructions.