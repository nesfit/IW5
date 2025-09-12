# IW5 Programming in .NET and C# - CookBook Application

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the information provided here.

## Working Effectively

### Prerequisites and Environment Setup
- Install .NET 8.0 SDK: `wget https://dot.net/v1/dotnet-install.sh && chmod +x dotnet-install.sh && ./dotnet-install.sh --version 8.0.119`
- Verify installation: `dotnet --version` (should show 8.0.119 or compatible)
- Navigate to project directory: `cd /home/runner/work/IW5/IW5/src/CookBook`

### Build and Restore Process
- **NEVER CANCEL builds or package restores** - they may take 2-5 minutes to complete
- Restore NuGet packages: `dotnet restore` -- takes approximately 35 seconds, set timeout to 90+ seconds
- Build entire solution: `dotnet build` -- takes approximately 25 seconds, set timeout to 90+ seconds
- Build API project only: `dotnet build CookBook.Api.App/CookBook.Api.App.csproj` -- takes approximately 2 seconds
- Build Web project only: `dotnet build CookBook.Web.App/CookBook.Web.App.csproj` -- takes approximately 2 seconds

### Testing
- **NEVER CANCEL test runs** - they typically complete in under 10 seconds but set timeout to 120+ seconds
- Run all tests: `dotnet test --no-build --verbosity normal` -- takes approximately 7 seconds
- Run specific test project: `dotnet test CookBook.Api.BL.UnitTests --no-build`
- Test projects include:
  - Unit Tests: `CookBook.Api.BL.UnitTests`
  - Integration Tests: `CookBook.API.DAL.IntegrationTests`
  - End-to-End Tests: `CookBook.Api.App.EndToEndTests`

### Running Applications
- **API Application**: 
  - `cd CookBook.Api.App && dotnet run`
  - Runs on https://localhost:44378 and http://localhost:61245
  - Uses in-memory database by default (DALSelectionOptions:Type = "Memory")
  - Test API: `curl -k -s https://localhost:44378/api/recipe`
  - Swagger UI available at: `https://localhost:44378/swagger/index.html`
- **Web Application**:
  - `cd CookBook.Web.App && dotnet run`
  - Runs on https://localhost:44355 and http://localhost:56567
  - Blazor WebAssembly app that connects to API
  - Test web app: `curl -s http://localhost:56567`
- **IMPORTANT**: Start API application first, then Web application for full functionality

### Code Quality and Formatting
- Check formatting: `dotnet format --verify-no-changes`
- Fix formatting: `dotnet format`
- EditorConfig settings are enforced (4-space indents, final newlines required)
- **ALWAYS run `dotnet format` before committing changes** to ensure CI compliance

## Validation Scenarios

### Complete End-to-End Validation
After making changes, **ALWAYS** complete these validation steps:
1. Build: `dotnet build` (must succeed with no errors)
2. Test: `dotnet test --no-build` (all tests must pass)
3. Format: `dotnet format --verify-no-changes` (should exit with code 0)
4. Start API: `cd CookBook.Api.App && dotnet run` (verify it starts without errors)
5. Test API endpoint: `curl -k -s https://localhost:44378/api/recipe` (should return JSON recipe data)
6. Start Web app: `cd CookBook.Web.App && dotnet run` (verify it starts without errors)
7. Test Web app: `curl -s http://localhost:56567` (should return HTML with "CookBook.Web" title)

### Manual Testing Scenarios
- **Recipe Management**: API provides CRUD operations for recipes at `/api/recipe`
- **Ingredient Management**: API provides CRUD operations for ingredients at `/api/ingredient`
- **Web Interface**: Blazor WebAssembly app consumes the API and provides user interface
- **Localization**: Application supports English ("en") and Czech ("cs") cultures

## Architecture and Codebase Navigation

### Project Structure
The solution follows Clean Architecture with these main components:

**API Layer**:
- `CookBook.Api.App` - Web API application entry point
- `CookBook.Api.BL` - Business Logic layer
- `CookBook.Api.DAL.EF` - Entity Framework data access
- `CookBook.Api.DAL.Memory` - In-memory data access (default)
- `CookBook.Api.DAL.Common` - Shared data access contracts

