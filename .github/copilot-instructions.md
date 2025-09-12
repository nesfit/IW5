# IW5 Programming in .NET and C# - CookBook Application

**ALWAYS follow these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

## Working Effectively

### Bootstrap and Setup
- **REQUIRED**: Install .NET 9.0 SDK (project targets net9.0, not net8.0):
  ```bash
  wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
  chmod +x dotnet-install.sh
  ./dotnet-install.sh --channel 9.0
  export PATH="$HOME/.dotnet:$PATH"
  ```
- **REQUIRED**: Install Entity Framework Core tools globally:
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- Verify setup: `dotnet --version` (should show 9.0.x)

### Build and Test Commands
- Navigate to solution: `cd /home/runner/work/IW5/IW5/src/CookBook`
- **ALWAYS** ensure PATH includes .NET: `export PATH="$HOME/.dotnet:$PATH"`
- Restore packages: `dotnet restore` -- takes 30 seconds. NEVER CANCEL. Set timeout to 60+ minutes.
- Build solution: `dotnet build --no-restore` -- takes 15 seconds. NEVER CANCEL. Set timeout to 30+ minutes.
- Run all tests: `dotnet test --no-build` -- takes 3 seconds. NEVER CANCEL. Set timeout to 15+ minutes.

### Run Applications
**API (ASP.NET Core Web API):**
- Start: `cd CookBook.Api.App && dotnet run --no-build`
- URLs: https://localhost:44378 (HTTPS) and http://localhost:61245 (HTTP)
- Swagger UI: https://localhost:44378/swagger
- API endpoints: https://localhost:44378/api/recipe, https://localhost:44378/api/ingredient

**Web App (Blazor WebAssembly):**
- Start: `cd CookBook.Web.App && dotnet run --no-build`
- URLs: https://localhost:44355 (HTTPS) and http://localhost:56567 (HTTP)

**Run Both Simultaneously:**
- Use separate terminal sessions or background processes
- API must be running for Web app to function fully

## Validation Scenarios

**ALWAYS test these complete scenarios after making changes:**

1. **API Functionality Test:**
   ```bash
   # Create a new ingredient
   curl -k -X POST https://localhost:44378/api/ingredient \
     -H "Content-Type: application/json" \
     -d '{"id": "00000000-0000-0000-0000-000000000000", "name": "Test Ingredient", "description": "Test", "imageUrl": "https://example.com/test.jpg"}'
   
   # Verify it appears in the list
   curl -k https://localhost:44378/api/ingredient
   ```

2. **Full Application Test:**
   - Start API in background
   - Start Web app in background  
   - Access Web app in browser at https://localhost:44355
   - Verify ingredient and recipe lists load
   - Test creating/editing functionality

3. **Build and Test Validation:**
   - Always run `dotnet test` after code changes
   - Check that all 11 tests pass
   - Build warnings are normal but no errors should occur

## Database Configuration

**Memory Provider (Default):**
- Configured in `appsettings.json`: `"DALSelectionOptions": { "Type": "Memory" }`
- Works out of the box, no setup required
- Data persists only during application lifetime

**Entity Framework Provider:**
- Change `appsettings.json`: `"DALSelectionOptions": { "Type": "EntityFramework" }`
- Requires SQL Server connection string in `"ConnectionStrings": { "DefaultConnection": "..." }`
- Run database migrations: `cd CookBook.Api.App && dotnet ef database update`

## CI/CD and Azure

**Azure DevOps Pipelines:**
- CI pipelines: `CI-API.yml`, `CI-WEB.yml` 
- CD pipelines: `CD-API.yml`, `CD-WEB.yml`
- Build templates: `build-API.yml`, `build-WEB.yml`
- Targets .NET 6.0 and Windows runtime for Azure deployment
- **Note**: Local development uses .NET 9.0, Azure pipelines use .NET 6.0

**Azure Deployment:**
- Follow naming conventions from Lecture 1 presentations
- Grant access to `uciteliw5@vutbr.cz` account for all Azure resources

## Troubleshooting

**Common Issues:**
- "dotnet command not found" → Ensure `export PATH="$HOME/.dotnet:$PATH"`
- ".NET 8.0 not supported" → Install .NET 9.0 as shown above
- "dotnet-ef command not found" → Install EF tools: `dotnet tool install --global dotnet-ef`
- HTTPS certificate warnings → Normal in development, use `-k` flag with curl
- AutoMapper license warnings → Normal in development/testing scenarios

**Performance Expectations:**
- Solution has 13 projects across multiple layers
- Clean builds may take longer on first run due to NuGet package downloads
- Memory provider starts instantly, EF provider requires database setup

## Project Structure

```
/src/CookBook/
├── CookBook.Api.App/              # Web API application
├── CookBook.Api.BL/               # API business logic
├── CookBook.Api.DAL.Common/       # Data access interfaces
├── CookBook.Api.DAL.EF/           # Entity Framework implementation
├── CookBook.Api.DAL.Memory/       # In-memory implementation  
├── CookBook.Web.App/              # Blazor WebAssembly app
├── CookBook.Web.BL/               # Web business logic
├── CookBook.Web.DAL/              # Web data access
├── CookBook.Common/               # Shared utilities
├── CookBook.Common.BL/            # Shared business logic
├── CookBook.Common.Models/        # Shared models
├── ***.UnitTests/                 # Unit test projects
├── ***.IntegrationTests/          # Integration test projects
└── ***.EndToEndTests/             # End-to-end test projects
```

## Key Files Reference

**Configuration:**
- `CookBook.Api.App/appsettings.json` - API configuration
- `CookBook.Web.App/wwwroot/appsettings.json` - Web app configuration
- `CookBook.sln` - Solution file
- `Directory.Build.props` - Global MSBuild properties

**Launch Settings:**
- `CookBook.Api.App/Properties/launchSettings.json` - API launch URLs
- `CookBook.Web.App/Properties/launchSettings.json` - Web app launch URLs

**Visual Studio Integration:**
- `CookBook.sln.startup.json` - Multi-project startup configuration
- `CookBook.slnLaunch` - Launch profile for both API and Web

**Always validate your changes by running the complete build-test-run cycle before committing.**