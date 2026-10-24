# Choose an authentication scenario

Follow one setup guide. Each covers local development and Azure App Service.

| Scenario | Guide | Applications to deploy |
|---|---|---|
| Entra manages users; no CookBook IdentityProvider | [Direct Entra](authentication-setup-entra.md) | Web + API |
| CookBook manages local usernames/passwords | [Local accounts](authentication-setup-local.md) | Web + API + IdentityProvider |
| Local accounts plus an Entra login button | [Combined login](authentication-setup-combined.md) | Web + API + IdentityProvider |

For the identity-disabled demonstration, set `IdentityOptions:IsEnabled=false`
in both API and Web. No IdentityProvider is needed. API reads remain public;
the switch controls authentication requirements on modifying endpoints.

## Addresses

| Application | Local | Azure |
|---|---|---|
| Web | `https://localhost:44355` | `https://app-iw5-web.azurewebsites.net` |
| API | `https://localhost:44378` | `https://app-iw5-api.azurewebsites.net` |
| IdentityProvider | `https://localhost:5001` | `https://app-iw5-identity.azurewebsites.net` |

The supplied HTTP Web/API addresses use these same hosts, but **use HTTPS in all
authentication settings**. Enable **HTTPS Only** on the App Services. Entra does
not accept public HTTP callbacks, and HTTPS Web cannot call an HTTP API.

## Where configuration belongs

| Application | Local | Azure production |
|---|---|---|
| API | App project's **Manage User Secrets** | App Service **Settings > Environment variables > App settings** |
| IdentityProvider | App project's **Manage User Secrets** | Same App Service page; secrets preferably use Key Vault references |
| Web (Blazor WebAssembly) | `wwwroot\appsettings.Development.json` | Deployment artifact's `wwwroot\appsettings.json` |

Server projects already have user-secrets IDs. Merge examples into their
existing secrets files; preserve database settings. Run locally in
`Development`. User secrets are not encrypted and are not production storage.
Nested objects under colon-delimited keys are supported; do not duplicate the
same effective key using both nested and flattened forms.

Azure setting names use `__` instead of `:`. Save/apply settings and restart
the corresponding server. Environment variables override normal server JSON.
Mark environment-specific credentials/settings as **Deployment slot settings**
when using slots.

**Web configuration is public and downloaded by the browser.** App Service
environment variables do not rewrite static WebAssembly JSON. Substitute values
in the publish artifact before deployment; never put secrets there. Exclude
`appsettings.Development.json` from that artifact: Web currently loads it
explicitly even outside Development, so it would override production settings.
Do not rely on `appsettings.Production.json` overriding the files explicitly
loaded by the current `Program.cs`.

JSON arrays merge by index, not by provider name. Existing layouts are:

| Collection | Index 0 | Index 1 |
|---|---|---|
| API `TrustedIssuersOptions:Issuers` | CookBookIdentityProvider | Entra |
| Web `WebAuthenticationOptions:Providers` | CookBookIdentityProvider | Entra |
| IdentityProvider `CookBookIdentityProviderOptions:ExternalProviders` | Entra | unused |

The guides retain these positions. Web provider selection does not restrict
which issuers the API trusts; all configured API issuers remain registered.
For a strictly single-issuer deployment, replace the API issuer collection in
the deployment artifact with only the required entry and adjust override
indices. Environment overrides cannot remove an inherited array entry.

## Common Azure settings

On each deployed App Service:

1. Set **HTTPS Only** to **On** (under **Configuration > General settings** or
   **Configuration > Stack settings**, depending on portal layout).
2. Keep App Service **Authentication** / Easy Auth disabled for these guides:
   the applications already handle authentication. A second login layer can
   intercept API calls and callbacks.
3. Set the server runtime to a version supporting the project's target framework
   (currently `net10.0`), or publish self-contained with the appropriate runtime.
4. Set server `ASPNETCORE_ENVIRONMENT=Production`; do not deploy local secrets.
5. Use **Monitoring > App Service logs**, enable application logging appropriate
   to the host, then open **Log stream** for server failures.

The sample allows cross-origin API calls in application middleware. Avoid adding
an incompatible second CORS policy in App Service. A hardened deployment should
allow the exact Web origin `https://app-iw5-web.azurewebsites.net`, without a path.

## Troubleshooting after changing scenarios

Use a fresh private window and establish a new login, not an old callback URL.
Old cookies and access tokens retain old names, roles, and audiences.

| Symptom | Check |
|---|---|
| Generic sign-in error | Browser Console + preserved Network log; matching IdentityProvider logs |
| API `401` | Bearer header present; token `iss` and `aud` match API configuration; token not expired |
| API `403` | Authentication passed; inspect roles and ownership rules for that operation |
| Entra callback mismatch | Exact URI, HTTPS, and correct SPA versus Web platform |
| Entra secret error | Secret **Value**, not Secret ID; owning registration and expiry |
| Old generated username | Complete a new external login; existing users need no deletion |

Do not disable signature, issuer, or audience validation to resolve a `401`.
Do not share tokens, cookies, client secrets, or an unredacted HAR.

The sample's named admin endpoint policies may be commented out; assigning
`admin` does not itself enable those policies. Business-layer ownership checks
can also return `403`.

## Regression checks

From the repository root:

```powershell
dotnet test src\CookBook\CookBook.IdentityProvider.App.UnitTests\CookBook.IdentityProvider.App.UnitTests.csproj
dotnet test src\CookBook\CookBook.Web.BL.UnitTests\CookBook.Web.BL.UnitTests.csproj
node --test src\CookBook\CookBook.Web.BL.UnitTests\authentication-loader.test.cjs
```

These do not replace a live login and authenticated API write in each deployed
environment.

References: [App Service configuration](https://learn.microsoft.com/en-us/azure/app-service/configure-common),
[Entra redirect URI rules](https://learn.microsoft.com/en-us/entra/identity-platform/reply-url).