**Web Layer**:
- `CookBook.Web.App` - Blazor WebAssembly application
- `CookBook.Web.BL` - Web business logic
- `CookBook.Web.DAL` - Web data access layer

**Common/Shared**:
- `CookBook.Common` - Shared utilities and extensions
- `CookBook.Common.BL` - Common business logic
- `CookBook.Common.Models` - Shared data models

**Test Projects**:
- `CookBook.Api.BL.UnitTests` - Unit tests for API business logic
- `CookBook.API.DAL.IntegrationTests` - Integration tests for data access
- `CookBook.Api.App.EndToEndTests` - End-to-end API tests

### Key Files and Locations
- Solution file: `/src/CookBook/CookBook.sln`
- Global build props: `/src/CookBook/Directory.Build.props`
- EditorConfig: `/src/CookBook/.editorconfig`
- API configuration: `/src/CookBook/CookBook.Api.App/appsettings.json`
- Web configuration: `/src/CookBook/CookBook.Web.App/wwwroot/appsettings.json`
- CI/CD pipelines: `/CI-CD pipelines/` directory

### Technologies Used
- **.NET 8.0** - Target framework for all projects
- **ASP.NET Core** - Web API framework with Minimal APIs
- **Blazor WebAssembly** - Frontend framework
- **Entity Framework Core 8.0** - ORM (optional, defaults to in-memory)
- **AutoMapper** - Object mapping
- **xUnit** - Testing framework
- **NSwag** - OpenAPI/Swagger documentation
- **FluentValidation** - Input validation

### Development Patterns
- **Dependency Injection** - Used throughout for loose coupling
- **Repository Pattern** - Data access abstraction
- **Facade Pattern** - Business logic encapsulation
- **Clean Architecture** - Separation of concerns across layers
- **SOLID Principles** - Applied throughout codebase

## Common Development Tasks

### Adding New Features
1. Start with API layer: Add endpoints in `CookBook.Api.App/Program.cs`
2. Implement business logic in appropriate facade in `CookBook.Api.BL/Facades/`
3. Add data access in repository classes in `CookBook.Api.DAL.*/Repositories/`
4. Add Web UI components in `CookBook.Web.App/Pages/` or `CookBook.Web.App/Shared/`
5. **ALWAYS write tests** for new business logic in appropriate test projects
6. **ALWAYS run full validation** after implementing features

### Debugging Issues
- Check build warnings - they often indicate potential runtime issues
- API issues: Check `appsettings.json` DALSelectionOptions configuration
- Web issues: Verify `wwwroot/appsettings.json` ApiBaseUrl matches running API
- Database issues: Default is in-memory; no persistence between restarts
- CORS issues: API allows all origins in development mode

### CI/CD Integration
- Azure DevOps pipelines configured in `/CI-CD pipelines/`
- Separate pipelines for API (`CI-API.yml`) and Web (`CI-WEB.yml`)
- Both target .NET 6.0 in CI but projects use .NET 8.0 locally
- Builds target Windows (`win-x64`) runtime

## Important Notes

### Configuration Details
- **API runs in-memory mode by default** - no database setup required
- **HTTPS certificates** - Development certificates may show warnings but applications function normally
- **Port configuration** - API uses 44378 (HTTPS) and 61245 (HTTP), Web app uses dynamic ports
- **Localization** - Supports English and Czech, defaults to English

### Known Issues and Workarounds
- Code analysis warnings (CA1002, CA1062) exist but do not prevent build success
- HTTPS certificate warnings appear but can be ignored in development
- Formatting tool cannot fix code analysis warnings - only whitespace and style issues

### Educational Context
This is a student project for learning:
- Clean Architecture principles
- .NET 8.0 and C# development
- ASP.NET Core Web API development
- Blazor WebAssembly frontend development
- Testing strategies (Unit, Integration, End-to-End)
- CI/CD with Azure DevOps

**CRITICAL REMINDERS**:
- **NEVER CANCEL builds, tests, or package operations** - they complete quickly but need appropriate timeouts
- **ALWAYS validate completely** - build, test, format, and run both applications
- **ALWAYS run `dotnet format`** before committing to ensure CI pipeline success
- **Test real functionality** - don't just start/stop applications, verify they work correctly