# Scenario 1: Local CookBook accounts

[Choose another scenario](authentication-overview.md). Run/deploy **Web, API, and
IdentityProvider**. No Entra registrations or Entra secrets are needed.
Web uses the Duende client `cookbookclient`, not an Entra client ID.

This is the first teaching example: user storage, credential validation, claims,
and token issuance are visible in your own application. It is a valid project
choice, not just preparation for Entra. If you prefer delegated user management
and fewer deployed components, [scenario 2: direct Entra](authentication-setup-entra.md)
reaches the same authentication/authorization goals without this server.
[Scenario 3: combined login](authentication-setup-combined.md) extends this setup
with an external login option.

## 1. IdentityProvider: database and local login

The active store is persistent ASP.NET Identity. Provision SQL Server locally
and create a dedicated identity database. In Visual Studio, open
**CookBook.IdentityProvider.App > Manage User Secrets**, not the DAL project.
Merge:

```json
{
  "ConnectionStrings:DefaultConnection": "YOUR_LOCAL_IDENTITY_SQL_CONNECTION_STRING",
  "CookBookIdentityProviderOptions:Local:IsEnabled": true,
  "CookBookIdentityProviderOptions:ExternalProviders:0": {
    "IsEnabled": false
  }
}
```

Run in Development. Apply the identity migrations from the App directory:

```powershell
Set-Location src\CookBook\CookBook.IdentityProvider.App
dotnet ef database update --project ..\CookBook.IdentityProvider.DAL --startup-project .
```

After migration, check whether any duplicate subjects were renamed:

```sql
SELECT Id, UserName, Subject
FROM AspNetUsers
WHERE Subject LIKE 'legacy-duplicate-%';
```

If this query returns rows, invalidate existing IdentityProvider authentication
cookies by rotating the Data Protection key ring and retiring its previous keys,
or by renaming the authentication cookie. The IdentityProvider has no server-side
sessions. Require affected users to sign in again.

Install a `dotnet-ef` version matching the project's EF Core major version if
missing. The design-time factory must resolve your database connection; if the
tool host does not discover App user secrets, also set the same connection in
the DAL project's **Manage User Secrets** for migration tooling only. Do not
commit it to JSON. Return to the repository root after running the command.

### Create the local admin and user

After applying the identity migrations, create an admin and a regular user with
the opt-in **Development** seeder. Add
these settings to the App project's user secrets, using your own distinct
usernames and strong passwords:

```json
{
  "DevelopmentSeed:Enabled": true,
  "DevelopmentSeed:Admin:UserName": "local-admin",
  "DevelopmentSeed:Admin:Password": "REPLACE_WITH_A_LOCAL_SECRET",
  "DevelopmentSeed:User:UserName": "local-user",
  "DevelopmentSeed:User:Password": "REPLACE_WITH_A_DIFFERENT_LOCAL_SECRET"
}
```

Start or restart **CookBook.IdentityProvider.App** using its `SelfHost` launch
profile, which sets `ASPNETCORE_ENVIRONMENT` to `Development`. Alternatively,
from the App directory run:

```sh
dotnet run --launch-profile SelfHost
```

Look for `Development provisioning: 2 created, 0 existing accounts left unchanged.`
in the startup output. Start API and Web as described below, then use the Web
app's login to sign in as `local-admin` or `local-user` with the corresponding
password from user secrets. No HTTP request to create users is needed: the
`POST /api/user` and `POST /user` endpoints have been removed.

The `admin` and `user` roles are assigned through ASP.NET Identity and included
in access tokens. Existing usernames are skipped unchanged, including their
passwords and roles. Creation is transactional and fails the startup if it
fails. This seeder never runs outside Development. Use a separate local identity
database: a Development process pointed at a production database can still
write to it.

If either username already exists (for example, from the old API), it is not
upgraded to the requested role or given a new password. Choose fresh usernames
in the seed settings to create the role-bearing accounts. After creation, you
can set `DevelopmentSeed:Enabled` to `false` and remove the seed passwords from
user secrets; the accounts remain in the database.

