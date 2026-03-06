# Contributing to Property Management Platform

Thank you for your interest in contributing! This document outlines the process and conventions to ensure consistency and quality across the codebase.

## 🧩 Getting Started

1. **Fork the repository** and clone your fork:
   ```bash
   git clone https://github.com/<your‑username>/PropertyManagementPlatform.git
   cd PropertyManagementPlatform
   ```
2. **Create a new branch** for your work:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Install prerequisites** (see the top-level README).

## 🛠 Coding Guidelines

- Follow existing naming conventions and folder structure.
- Keep each project focused on its responsibility (API, services, models, repositories).
- Write small, self-contained commits with clear messages.
- Update README(s) when behaviour or configuration changes.
- Keep code clean – delete commented-out sections and unused usings.

## ✅ Tests

- Add unit tests alongside the code they validate (e.g. `MyApp.Services.Tests`).
- Aim for high coverage of business logic. Infrastructure-heavy code may use integration tests.
- Run `dotnet test` from the repo root to execute all tests.

## 🔗 Pull Requests

1. Push your branch to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```
2. Open a pull request against the `main` (or `develop`) branch of the upstream repo.
3. Describe the changes, why they were made, and any testing instructions.
4. A reviewer will provide feedback and may request changes.

## 🧼 Code Review

- Ensure PRs build successfully and tests pass automatically.
- Keep PRs focused and avoid mixing unrelated changes.
- Respond to review comments promptly; commit fixes to the same branch.

## 🎯 Style and Formatting

- Use the .editorconfig rules (provided in repository) to maintain consistent formatting.
- Run `dotnet format` if you modify C# files.

## 📋 Issue Tracking

- Bug fixes should reference the corresponding issue number in the commit message or PR description.
- Feature requests should be discussed via issues before implementation.

## 📦 Licensing and Attribution

- Contributions will be licensed under the same terms as the project (see LICENSE file).
- When including third-party code or libraries, include appropriate attribution.

---

Thanks again for contributing! If you have questions, open an issue or reach out to the maintainers.