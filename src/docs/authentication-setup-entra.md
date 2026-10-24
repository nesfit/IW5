# Scenario 1: Direct Entra login

[Choose another scenario](authentication-overview.md). Run/deploy **Web and API only**.
Entra manages users; CookBook IdentityProvider and its database are not needed.

## 1. Create two Entra registrations

Open [Entra admin center](https://entra.microsoft.com/) and select your tenant.
Under **Entra ID > App registrations > New registration**, choose **Accounts in
this organizational directory only** for both registrations.

| Registration | Record from Overview | Platform |
|---|---|---|
| `IW5 CookBook API` | `API_CLIENT_ID` | No redirect URI |
| `IW5 CookBook Web` | `WEB_CLIENT_ID` | Single-page application (SPA) |

Record the directory ID as `TENANT_ID`. Replace these literal placeholders
everywhere below. They are not substituted automatically.

In **IW5 CookBook API**:

1. **Expose an API > Application ID URI**: set `api://API_CLIENT_ID`.
2. **Add a scope**: name `access_as_user`, state **Enabled**, consent **Admins and
   users** if tenant policy permits. Fill the consent display name/description
   with `Access CookBook as the signed-in user`.
3. **Manifest**: set `api.requestedAccessTokenVersion` to `2`, preserving the
   existing `api` properties and scopes. A v2 login URL alone does not force
   v2 API tokens.
4. **App roles > Create app role**: display name `CookBook Administrator`,
   allowed members **Users/Groups**, value `admin`, enabled.
5. Create a second enabled app role: display name `CookBook User`, allowed
   members **Users/Groups**, exact value `user`.
6. **Entra ID > Users > All users > New user > Create new user**: create two
   demonstration accounts, or use two existing tenant accounts. Complete any
   first-login password change before the demonstration.
7. **Entra ID > Enterprise applications > IW5 CookBook API > Users and groups >
   Add user/group**: assign the accounts separately:

   | Account | Selected app role | Expected API access-token `roles` |
   |---|---|---|
   | Administrator demo | `CookBook Administrator` | Contains `admin` |
   | Non-admin demo | `CookBook User` | Contains `user`, not `admin` |

Assign individuals for a simple demonstration. Ensure the non-admin account
has no direct or group-based `admin` assignment on this enterprise application.
These are application roles, not Entra directory administrator roles.

In **IW5 CookBook Web**:

1. **Authentication > Add a platform > Single-page application**. Register all
   four URIs, preserving localhost for development:

   | Purpose | URI |
   |---|---|
   | Local sign-in callback | `https://localhost:44355/authentication/login-callback` |
   | Local logout destination | `https://localhost:44355/` |
   | Azure sign-in callback | `https://app-iw5-web.azurewebsites.net/authentication/login-callback` |
   | Azure logout destination | `https://app-iw5-web.azurewebsites.net/` |

2. Leave implicit-grant checkboxes and public-client password/device flows
   disabled. Do not create a SPA secret.
3. **API permissions > Add a permission > My APIs > IW5 CookBook API >
   Delegated permissions**: select `access_as_user`. Grant tenant admin consent
   if policy requires it and you are authorized.

If **Enterprise applications > IW5 CookBook Web > Properties > Assignment
required?** is **Yes**, also assign both accounts to that Web enterprise
application so they can sign in (use its default access role if no roles are
defined there). Do not disable an organizational assignment policy as a
workaround. Authorization roles for this flow still belong to **CookBook API**,
not Web. Both users need the delegated permission/consent; assigning `user`
does not replace that permission.

The logout destination above is an additional SPA redirect URI, **not** the
Front-channel logout URL; leave that field empty for this setup.
Neither registration needs a client secret. Graph `User.Read` is not required.
For a teaching demonstration the same registrations cover local and Azure;
separate registrations are preferable for isolated production environments.

## 2. Local API: user secrets

In Visual Studio, **CookBook.Api.App > Manage User Secrets**. Merge:

```json
{
  "IdentityOptions:IsEnabled": true,
  "TrustedIssuersOptions:Issuers:1": {
    "Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
    "ValidIssuers": [ "https://login.microsoftonline.com/TENANT_ID/v2.0" ],
    "ValidAudiences": [ "API_CLIENT_ID" ]
  }
}
```

Index `1` inherits the Entra scheme and claim mappings from shared settings.
The v2 API audience is `API_CLIENT_ID`, **not** the Web client ID or scope URL.
Restart API in Development.

## 3. Local Web: development JSON

Create/merge `src\CookBook\CookBook.Web.App\wwwroot\appsettings.Development.json`:

```json
{
  "ApiOptions": { "BaseUrl": "https://localhost:44378/" },
  "IdentityOptions": { "IsEnabled": true },
  "WebAuthenticationOptions": {
    "SelectedProvider": "Entra",
    "Providers": [
      { "ProviderType": "CookBookIdentityProvider", "IsEnabled": false },
      {
        "ProviderType": "Entra",
        "IsEnabled": true,
        "Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
        "ClientId": "WEB_CLIENT_ID",
        "ResponseType": "code",
        "RedirectUri": "https://localhost:44355/authentication/login-callback",
        "PostLogoutRedirectUri": "https://localhost:44355/",
        "DefaultScopes": [ "openid", "profile", "api://API_CLIENT_ID/access_as_user" ],
        "UpstreamLogoutIsEnabled": true
      }
    ]
  }
}
```

Keep both array positions. Web selects MSAL automatically for Entra; MSAL
handles its protocol scopes and acquires the API token separately.

## 4. Azure API: App Service settings

Complete the [common Azure settings](authentication-overview.md#common-azure-settings).
Open **App Services > app-iw5-api > Settings > Environment variables >
App settings > Add**, and enter:

| Name | Value |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `IdentityOptions__IsEnabled` | `true` |
| `TrustedIssuersOptions__Issuers__1__Authority` | `https://login.microsoftonline.com/TENANT_ID/v2.0` |
| `TrustedIssuersOptions__Issuers__1__ValidIssuers__0` | `https://login.microsoftonline.com/TENANT_ID/v2.0` |
| `TrustedIssuersOptions__Issuers__1__ValidAudiences__0` | `API_CLIENT_ID` |

Save/apply and restart API. These override the published API JSON; no Entra
secret is needed. The default API Memory provider is suitable for a transient
demo only. For persistent recipes/ingredients, provision a database, select
`DALSelectionOptions__Type=EntityFramework`, set
`ConnectionStrings__DefaultConnection`, and apply the API's migrations.

## 5. Azure Web: published JSON, not App Service variables

In the deployment artifact's `wwwroot\appsettings.json`, merge the section 3
Web configuration with these substitutions:

| Setting | Production value |
|---|---|
| `ApiOptions:BaseUrl` | `https://app-iw5-api.azurewebsites.net/` |
| Entra `RedirectUri` | `https://app-iw5-web.azurewebsites.net/authentication/login-callback` |
| Entra `PostLogoutRedirectUri` | `https://app-iw5-web.azurewebsites.net/` |

Keep `IdentityOptions:IsEnabled=true`, selection `Entra`, your tenant/client IDs,
and the API scope. Deploy to **app-iw5-web**. Exclude
`appsettings.Development.json` from the artifact. App Service environment
variables do not configure this static browser app.

## 6. Verify

Run API + Web locally, or open the Azure HTTPS Web URL. In a fresh private
window, log in, create an ingredient, then log out. Reads alone do not prove
API authentication because they are public. Inspect the failed request locally
if a write returns `401`: its access token must have the tenant v2 `iss` and
`aud=API_CLIENT_ID`.
Do not send the API token to Graph UserInfo.

Repeat with both demo accounts in separate private sessions. Inspect the
**API access token**, not the Web ID token: the administrator must have `admin`;
the non-admin must have `user` and no `admin`. The same assignments work locally
and in Azure when sharing registrations; repeat them on separate production
registrations if you use those.

For an explicit role-policy demonstration, uncomment the existing
`ingredientDeleteEndpoint.RequireAuthorization(ApiPolicies.IngredientAdmin)`
line in `CookBook.Api.App\Endpoints\IngredientEndpoints.cs` and rebuild/redeploy
API with identity enabled. Delete a disposable ingredient as admin: it should
succeed. Create a separate disposable ingredient as the non-admin, then try to
delete it: expect **403**, even though that user owns it. Other authenticated
operations remain governed by their own policies/ownership checks; `user` does
not mean read-only. Without enabling the admin policy, deletion alone does not
demonstrate a role restriction. After changing assignments, obtain fresh tokens
by signing out and back in; an already-issued token retains its old roles.

References: [Register apps](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-register-app),
[Expose an API](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-configure-app-expose-web-apis),
[App roles](https://learn.microsoft.com/en-us/entra/identity-platform/howto-add-app-roles-in-apps).