Provisioning generates each account's stable token subject (`sub`) and hashes
passwords through ASP.NET Identity `UserManager` / `RoleManager`. Never manually
hash passwords or change existing subjects. The anonymous `/Account/Create`
registration page and user-search endpoints remain teaching infrastructure:
restrict/protect them before exposing a real production identity service.

### One-time production account provisioning

For production, there is no automatic account seeding. To create one account,
run the IdentityProvider as a one-time command from its application directory,
after applying migrations and providing its production configuration:

```sh
dotnet CookBook.IdentityProvider.App.dll provision-user
```

Supply `ConnectionStrings__DefaultConnection`, `ProvisionUser__UserName`,
`ProvisionUser__Password`, and `ProvisionUser__Role` (`admin` or `user`)
through the host's protected configuration/secret store, such as a Key Vault
reference. Do not put passwords in Git, command-line arguments, shell history,
or deployment logs. An optional `ProvisionUser__Email` is stored **unconfirmed**;
verify its ownership separately before marking it confirmed. This prevents
unverified email addresses from being used to link external identities.
Repeat the command with different settings for the second account, and remove
the temporary provisioning secrets afterward. The command refuses an existing
username rather than resetting its password or granting it a role. It exits
nonzero on failure and does not start the web server.

Use appropriately strong passwords and tighten the sample Identity password
policy before production. Restrict who can invoke provisioning or access its
secrets: the `admin` role grants administrative access.

## 2. Local API: user secrets

Open **CookBook.Api.App > Manage User Secrets**, and merge:

```json
{
  "IdentityOptions:IsEnabled": true,
  "TrustedIssuersOptions:Issuers:0": {
    "Authority": "https://localhost:5001",
    "ValidIssuers": [ "https://localhost:5001" ],
    "ValidAudiences": [ "cookbookclientaudience" ]
  }
}
```

Keep index `0` for CookBook and index `1` for the inherited Entra entry.
The API registers both; Entra is not selected by this Web scenario.

## 3. Local Web: development JSON

Create/merge `src\CookBook\CookBook.Web.App\wwwroot\appsettings.Development.json`:

```json
{
  "ApiOptions": { "BaseUrl": "https://localhost:44378/" },
  "IdentityOptions": { "IsEnabled": true },
  "WebAuthenticationOptions": {
    "SelectedProvider": "CookBookIdentityProvider",
    "Providers": [
      {
        "ProviderType": "CookBookIdentityProvider",
        "IsEnabled": true,
        "Authority": "https://localhost:5001",
        "ClientId": "cookbookclient",
        "ResponseType": "code",
        "RedirectUri": "https://localhost:44355/authentication/login-callback",
        "PostLogoutRedirectUri": "https://localhost:44355",
        "DefaultScopes": [ "openid", "profile", "cookbookapi" ]
      },
      { "ProviderType": "Entra", "IsEnabled": false }
    ]
  }
}
```

Start IdentityProvider (`SelfHost`), API (`CookBook.Api`), and Web. Trust the
local development HTTPS certificate if necessary (`dotnet dev-certs https --trust`).

## 4. Azure IdentityProvider: prerequisites and portal settings

