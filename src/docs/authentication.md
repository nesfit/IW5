# CookBook authentication scenarios

CookBook supports identity-disabled demonstrations, direct Microsoft Entra ID authentication, local authentication through `CookBook.IdentityProvider.App`, and combined local plus external login through that IdentityProvider.

## Scenario matrix

| Scenario | API `IdentityOptions:IsEnabled` | Web `IdentityOptions:IsEnabled` | Web selected provider | IdentityProvider login |
|---|---:|---:|---|---|
| No identity | `false` | `false` | ignored | not required |
| Direct Entra | `true` | `true` | `Entra` | not required |
| Local CookBook identity | `true` | `true` | `CookBookIdentityProvider` | local enabled |
| Local + Entra | `true` | `true` | `CookBookIdentityProvider` | local and Entra enabled |

The API always registers every entry in `TrustedIssuersOptions`, even when endpoint authorization is disabled. `IdentityOptions:IsEnabled` only determines whether modifying endpoint groups require authorization.

Direct Entra login uses MSAL; CookBook IdentityProvider uses generic OIDC.
Web loads only the JavaScript adapter for the selected provider. Both paths
continue to supply API tokens through `IAccessTokenProvider`.

The login header displays `username` for CookBook accounts or
`preferred_username` for Entra accounts, falling back to a display name or email.
Opaque subject/object identifiers remain account identifiers, not UI labels;
readable claims are never used as authorization identifiers.

For external login through IdentityProvider, both the IdentityProvider header
and the federated Web session use the current provider's `preferred_username`,
falling back to `name` or email, or `signed-in user` when none is available.
The generated shadow-account username stays internal; no database rename is
needed for existing users. Each external login saves or refreshes a display-only
`cookbook:external_display_name` claim in the existing user-claims store. This
allows UserInfo/profile requests containing only `sub` to resolve the readable
name without relying on login-cookie claims. The internal claim is not returned
to clients; the profile service emits the usual `name` and `username` labels.
A profile request with an explicit local identity provider keeps the stored
local username; a subject-only request uses the last saved external label.
These labels do not verify an email or link accounts.
After updating, restart IdentityProvider and sign out and back in to replace
the old display name stored in the login cookie and downstream tokens.
Existing users receive the display claim on their next external login; do not
delete or recreate them. The local database `Id` and IdentityProvider `Subject`
are intentionally separate from Entra's identifiers. The external-login mapping
stores the provider scheme and `tid:oid` key.

## Entra app registrations

For detailed portal steps, configuration examples, and current implementation
limitations, see [Entra ID portal setup](entra-id-portal-setup.md).

Create three single-tenant registrations.

### CookBook API

1. Expose `api://<api-client-id>/access_as_user`.
2. Add an app role with value `admin`, allowed for users/groups.
3. Assign users or groups through the API Enterprise Application.
4. Put the tenant-specific v2 issuer and API client ID into the API's Entra `TrustedIssuersOptions` entry.

### CookBook Web

1. Configure it as a public SPA client.
2. Add `https://localhost:44355/authentication/login-callback` as a SPA redirect URI.
3. Add `https://localhost:44355/` as an additional SPA redirect URI for the configured logout return destination; this is not the front-channel logout URL.
4. Grant delegated permission to the CookBook API `access_as_user` scope.
5. Put its client ID, tenant authority, and API scope into the Web `Entra` provider registration.

### CookBook IdentityProvider

1. Configure it as a confidential web client.
2. Add the configured `/signin-entra` callback and `/signout-callback-entra` signed-out callback.
3. Add an app role with value `admin`; this role is emitted in the ID token used for federated login.
4. Right-click **CookBook.IdentityProvider.App > Manage User Secrets** in Visual
   Studio. Merge this object into the existing `secrets.json`, preserving database
   connection settings, and replace the placeholders:

   ```json
   {
     "CookBookIdentityProviderOptions:ExternalProviders:0": {
       "IsEnabled": true,
       "Authority": "https://login.microsoftonline.com/TENANT_ID/v2.0",
       "ClientId": "IDP_CLIENT_ID",
       "ClientSecret": "YOUR_SECRET_VALUE",
       "UpstreamLogoutIsEnabled": true
     }
   }
   ```

