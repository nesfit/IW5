# Scenario 3: Local accounts plus Entra

[Choose another scenario](authentication-overview.md). Run/deploy **Web, API, and
IdentityProvider**. Web always logs in through CookBook IdentityProvider;
IdentityProvider offers local credentials and an Entra button.

This third teaching example connects the two earlier approaches. It is a
project choice for applications needing both login methods, not a required
upgrade from either local IdentityProvider or direct Entra.

## 1. Complete the local-account setup

Follow [Local accounts](authentication-setup-local.md), including its Azure steps:
identity SQL database, environment-variable factory prerequisite, Duende client
callback/logout/CORS allowlists, API issuer/audience, and Web deployment JSON.
Do not change Web's selection from `CookBookIdentityProvider`.

Only **one Entra registration** is needed for this scenario: IdentityProvider.
The direct-Entra API/SPA registrations are unnecessary unless you also intend
to demonstrate [direct Entra](authentication-setup-entra.md).

## 2. Register IdentityProvider in Entra

In [Entra admin center](https://entra.microsoft.com/), select your tenant:

1. **Entra ID > App registrations > New registration**: name
   `IW5 CookBook IdentityProvider`; choose **Accounts in this organizational
   directory only**.
2. Select platform **Web**, not SPA. Register the application and record
   **Directory (tenant) ID** as `TENANT_ID` and **Application (client) ID** as
   `IDP_CLIENT_ID`.
3. Under **Authentication > Web > Redirect URIs**, add all four entries:

   | Purpose | URI |
   |---|---|
   | Local sign-in | `https://localhost:5001/signin-entra` |
   | Local logout callback | `https://localhost:5001/signout-callback-entra` |
   | Azure sign-in | `https://app-iw5-identity.azurewebsites.net/signin-entra` |
   | Azure logout callback | `https://app-iw5-identity.azurewebsites.net/signout-callback-entra` |

4. Leave implicit grants disabled. Leave **Front-channel logout URL** empty
   for this setup; it is not a logout-return setting.
5. **Certificates & secrets > Client secrets > New client secret**: choose an
   expiry, then copy its **Value**, not Secret ID, as `IDP_CLIENT_SECRET`.
   Use separate local and
   production credentials where practical; record expiry and plan rotation.
6. **App roles > Create app role**: display `CookBook Administrator`, member
   types **Users/Groups**, exact value `admin`, enabled.
7. Create another enabled app role: display `CookBook User`, member types
   **Users/Groups**, exact value `user`.
8. **Entra ID > Users > All users > New user > Create new user**: create two
   demo accounts, or use two existing accounts. Complete first-login password
   changes before testing.
9. **Entra ID > Enterprise applications > IW5 CookBook IdentityProvider >
   Users and groups > Add user/group**: assign each account:

   | Account | Selected app role | Expected Entra ID-token `roles` |
   |---|---|---|
   | Administrator demo | `CookBook Administrator` | Contains `admin` |
   | Non-admin demo | `CookBook User` | Contains `user`, not `admin` |

Ensure the non-admin has no direct or group-based `admin` assignment here.
These assignments also satisfy this enterprise application's **Assignment
required?** policy when enabled. Both role definitions use **Users/Groups**;
assign individuals for this demonstration. These are not Entra directory roles.

The registration does not need a delegated CookBook API permission. Entra
authenticates the user to IdentityProvider, which issues its own API tokens.
The Entra role must be assigned on the **IdentityProvider** enterprise app,
not the direct-login API enterprise app. The configured OIDC scopes are
`openid`, `profile`, and `email`; Graph `User.Read` is not required.

## 3. Local IdentityProvider: user secrets

Open **CookBook.IdentityProvider.App > Manage User Secrets** and merge this
object alongside your existing SQL connection string:

```json
{
  "CookBookIdentityProviderOptions:Local:IsEnabled": true,
  "CookBookIdentityProviderOptions:ExternalProviders:0": {
    "IsEnabled": true,
    "Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
    "ClientId": "IDP_CLIENT_ID",
    "ClientSecret": "IDP_CLIENT_SECRET",
    "UpstreamLogoutIsEnabled": true
  }
}
```

Replace all placeholders. Keep all local overrides here, not in shared
`appsettings.json`. In particular, replace `IDP_CLIENT_SECRET` with the client
secret **Value** copied from Entra. The same searchable placeholder appears in
`CookBook.IdentityProvider.App/appsettings.json`; override it through user
secrets locally or App Service settings in Azure, never commit the real value.
Index `0` inherits scheme `Entra`, provider type, callback
paths, and claim mappings. Do not also define flattened keys targeting the same
properties. Restart with the Development `SelfHost` profile.

Web and API keep the local-account guide's configuration unchanged:

| Application | Setting | Value |
|---|---|---|
| Web | Selected provider | `CookBookIdentityProvider` |
| Web | Authority | `https://localhost:5001` |
| Web | Client ID | `cookbookclient` |
| Web | Scopes | `openid`, `profile`, `cookbookapi` |
| API | CookBook issuer, index `0` | `https://localhost:5001` |
| API | CookBook audience | `cookbookclientaudience` |

Do not substitute `IDP_CLIENT_ID` into the Web client or API audience.

## 4. Azure IdentityProvider: App Service settings

Retain the local-account guide's production SQL, HTTPS, client allowlists, and
key-storage setup. In **App Services > app-iw5-identity > Settings >
Environment variables > App settings**, add/update:

| Name | Value |
|---|---|
| `CookBookIdentityProviderOptions__Local__IsEnabled` | `true` |
| `CookBookIdentityProviderOptions__ExternalProviders__0__IsEnabled` | `true` |
| `CookBookIdentityProviderOptions__ExternalProviders__0__Authority` | `https://login.microsoftonline.com/TENANT_ID/v2.0` |
| `CookBookIdentityProviderOptions__ExternalProviders__0__ClientId` | `IDP_CLIENT_ID` |
| `CookBookIdentityProviderOptions__ExternalProviders__0__ClientSecret` | Replace `IDP_CLIENT_SECRET` with the production secret **Value**, preferably using a Key Vault reference |
| `CookBookIdentityProviderOptions__ExternalProviders__0__UpstreamLogoutIsEnabled` | `true` |

Save/apply and restart. Never put the server credential in Web, source control,
or a screenshot. The API and Web Azure settings remain exactly those in the
local-account guide:

| Application | Production value |
|---|---|
| Web API base URL | `https://app-iw5-api.azurewebsites.net/` |
| Web authority / API CookBook authority and issuer | `https://app-iw5-identity.azurewebsites.net` |
| Web callback | `https://app-iw5-web.azurewebsites.net/authentication/login-callback` |
| Web logout destination | `https://app-iw5-web.azurewebsites.net/` |
| API audience | `cookbookclientaudience` |

Entra returns to IdentityProvider's `/signin-entra`, **not** directly to Web's
callback. Both allowlists are required: Entra's Web platform entries above and
Duende's Web-client entries from the local-account guide.

## 5. Verify both choices

Use separate private sessions to test local credentials and the Entra button.
For each, return to Web, confirm a readable name, create an ingredient, then
log out. API tokens in both cases have the **CookBook IdentityProvider** issuer
and `cookbookclientaudience`, not an Entra issuer or Entra API audience.

Test the Entra button with both demo accounts in separate private sessions.
IdentityProvider accepts the `user` and `admin` values from the validated Entra
ID token and forwards roles into its downstream API token. Check that the final
**CookBook API access token** contains `admin` for the admin account and `user`
without `admin` for the non-admin. Local roles are also merged: a linked account
with local `admin` membership is still an admin, so remove that membership from
the non-admin demo account through your trusted Identity provisioning process.
No Web/API configuration change is needed to distinguish the two users.

Ingredient deletion is admin-only (`AdminOnlyRequirement` in
`CookBook.Api.App\Endpoints\IngredientEndpoints.cs`). The admin can delete a
disposable ingredient; the non-admin must receive **403** when deleting another
disposable ingredient, even one they created. Updating ingredients/recipes and
deleting recipes require owner or admin; a `user` account is not read-only.

These Entra assignments apply to both local and Azure callbacks of this
registration; repeat them if production uses a separate registration. After
changing roles, sign out of Web and IdentityProvider and perform a fresh Entra
login, rather than silently reusing the IdentityProvider session. Existing
cookies/tokens retain old roles. Test local-account roles separately using
ASP.NET Identity role membership, not Entra assignments.

For external-only login through IdentityProvider, set
`CookBookIdentityProviderOptions:Local:IsEnabled=false` locally (Azure:
`CookBookIdentityProviderOptions__Local__IsEnabled=false`), leaving Entra enabled.
This is still the combined guide's architecture, not direct Entra.

For a logout demonstration, change external `UpstreamLogoutIsEnabled` in
IdentityProvider settings, restart, and establish a fresh login. `false` clears
the CookBook session but leaves the Entra session; `true` also requests Entra
logout. Microsoft/Windows SSO may still avoid a password prompt. Already-issued
access tokens are not all revoked by logout.

## Account linking and database expectations

The external account key is provider plus immutable `tid:oid`. The database row
ID and local `Subject` are separate identifiers and should not be replaced with
Entra's IDs. Existing accounts keep their stored username; external login
refreshes a display-only claim for the UI and UserInfo endpoint.

Automatic linking to a local account requires **exactly one normalized email
match and explicit `email_verified=true`**. Ordinary workforce Entra tokens
usually lack that claim, so a separate passwordless shadow account is normally
created. `email` or `preferred_username` alone is not proof of email ownership.
There is no normal portal checkbox that safely enables this linking; proving
ownership of the existing local account needs additional application logic.
Do not delete users or loosen linking rules to make names match.