Complete the [common Azure settings](authentication-overview.md#common-azure-settings).
Provision Azure SQL for identity users; allow the IdentityProvider App Service
to reach it using your chosen firewall/private-network configuration.
Apply identity migrations to that database in a controlled deployment step
before serving traffic. Then check whether any duplicate subjects were renamed:

```sql
SELECT Id, UserName, Subject
FROM AspNetUsers
WHERE Subject LIKE 'legacy-duplicate-%';
```

If this query returns rows, invalidate existing IdentityProvider authentication
cookies by rotating the Data Protection key ring and retiring its previous keys,
or by renaming the authentication cookie. The IdentityProvider has no server-side
sessions; require affected users to sign in again. Keep identity and recipe
databases separate.

**Required code prerequisite:** the current
`IdentityProvider.DAL\Factories\IdentityProviderDbContextFactory.cs` creates a
separate configuration builder which does not read environment variables.
Before deploying, add `.AddEnvironmentVariables()` **after** its
`.AddUserSecrets(startupAssembly, optional: true)` call and before `.Build()`.
Otherwise the Azure connection-string setting below is ignored by this factory,
even though ordinary App Service configuration works elsewhere in the app.
Do not work around this by checking a SQL password into `appsettings.json`.

In **CookBook.IdentityProvider.App\Config.cs**, keep the existing client and add
the production entries (do not replace the localhost entries):

```csharp
RedirectUris =
[
    "https://oauth.pstmn.io/v1/callback",
    "https://localhost:44355/authentication/login-callback",
    "https://app-iw5-web.azurewebsites.net/authentication/login-callback"
],
PostLogoutRedirectUris =
[
    "https://localhost:44355",
    "https://app-iw5-web.azurewebsites.net/"
],
AllowedCorsOrigins =
[
    "https://localhost:44355",
    "https://app-iw5-web.azurewebsites.net"
],
```

Keep `cookbookapi` in the client's allowed scopes and in
`ApiResources["cookbookclientaudience"].Scopes`. The API audience must be
`cookbookclientaudience`; the generic `<issuer>/resources` audience is not a
replacement. Duende client allowlists currently live in code: App Service
variables do not update `Config.cs`; rebuild and redeploy after changing it.

Open **App Services > app-iw5-identity > Settings > Environment variables >
App settings**, and add:

| Name | Value |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ConnectionStrings__DefaultConnection` | Azure SQL identity connection string, preferably a Key Vault reference |
| `ContentSecurityPolicy__ClientOrigin` | `https://app-iw5-web.azurewebsites.net` |
| `CookBookIdentityProviderOptions__Local__IsEnabled` | `true` |
| `CookBookIdentityProviderOptions__ExternalProviders__0__IsEnabled` | `false` |

Save/apply and restart. For Key Vault references, enable the App Service managed
identity and grant it permission to read the referenced secrets.

Before non-demo production use, configure persistent Duende signing-key storage
and ASP.NET Core Data Protection keys shared across instances; verify they
survive deployment/restarts. Verify the applicable Duende production license.
Portal authentication settings alone do not complete production hardening.

## 5. Azure API: App Service settings

Under **app-iw5-api > Settings > Environment variables > App settings**, add:

| Name | Value |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `IdentityOptions__IsEnabled` | `true` |
| `TrustedIssuersOptions__Issuers__0__Authority` | `https://app-iw5-identity.azurewebsites.net` |
| `TrustedIssuersOptions__Issuers__0__ValidIssuers__0` | `https://app-iw5-identity.azurewebsites.net` |
| `TrustedIssuersOptions__Issuers__0__ValidAudiences__0` | `cookbookclientaudience` |

Save/apply and restart. Memory-backed API data disappears on restart; use the
API EF provider, a separate SQL connection, and its migrations if persistence
is required.

## 6. Azure Web: deployment artifact

Merge section 3's Web configuration into the artifact's
`wwwroot\appsettings.json`, replacing:

| Setting | Production value |
|---|---|
| `ApiOptions:BaseUrl` | `https://app-iw5-api.azurewebsites.net/` |
| CookBook provider `Authority` | `https://app-iw5-identity.azurewebsites.net` |
| CookBook provider `RedirectUri` | `https://app-iw5-web.azurewebsites.net/authentication/login-callback` |
| CookBook provider `PostLogoutRedirectUri` | `https://app-iw5-web.azurewebsites.net/` |

Keep client `cookbookclient`, scopes `openid`, `profile`, `cookbookapi`, and
selection `CookBookIdentityProvider`. Exclude the development JSON before
deploying to **app-iw5-web**. Do not put passwords or server secrets in Web.

## 7. Verify

In a fresh private window, open Web, choose **Log in**, sign in with a local
account, create an ingredient, and log out. On Azure, verify IdentityProvider
discovery at
`https://app-iw5-identity.azurewebsites.net/.well-known/openid-configuration`;
its `issuer` must be `https://app-iw5-identity.azurewebsites.net` (HTTPS).
If it advertises HTTP, fix App Service/reverse-proxy HTTPS forwarding before
testing tokens; do not trust an HTTP issuer as a workaround.

An API access token must contain that issuer and audience
`cookbookclientaudience`. A Web ID token has a different audience and must not
be used for API requests. Confirm local roles and ownership rules separately
when an operation returns `403`.
