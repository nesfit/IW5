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

## Entra app registrations

Create three single-tenant registrations.

### CookBook API

1. Expose `api://<api-client-id>/access_as_user`.
2. Add an app role with value `admin`, allowed for users/groups.
3. Assign users or groups through the API Enterprise Application.
4. Put the tenant-specific v2 issuer and API client ID into the API's Entra `TrustedIssuersOptions` entry.

### CookBook Web

1. Configure it as a public SPA client.
2. Add `https://localhost:44355/authentication/login-callback` as a SPA redirect URI.
3. Add `https://localhost:44355/` as the post-logout URI.
4. Grant delegated permission to the CookBook API `access_as_user` scope.
5. Put its client ID, tenant authority, and API scope into the Web `Entra` provider registration.

### CookBook IdentityProvider

1. Configure it as a confidential web client.
2. Add the configured `/signin-entra` callback and `/signout-callback-entra` signed-out callback.
3. Add an app role with value `admin`; this role is emitted in the ID token used for federated login.
4. Store the client secret outside source control:

   ```powershell
   dotnet user-secrets --project src\CookBook\CookBook.IdentityProvider.App set "CookBookIdentityProviderOptions:ExternalProviders:0:ClientSecret" "<secret>"
   ```

5. Set the Entra external provider's `IsEnabled` to `true`.

## External account linking

The IdentityProvider first looks up an existing external login by immutable provider plus subject. For Entra the key is tenant ID plus object ID (`tid:oid`). If no link exists, it links to a local account only when:

- the provider positively emits `email_verified=true`;
- exactly one local account has that normalized email.

Username matches and unverified email claims are never used for linking. If there is no match, a passwordless shadow account is provisioned. An existing matched account keeps its password, subject, profile, and local roles, so either login path reaches the same account.

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
- **403 for admin action:** assign `admin` on the app registration that emits the relevant token (API registration for direct mode, IdentityProvider registration for federated mode).
- **Immediate login after logout:** upstream logout is disabled or the external provider retained its session.
- **No external login button:** the provider registration is disabled or local-only mode is configured.
- **Account not linked:** the email claim is absent/unverified, or more than one normalized local email matches.

Never place client secrets in checked-in `appsettings.json`.
