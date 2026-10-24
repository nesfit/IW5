# Entra ID setup through the web portal

This walkthrough configures Microsoft Entra ID for both supported authentication paths:

- **Direct:** CookBook Web signs users in with Entra and calls CookBook API with an Entra access token.
- **Federated:** CookBook Web signs users in through CookBook IdentityProvider, which offers local accounts and/or external Entra login and issues its own downstream tokens.

For all demonstrations, create three Entra app registrations. Only the first two are needed for direct authentication.

| Registration | Purpose | Credential |
|---|---|---|
| IW5 CookBook API | Represents the protected API and exposes its delegated scope | No client secret needed |
| IW5 CookBook Web | Public browser SPA for direct Entra login | No client secret |
| IW5 CookBook IdentityProvider | Confidential server application for federated Entra login | Server-side secret or certificate |

## Local addresses and prerequisites

The following addresses match the project's local launch settings:

| Application | Local URL |
|---|---|
| CookBook Web | `https://localhost:44355` |
| CookBook API | `https://localhost:44378` |
| CookBook IdentityProvider | `https://localhost:5001` |

You need access to a workforce Entra tenant, permission to create app registrations, and permission to obtain consent and assign demo users where tenant policy requires it. Use single-tenant registrations throughout this walkthrough.

The portal navigation labels may vary slightly. Start at [Microsoft Entra admin center](https://entra.microsoft.com/); the corresponding Microsoft Entra ID pages are also available through the Azure portal.

> **Logout terminology:** A post-logout redirect URI is the destination the application requests after logout. The portal's **Front-channel logout URL** is a different feature: an endpoint that receives logout notifications. Do not use that field as the post-logout destination.

## 1. Select the tenant

1. Sign in to the Entra admin center.
2. Use the directory switcher to select the tenant students will use.
3. Open **Entra ID > Overview**.
4. Copy **Tenant ID** and record it as `TENANT_ID`.

Use the same tenant for all registrations. Whenever creating an application, choose **Accounts in this organizational directory only**.

The authority used below is:

```text
https://login.microsoftonline.com/TENANT_ID/v2.0
```

Replace `TENANT_ID` with the actual GUID, without angle brackets. Do not use `common` or `organizations` for this single-tenant example.

The checked-in configuration uses the same placeholder names as this guide:

| Placeholder | Value to record |
|---|---|
| `TENANT_ID` | Directory (tenant) ID |
| `API_CLIENT_ID` | Application (client) ID of IW5 CookBook API |
| `WEB_CLIENT_ID` | Application (client) ID of IW5 CookBook Web |
| `IDP_CLIENT_ID` | Application (client) ID of IW5 CookBook IdentityProvider |

These are literal placeholders, not automatically substituted environment variables.
Keep shared `appsettings.json` files unchanged when configuring your own tenant:
use API user secrets and Web's local development file as described in section 5,
and IdentityProvider user secrets as described in section 7.
Never put a client secret in the browser application's configuration.

## 2. Register CookBook API

### Create the registration

1. Open **Entra ID > App registrations**.
2. Select **New registration**.
3. Enter the name `IW5 CookBook API`.
4. Choose **Accounts in this organizational directory only**.
5. Leave **Redirect URI** empty. The API does not perform interactive login.
6. Select **Register**.
7. On **Overview**, copy **Application (client) ID** and record it as `API_CLIENT_ID`.

The API does not need a client secret for this flow. It validates incoming tokens rather than authenticating itself to Entra.

### Expose the delegated scope

1. In this registration, open **Expose an API**.
2. Beside **Application ID URI**, select **Add** or **Set**.
3. Accept `api://API_CLIENT_ID` and save.
4. Select **Add a scope**.
5. Enter:

| Field | Value |
|---|---|
| Scope name | `access_as_user` |
| Who can consent | `Admins and users`, if tenant policy permits |
| Admin consent display name | `Access CookBook as the signed-in user` |
| Admin consent description | `Allow the application to call the CookBook API on behalf of the signed-in user.` |
| User consent display name | `Access CookBook on your behalf` |
| User consent description | `Allow CookBook Web to call the CookBook API on your behalf.` |
| State | `Enabled` |

6. Select **Add scope**.

The complete scope is:

```text
api://API_CLIENT_ID/access_as_user
```

Choosing **Admins and users** does not override tenant-wide restrictions on user consent. An administrator can grant consent later.

### Request v2 access tokens

The API configuration below expects a v2 issuer and a GUID audience.

1. Open **Manifest** in `IW5 CookBook API`.
2. In the Microsoft Graph-format manifest, locate the existing `api` object.
3. Set its `requestedAccessTokenVersion` property to `2`.
4. Preserve all other properties, including the scope just created.
5. Save.

Relevant fragment:

```json
"api": {
  "requestedAccessTokenVersion": 2
}
```

**Do not replace the entire `api` object with this fragment.** Change only that property. Using a `/v2.0` authorization endpoint alone does not force an API's access tokens to be version 2.

## 3. Create and assign the API administrator role

The delegated scope allows the browser to request access on a user's behalf. An application role represents that user's privileges.

1. In `IW5 CookBook API`, open **App roles**.
2. Select **Create app role**.
3. Enter:

| Field | Value |
|---|---|
| Display name | `CookBook Administrator` |
| Allowed member types | `Users/Groups` |
| Value | `admin` |
| Description | `Administrator access in CookBook.` |
| Enable this app role | Checked |

4. Select **Apply**.

Use the exact lowercase value `admin`, matching CookBook's role constant. This is not the Entra directory role **Global Administrator**.

Assign the role:

1. Open **Entra ID > Enterprise applications > All applications**.
2. Find `IW5 CookBook API`. Verify the application ID if names are duplicated.
3. Open **Users and groups**.
4. Select **Add user/group**.
5. Select the demo user.
6. Under **Select a role**, choose `CookBook Administrator`.
7. Select **Assign**.

For a simple demonstration, assign individual users. Group-based assignment has additional licensing requirements.

## 4. Register CookBook Web for direct login

### Create a SPA registration

1. Open **Entra ID > App registrations > New registration**.
2. Name it `IW5 CookBook Web`.
3. Choose the single-tenant account option.
4. Under **Redirect URI**, select **Single-page application (SPA)**.
5. Enter:

   ```text
   https://localhost:44355/authentication/login-callback
   ```

6. Select **Register**.
7. Copy **Application (client) ID** and record it as `WEB_CLIENT_ID`.

### Check authentication settings

1. Open **Authentication**.
2. Confirm the callback is under **Single-page application**, not **Web**.
3. Add this additional redirect URI to the same SPA platform for the configured logout return destination:

   ```text
   https://localhost:44355/
   ```

4. Save.
5. Leave implicit-grant access-token and ID-token checkboxes disabled. This setup uses authorization code flow with PKCE.
6. Do not enable public-client flows for device-code/password grants; this SPA flow does not need that setting.
7. Do not create a client secret. A browser cannot keep one confidential.

### Grant permission to CookBook API

1. In `IW5 CookBook Web`, open **API permissions**.
2. Select **Add a permission > My APIs**.
3. Select `IW5 CookBook API`.
4. Select **Delegated permissions**.
5. Check `access_as_user`.
6. Select **Add permissions**.
7. If authorized, select **Grant admin consent for your tenant** and confirm.

If the API is absent from **My APIs**, verify that both registrations belong to the same tenant and that you are an owner of them. The portal may also offer a search for APIs your organization uses.

The default Microsoft Graph `User.Read` permission does not grant access to CookBook API. The CookBook scope above is the relevant permission.

## 5. Configure direct Entra login in CookBook

Replace the placeholders with the values recorded above. These are application configuration changes, not portal settings.

### API configuration

Use **.NET user secrets**, not edits to the shared API `appsettings.json`.
The API project already declares a `UserSecretsId`; no initialization is needed
for this checkout.

1. In Visual Studio, right-click **CookBook.Api.App > Manage User Secrets**.
   This opens its local `secrets.json`, stored outside the repository.
2. Merge the following keys into that file. Preserve unrelated existing keys,
   such as database connection settings. Replace `TENANT_ID` and `API_CLIENT_ID`
   with your actual GUIDs.

```json
{
  "IdentityOptions:IsEnabled": true,
  "TrustedIssuersOptions:Issuers:1:Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
  "TrustedIssuersOptions:Issuers:1:ValidIssuers:0": "https://login.microsoftonline.com/TENANT_ID/v2.0",
  "TrustedIssuersOptions:Issuers:1:ValidAudiences:0": "API_CLIENT_ID"
}
```

3. Save and restart the API using the **CookBook.Api** launch profile.
   Its `ASPNETCORE_ENVIRONMENT` is `Development`, which enables automatic user
   secrets loading. Without `Development`, these values are not loaded by default.

Alternatively, from the repository root, run these PowerShell commands after
replacing the placeholders:

```powershell
dotnet user-secrets set "IdentityOptions:IsEnabled" "true" --project src\CookBook\CookBook.Api.App
dotnet user-secrets set "TrustedIssuersOptions:Issuers:1:Authority" "https://login.microsoftonline.com/TENANT_ID/v2.0" --project src\CookBook\CookBook.Api.App
dotnet user-secrets set "TrustedIssuersOptions:Issuers:1:ValidIssuers:0" "https://login.microsoftonline.com/TENANT_ID/v2.0" --project src\CookBook\CookBook.Api.App
dotnet user-secrets set "TrustedIssuersOptions:Issuers:1:ValidAudiences:0" "API_CLIENT_ID" --project src\CookBook\CookBook.Api.App
```

The tracked API configuration defines CookBook at issuer index `0` and Entra at
index `1`; the commands override only Entra's tenant-specific values. Configuration
arrays merge **by numeric index**, not by `Scheme`. If you previously removed or
reordered issuer entries in shared configuration, restore this layout or adjust
the secret keys to match. Review older user secrets for stale values at either
index before troubleshooting an unexpected effective issuer.

User secrets override `appsettings.json` and `appsettings.Development.json`.
Environment variables and command-line settings can override user secrets.
Use `dotnet user-secrets list --project src\CookBook\CookBook.Api.App` to inspect
your local overrides, but do not share its output if it contains credentials.
To revert one override, use `dotnet user-secrets remove "<configuration-key>"
--project src\CookBook\CookBook.Api.App` and restart the API. Do not use `clear`
unless you intend to remove all this project's secrets.

User secrets are a development convenience, **not an encrypted production vault**.
Tenant/client IDs are not passwords, but keeping these local values outside the
repository avoids changing the teaching defaults.

For these v2 access tokens, the audience is the API's client ID GUID, not `WEB_CLIENT_ID` or the complete scope URL.

This layout continues to trust both configured issuers. Selecting Entra in Web
does not disable the API's CookBook issuer.

### Web configuration

Use `src\CookBook\CookBook.Web.App\wwwroot\appsettings.Development.json`.
Do not edit the shared `wwwroot\appsettings.json` for your own tenant.

1. In Visual Studio, expand **CookBook.Web.App > wwwroot**.
2. If `appsettings.Development.json` does not exist, add a JSON file with that name
   **inside `wwwroot`**, not alongside the project file.
3. If it already exists, merge the following settings into it without discarding
   unrelated local values.
4. Replace `TENANT_ID`, `WEB_CLIENT_ID`, and `API_CLIENT_ID`.

```json
{
  "ApiOptions": {
    "BaseUrl": "https://localhost:44378/"
  },
  "IdentityOptions": {
    "IsEnabled": true
  },
  "WebAuthenticationOptions": {
    "SelectedProvider": "Entra",
    "Providers": [
      {
        "ProviderType": "CookBookIdentityProvider",
        "IsEnabled": true,
        "Authority": "https://localhost:5001",
        "ClientId": "cookbookclient",
        "ResponseType": "code",
        "RedirectUri": "https://localhost:44355/authentication/login-callback",
        "PostLogoutRedirectUri": "https://localhost:44355",
        "DefaultScopes": [ "openid", "profile", "cookbookapi" ],
        "UpstreamLogoutIsEnabled": true
      },
      {
        "ProviderType": "Entra",
        "IsEnabled": true,
        "Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
        "ClientId": "WEB_CLIENT_ID",
        "ResponseType": "code",
        "RedirectUri": "https://localhost:44355/authentication/login-callback",
        "PostLogoutRedirectUri": "https://localhost:44355/",
        "DefaultScopes": [
          "openid",
          "profile",
          "api://API_CLIENT_ID/access_as_user"
        ],
        "UpstreamLogoutIsEnabled": true
      }
    ]
  }
}
```

5. Save, restart Web, and reload the browser.
6. In Developer Tools **Network**, inspect the `appsettings.Development.json`
   response to confirm that the browser received your local settings.

Keep both provider entries in the displayed order: CookBook at index `0`, Entra
at index `1`. Arrays merge by index, not `ProviderType`; putting only Entra at
index `0` would merge it with the CookBook entry and leave the original Entra
entry at index `1`.

The repository already ignores `appsettings.*.json`, including this local file.
Do not force-add it to Git. Web's `Program.cs` explicitly loads the optional
development file **after** the shared configuration, so its keys take precedence.
That explicit load is currently not conditional on the environment: keep this
local file out of deployment output, even when publishing a Release build.
To return to defaults, remove the relevant overrides or rename the local file
outside `wwwroot`, then restart and reload.

Everything in Web's `wwwroot` is public. Tenant IDs and client IDs may be there; secrets must not.
Being Git-ignored does not make this file private: the browser downloads it.
Server-side user secrets cannot provide private configuration to a WebAssembly
client.

Web selects `AddMsalAuthentication` and its matching JavaScript adapter for direct Entra login. MSAL uses the ID token for the signed-in user's identity and requests the CookBook API scope for API calls; it does not send that API token to Microsoft Graph's UserInfo endpoint. The `CookBookIdentityProvider` selection continues to use generic OIDC.

Keep `openid` and `profile` in the provider configuration if switching between examples. The Entra adapter excludes OIDC scopes (`openid`, `profile`, `email`, `offline_access`) from `DefaultAccessTokenScopes` because MSAL handles its protocol scopes itself. No Graph `User.Read` permission is needed for this login flow.

After updating from the older generic-OIDC implementation, restart Web and begin a new login from the home page, not an old `/authentication/login-callback` URL. Use a fresh private window to avoid obsolete OIDC callback state. If the browser still runs old assets, unregister CookBook's service worker and clear this site's storage through Developer Tools before reloading. Clearing site storage also removes locally cached application data.

## 6. Register Entra login for CookBook IdentityProvider

This is a confidential **Web** application: the server exchanges the authorization code.

1. Open **App registrations > New registration**.
2. Name it `IW5 CookBook IdentityProvider`.
3. Choose the single-tenant account option.
4. Under **Redirect URI**, select **Web**.
5. Enter:

   ```text
   https://localhost:5001/signin-entra
   ```

6. Select **Register**.
7. Copy its client ID and record it as `IDP_CLIENT_ID`.
8. Open **Authentication**.
9. Add this second redirect URI under the **Web** platform:

   ```text
   https://localhost:5001/signout-callback-entra
   ```

10. Save. Leave implicit grant disabled.

Do not register Blazor's `/authentication/login-callback` here. In this flow Entra redirects to CookBook IdentityProvider, which subsequently redirects to Blazor.

### Create the server credential

1. Open **Certificates & secrets**.
2. Under **Client secrets**, select **New client secret**.
3. Enter a description such as `Local teaching demonstration`.
4. Choose an expiry consistent with organizational policy.
5. Select **Add**.
6. Immediately copy the secret's **Value**; it will not be displayed again.

Use the **Value**, not the **Secret ID**. Store it in the complete external-provider
override object in IdentityProvider user secrets, as shown in section 7, rather
than configuring only `ClientSecret`. Do not put the value in `appsettings.json`.
Use protected hosting configuration or Key Vault for deployment, and plan
credential rotation before expiry.

### Configure federated administrator roles

Repeat section 3's role creation on `IW5 CookBook IdentityProvider`, using value `admin`. Assign the demo user that role in its corresponding **Enterprise application**.

This duplication is intentional:

| Login path | Role assignment location | Token containing the role |
|---|---|---|
| Web directly to Entra | CookBook API enterprise application | API access token |
| Web through CookBook IdentityProvider | CookBook IdentityProvider enterprise application | Entra ID token received by IdentityProvider |

Assigning the API role does not automatically assign the IdentityProvider role.

The IdentityProvider registration does not need the CookBook API delegated scope merely to authenticate external users. In the federated flow, CookBook IdentityProvider issues its own downstream API token.

## 7. Configure federated login in CookBook

### IdentityProvider user secrets

Use **.NET user secrets for all local IdentityProvider overrides**, not edits to
`src\CookBook\CookBook.IdentityProvider.App\appsettings.json`.

1. In Visual Studio, right-click **CookBook.IdentityProvider.App > Manage User
   Secrets**. Select the App project, not IdentityProvider.DAL or CookBook.Api.App:
   each project has its own secrets store.
2. The App project already declares `UserSecretsId`
   `31fe5f25-e562-4895-8a2d-98f3c032c123`, so no initialization is required.
   On Windows its file is
   `%APPDATA%\Microsoft\UserSecrets\31fe5f25-e562-4895-8a2d-98f3c032c123\secrets.json`.
3. Merge this example into that file. Preserve existing entries, particularly
   `ConnectionStrings:DefaultConnection` for your identity database.
4. Replace `TENANT_ID`, `IDP_CLIENT_ID`, and `YOUR_SECRET_VALUE` with the actual
   values from the IdentityProvider Web registration:

```json
{
  "CookBookIdentityProviderOptions:Local:IsEnabled": true,
  "CookBookIdentityProviderOptions:ExternalProviders:0": {
    "IsEnabled": true,
    "Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
    "ClientId": "IDP_CLIENT_ID",
    "ClientSecret": "YOUR_SECRET_VALUE",
    "UpstreamLogoutIsEnabled": true
  }
}
```

5. Save and restart IdentityProvider with its **SelfHost** launch profile.
   That profile sets `ASPNETCORE_ENVIRONMENT=Development`, enabling automatic
   loading of the App project's user secrets.

The colon-delimited object key targets provider index `0`, while its nested
properties override that provider's individual settings. It does **not** replace
the entire provider entry or collection. `Scheme`, `ProviderType`, display name,
claim mappings, and callback paths are inherited from shared configuration.
The inherited callback paths remain `/signin-entra` and
`/signout-callback-entra`, matching section 6's portal registration.

Index `0` assumes Entra remains the first external provider. Configuration merges
arrays by index, not by scheme. If you already have flattened keys such as
`CookBookIdentityProviderOptions:ExternalProviders:0:ClientSecret`, consolidate
them into the object above rather than defining the same effective property
twice in one JSON file.

For hybrid login, keep `CookBookIdentityProviderOptions:Local:IsEnabled` set to
`true` in **user secrets**. Set it to `false` there for external-only login,
leaving Entra enabled. For the logout demonstration, change
`UpstreamLogoutIsEnabled` inside the same secret object and restart the app.
Keep the checked-in `ClientSecret` empty.

User secrets override the shared JSON configuration in Development; environment
variables and command-line settings can override them in turn. User secrets are
not encrypted and are not a production vault. Never copy the real secret into
Web's `wwwroot`, source control, screenshots, or shared diagnostic output.

### Web and API local overrides

In Web's local `wwwroot\appsettings.Development.json`, select
`CookBookIdentityProvider`, ensure that provider is enabled, and update its entry:

| Setting | Value |
|---|---|
| `Authority` | `https://localhost:5001` |
| `ClientId` | `cookbookclient` |
| `RedirectUri` | `https://localhost:44355/authentication/login-callback` |
| `PostLogoutRedirectUri` | `https://localhost:44355` |
| `DefaultScopes` | `openid`, `profile`, `cookbookapi` |

`cookbookclient` is the Duende client ID defined in CookBook's `Config.cs`, not an Entra client ID. The logout destination above matches the current Duende allowlist; keep both consistent when changing it.

In API user secrets, override the CookBook issuer at index `0` for local use:

```powershell
dotnet user-secrets set "TrustedIssuersOptions:Issuers:0:Authority" "https://localhost:5001" --project src\CookBook\CookBook.Api.App
dotnet user-secrets set "TrustedIssuersOptions:Issuers:0:ValidIssuers:0" "https://localhost:5001" --project src\CookBook\CookBook.Api.App
```

Keep its audience `cookbookclientaudience`. Restart API after saving. Leave the
Entra issuer overrides at index `1` if the API should accept both token sources.

## 8. Account-linking limitation

Entra's ordinary `email` claim is not proof that an email address was verified. Adding `email` under **Token configuration > Add optional claim > ID** can make the address available but does not make it verified.

The current callback requires an explicit `email_verified=true` claim before linking by email. Standard workforce Entra tokens generally do not provide this claim. Normal portal configuration will therefore usually provision a separate shadow account rather than automatically link an existing local account.

There is no ordinary "enable verified email" checkbox that resolves this. Do not treat `preferred_username` or the presence of `email` as verification. Secure same-account linking with standard Entra requires an additional application-side mechanism, such as proving ownership of the existing local account. Portal configuration alone does not implement that mechanism.

## 9. Run the teaching scenarios

Start in a private browser window to reduce confusion from existing sessions.

| Demonstration | API identity | Web identity | Web selection | IdentityProvider |
|---|---|---|---|---|
| No authentication | Disabled | Disabled | Ignored | Not required |
| Direct Entra | Enabled | Enabled | `Entra` | Not required |
| Local accounts | Enabled | Enabled | `CookBookIdentityProvider` | Local enabled; Entra disabled |
| Hybrid login | Enabled | Enabled | `CookBookIdentityProvider` | Local and Entra enabled |
| External-only through IdentityProvider | Enabled | Enabled | `CookBookIdentityProvider` | Local disabled; Entra enabled |

For direct login, run API and Web. Also run IdentityProvider for local or federated login. Its currently selected persistent user store needs a working identity database; configuring Entra does not configure that database.

Check that login returns to Web, API reads load, and authenticated modifying requests reach the API. To demonstrate administrator authorization, use an endpoint whose admin policy is actually enabled; assigning a role does not by itself enable the sample's commented-out endpoint policies.

For the upstream-logout demonstration:

1. Keep Web's logout directed at CookBook IdentityProvider so its server session is cleared.
2. Change the external provider's `UpstreamLogoutIsEnabled` in IdentityProvider
   user secrets (the override object in section 7).
3. Restart IdentityProvider.
4. Establish a new login session before testing logout.
5. Compare the next login with upstream logout enabled versus disabled.

Logout does not revoke every already-issued access token. Existing Microsoft/Windows sessions can also allow passwordless sign-in again even after upstream logout, so a missing password prompt is not sufficient evidence that logout failed.

## 10. Troubleshooting

| Symptom | Check |
|---|---|
| `AADSTS50011` | Register the exact `redirect_uri` sent by the failing app under the correct SPA/Web platform. |
| `AADSTS7000215` | Use the secret Value, not Secret ID; check expiry and the owning registration. |
| `AADSTS700016` | Check the client ID and tenant authority. |
| `AADSTS65001` / consent required | Add the delegated CookBook API permission and obtain required consent. |
| `AADSTS50105` | The enterprise application's assignment-required policy is enabled but the user is not assigned. |
| API returns `401` | Check v2 access-token configuration, issuer, audience, and the requested API scope. A Graph token is not a CookBook token. |
| No `admin` role | Define and assign it on the registration producing the relevant token, then establish a new login session. |
| Existing local account not linked | An email address alone is insufficient; see section 8. |
| Browser token-redemption/CORS error | Verify the callback is registered as SPA, the correct Web client ID is selected, and the MSAL adapter is loaded as described in section 5. |
| Graph `/oidc/userinfo` returns `401` with `Invalid audience` | An old generic OIDC client is sending a CookBook API token to Graph. Confirm `SelectedProvider` is `Entra`, restart with the MSAL implementation, and reload fresh assets. Do not change the API audience or request Graph access as a workaround. |

For deployment, register the deployed callback and logout-return addresses under the corresponding platforms and update CookBook configuration. Localhost registrations do not cover Azure-hosted URLs.

## Microsoft references

- [Register an application](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-register-app)
- [Expose a web API and create scopes](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-configure-app-expose-web-apis)
- [Create app roles and assign users](https://learn.microsoft.com/en-us/entra/identity-platform/howto-add-app-roles-in-apps)
- [Application manifest settings](https://learn.microsoft.com/en-us/entra/identity-platform/reference-microsoft-graph-app-manifest)
- [ID token claims and stable user identifiers](https://learn.microsoft.com/en-us/entra/identity-platform/id-token-claims-reference)
