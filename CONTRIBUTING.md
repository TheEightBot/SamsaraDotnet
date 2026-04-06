# Contributing to Samsara.Sdk

Thank you for your interest in contributing! This guide covers how to build the project, run tests, submit pull requests, and cut a new release.

## Table of Contents

- [Development Setup](#development-setup)
- [Building](#building)
- [Testing](#testing)
- [Code Style](#code-style)
- [Submitting a Pull Request](#submitting-a-pull-request)
- [Releasing a New Version](#releasing-a-new-version)

---

## Development Setup

**Prerequisites**

| Tool | Minimum Version |
|------|----------------|
| .NET SDK | 8.0 |
| Git | 2.x |

Clone the repository:

```shell
git clone https://github.com/YOUR-ORG/SamsaraDotnet.git
cd SamsaraDotnet
```

Restore dependencies:

```shell
dotnet restore
```

---

## Building

```shell
# Debug build (default)
dotnet build

# Release build
dotnet build --configuration Release
```

The `TreatWarningsAsErrors` property is enabled globally — zero warnings is the bar.

---

## Testing

Run all unit tests:

```shell
dotnet test tests/Samsara.Sdk.Tests --configuration Release
```

Run with code coverage:

```shell
dotnet test tests/Samsara.Sdk.Tests \
  --configuration Release \
  --collect:"XPlat Code Coverage" \
  --results-directory ./coverage
```

**Integration tests** (`Samsara.Sdk.IntegrationTests`) require a valid Samsara API token. Set the environment variable before running:

```shell
export SAMSARA_API_TOKEN="samsara_api_..."
dotnet test tests/Samsara.Sdk.IntegrationTests --configuration Release
```

---

## Code Style

- Follow existing patterns: immutable `record` models, `IAsyncEnumerable<T>` pagination, typed exceptions.
- Keep implementations `internal sealed` — only interfaces are public.
- Add XML doc comments to any new public interface members.
- Run `dotnet build` before opening a PR to catch warnings.

`.editorconfig` enforces formatting rules. Most IDEs (Visual Studio, Rider, VS Code + C# extension) will apply them automatically.

---

## Submitting a Pull Request

1. Fork the repo and create a feature branch from `main`.
2. Make your changes, add tests, ensure `dotnet test` passes.
3. Open a PR using the provided template.
4. At least one maintainer approval is required before merging.

For significant changes, open an issue first to discuss the approach.

---

## Releasing a New Version

Releases are fully automated via GitHub Actions. To publish a new version to NuGet.org:

1. Ensure all changes are merged to `main`.
2. Update `CHANGELOG.md` — move items from `[Unreleased]` to a new version section.
3. Push a version tag:

   ```shell
   git tag v1.0.0
   git push origin v1.0.0
   ```

The `release.yml` workflow will automatically:
- Build and test the solution
- Pack `Samsara.Sdk` (version is derived from the git tag via [MinVer](https://github.com/adamralph/minver))
- Push `.nupkg` and `.snupkg` to [NuGet.org](https://www.nuget.org)
- Create a GitHub Release with auto-generated release notes

> **Required secret:** `NUGET_API_KEY` must be set in the repository secrets with a valid NuGet.org API key scoped to `Samsara.Sdk`.

### Pre-release versions

Use a tag suffix for pre-release packages:

```shell
git tag v1.1.0-beta.1
git push origin v1.1.0-beta.1
```

This produces a `1.1.0-beta.1` NuGet package.