5. Run IdentityProvider in **Development** and restart after changing secrets.
   Use this secrets store for all local IdentityProvider overrides instead of
   editing shared `appsettings.json`. The object overrides individual fields of
   external provider index `0`; other fields are inherited. See
   [the complete user-secrets instructions](entra-id-portal-setup.md#identityprovider-user-secrets)
   for local-login settings and configuration precedence.

## External account linking

The IdentityProvider first looks up an existing external login by immutable provider plus subject. For Entra the key is tenant ID plus object ID (`tid:oid`). If no link exists, it links to a local account only when:

- the provider positively emits `email_verified=true`;
- exactly one local account has that normalized email.

Username matches and unverified email claims are never used for linking. If there is no match, a passwordless shadow account is provisioned. An existing matched account keeps its password, subject, profile, and local roles, so either login path reaches the same account.

Standard workforce Entra tokens generally do not contain `email_verified=true`.
Adding an optional `email` claim does not satisfy this check. See the
[account-linking limitation](entra-id-portal-setup.md#8-account-linking-limitation)
before demonstrating linking with Entra.

## Test users versus persistent users

`CookBook.IdentityProvider.App/HostingExtensions.cs` contains the two adjacent registration pairs. Keep exactly one pair active:

```csharp
// In-memory teaching store
identityServerBuilder.AddTestUsers(TestUsers.Users);
builder.Services.AddScoped<IExternalUserService, TestStoreExternalUserService>();

// Persistent store
identityServerBuilder.AddProfileService<LocalAppUserProfileService>();
builder.Services.AddScoped<IExternalUserService, PersistentExternalUserService>();
```

The external callback does not change when switching stores.

## Upstream logout

Each provider has an upstream logout switch. When disabled, CookBook clears its local session but leaves the upstream provider session intact, demonstrating single sign-on on the next challenge. When enabled, CookBook also challenges the upstream sign-out endpoint when the handler supports it.

## Adding another provider

Add an `AuthenticationProviderType`, provider-specific registration adapter, and claim mapping. A provider used directly by Web must also produce an access token that the CookBook API can independently validate for the API audience. Google/Facebook login tokens do not automatically satisfy that requirement; route them through CookBook IdentityProvider or implement an explicit token-exchange/backend design.

## Troubleshooting

- **AADSTS redirect mismatch:** redirect URI and URI type must exactly match the app registration.
- **401 from API:** inspect `iss` and `aud`; both must match one configured trusted issuer entry.
- **401 after CookBook IdentityProvider login:** the `cookbookapi` scope must
  belong to the `cookbookclientaudience` API resource in IdentityProvider's
  `Config.cs`. The API trusts that resource name as its audience, not the Web
  client ID. An unassociated scope does not produce this audience; with static
  audience emission enabled, the token can instead contain only the generic
  `<issuer>/resources` audience. Keep audience validation enabled, correct the
  resource/scope association, restart IdentityProvider, and obtain a new access
  token by signing out and back in.
- **403 for admin action:** assign `admin` on the app registration that emits the relevant token (API registration for direct mode, IdentityProvider registration for federated mode).
- **Immediate login after logout:** upstream logout is disabled or the external provider retained its session.
- **No external login button:** the provider registration is disabled or local-only mode is configured.
- **Account not linked:** the email claim is absent/unverified, or more than one normalized local email matches.

Never place client secrets in checked-in `appsettings.json`.

## Authentication regression tests

From the repository root:

```powershell
dotnet test src\CookBook\CookBook.Web.BL.UnitTests\CookBook.Web.BL.UnitTests.csproj
dotnet test src\CookBook\CookBook.IdentityProvider.App.UnitTests\CookBook.IdentityProvider.App.UnitTests.csproj
node --test src\CookBook\CookBook.Web.BL.UnitTests\authentication-loader.test.cjs
```

The .NET tests cover provider registration, API scopes, and matching JavaScript
selection. The Node tests use the built-in test runner to cover script loading,
adapter isolation, and explicit load failures. They do not replace a live
tenant login test.
