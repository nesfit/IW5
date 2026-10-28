# Entra ID Authentication Support Plan

## Problem and approach

CookBook currently has a binary `IdentityOptions:IsEnabled` switch. When enabled, the Blazor WebAssembly client authenticates through `CookBook.IdentityProvider.App`, and the API accepts bearer tokens from its configured authority. The CookBook IdentityProvider supports local ASP.NET Identity users, while its checked-in external-login pages are incomplete sample code backed by a disabled `TestUserStore`.

Keep the API's binary authorization switch, introduce an explicit provider selection in the Web client, and make the CookBook IdentityProvider's local and external login methods independently configurable. The external-provider model is provider-neutral; Entra ID is the first implementation. This produces four clear teaching stages:

| Stage | API configuration | Web configuration | Login/token issuer | CookBook IdentityProvider configuration |
|---|---|---|---|---|
| 1. No identity | `IsEnabled: false` | `IsEnabled: false` | None | Not required |
| 2. Direct Entra | `IsEnabled: true`; trusted issuers remain registered | `IsEnabled: true`, provider `Entra` | Single-tenant Entra ID | Not required |
| 3. Local identity | `IsEnabled: true`; trusted issuers remain registered | `IsEnabled: true`, provider `CookBookIdentityProvider` | CookBook IdentityProvider | Local enabled, external providers disabled |
| 4. Combined identity | `IsEnabled: true`; trusted issuers remain registered | `IsEnabled: true`, provider `CookBookIdentityProvider` | CookBook IdentityProvider | Local enabled, Entra external provider enabled |

The independent CookBook IdentityProvider switches also support an external provider as its only login path (`Local:IsEnabled=false` with one or more external providers enabled). Direct Entra authorization uses Entra app roles. In persistent-user mode, a federated Entra identity is linked to the single existing local account with the same verified email; otherwise a local shadow user is auto-provisioned. Federated users receive the de-duplicated union of external roles and local CookBook IdentityProvider roles.

## Design decisions

- Support one Entra workforce tenant only. Do not use `common`, `organizations`, or personal Microsoft accounts.
- Use three Entra app registrations:
  - **CookBook API**: exposes a delegated API scope and the `Admin` app role used in direct Entra access tokens.
  - **CookBook Web**: public SPA client using authorization code with PKCE and no client secret.
  - **CookBook IdentityProvider**: confidential web client using an environment/user-secret client secret and an `Admin` app role used in Entra ID tokens during federated login.
- Make external login provider registration extensible. Use stable provider keys and provider-specific adapters/options; implement `Entra` now without assuming all future providers use Entra claims or token formats.
- In the Web client, select an enabled authentication provider explicitly. Use `CookBookIdentityProvider` for the local/federated CookBook service and `Entra` for direct Entra. The configuration shape supports adding providers such as Microsoft, Google, or Facebook later.
- Keep `IdentityOptions:IsEnabled` in the API solely as the endpoint authorization switch. Register all configured trusted bearer-token issuers regardless of that value so toggling demonstration authorization does not rebuild authentication configuration.
- Validate issuer, signature, lifetime, and audience for every trusted API issuer. Remove the current `ValidateAudience = false`. Multiple issuer support means multiple validated JWT bearer schemes, not acceptance of arbitrary tokens.
- First resolve an external account by immutable `(provider, provider subject)` linkage. If no link exists, automatic local-account linking is allowed only when the provider supplies a verified email and exactly one local account has the same normalized email. Never link by username or an unverified email.
- Keep external roles external rather than copying them into persistent ASP.NET Identity role assignments. Merge them with local roles when the CookBook IdentityProvider issues tokens.
- Keep Duende `TestUserStore` as a teaching path. Make the test-user and persistent ASP.NET Identity implementations swappable through one small, adjacent registration block with one option commented and the other uncommented.
- Make upstream external-provider logout independently configurable. Local CookBook logout always occurs; upstream logout is challenged only when enabled.
- Never log access tokens, authorization codes, client secrets, or external claim dumps containing PII.
- Never fall back to an unvalidated token issuer, anonymous authorization, or another login provider when enabled authentication configuration is invalid or an external provider is unavailable.

## Implementation todos

### 1. Introduce provider-neutral, validated authentication configuration

**Files/components**

- `CookBook.Common/Options/IdentityOptions.cs`
- New option types under `CookBook.Common/Options/`
- API, Web, and CookBook IdentityProvider `appsettings*.json`
- Configuration validation helpers/tests

**Changes**

- Retain `IdentityOptions.IsEnabled` as the authorization/UI switch.
- Add a provider-neutral authentication-provider identifier/configuration model. The initial provider keys are:
  - `CookBookIdentityProvider`
  - `Entra`
- Web options contain a collection of provider registrations plus the selected provider. Each registration owns its authority, client ID, callback paths, API scope, and provider type. This allows future adapters for Microsoft, Google, and Facebook without changing the core Web authentication selection contract.
- API options contain a collection of trusted token issuers. Each entry has a unique scheme name, authority/metadata address, valid issuers, valid audiences, name claim type, role claim type, and enabled validation handler. These validators are registered even when `IdentityOptions.IsEnabled` is false.
- Add CookBook IdentityProvider login options with:
  - `Local.IsEnabled`.
  - `ExternalProviders`, a collection of provider-neutral registrations.
  - Entra-specific settings inside the Entra registration: tenant ID, confidential client ID, client secret configuration key, callback path, signed-out callback path, display name, claim mappings, and `UpstreamLogout.IsEnabled`.
- Name all settings and enum values `CookBookIdentityProvider`, not the ambiguous `IdentityProvider`.
- Use options validation at startup. Errors must identify missing or contradictory keys.
- Reject:
  - Unknown selected Web provider keys or duplicate provider/scheme names.
  - Enabled Web authentication without a selected, enabled provider or its authority, client ID, and API scope.
  - API trusted issuer entries without authority/metadata, issuer, audience, or claim mappings.
  - Entra registrations with non-GUID/sentinel tenant or client values.
  - CookBook IdentityProvider startup when local login and all external providers are disabled.
  - Enabled external providers without their adapter-specific required settings.
- Preserve environment-variable and user-secrets overrides; checked-in files contain placeholders only.

### 2. Support multiple trusted API token issuers while retaining `IsEnabled`

**Files/components**

- `CookBook.Api.App/Program.cs`
- `CookBook.Api.App/Endpoints/IngredientEndpoints.cs`
- `CookBook.Api.App/Endpoints/RecipeEndpoints.cs`
- API authorization constants/helpers
- `CookBook.Api.App/appsettings*.json`
- `Directory.Packages.props` and API project references if `Microsoft.Identity.Web` is adopted

**Changes**

- Keep `IdentityOptions.IsEnabled` as the only switch controlling whether protected endpoint groups call `RequireAuthorization`.
- Always register one JWT bearer scheme per trusted issuer:
  - CookBook IdentityProvider with its exact issuer and API audience.
  - Single-tenant Entra with its tenant-specific issuer and API audience. Prefer Microsoft-supported `Microsoft.Identity.Web` configuration for this scheme where it composes cleanly with multiple schemes.
- Add a policy scheme that reads only enough of the unvalidated JWT to select a configured handler by `iss`; the selected JWT handler must still fully validate issuer, signing key, lifetime, and audience. Unknown or ambiguous issuers fail authentication.
- Keep the trusted issuer collection extensible. A future provider may be added only if it can issue an API access token that the API can independently validate. Social login tokens that target Google/Facebook rather than CookBook API are not accepted directly; those providers require the CookBook IdentityProvider or a future token-exchange/backend adapter.
- Normalize each scheme's configured subject/name and role claims to the claims consumed by `EndpointsBase` and existing `RequireRole(UserRoles.Admin)` policies. Entra uses `oid`/`sub` and `roles`; CookBook IdentityProvider uses its local subject and standard role claims.
- Continue using `IsEnabled` checks in endpoint mapping. Turning authorization off leaves validators registered but allows intended endpoints to execute anonymously.
- Ensure authenticated modifying endpoints return `401` for missing/invalid tokens and `403` for failed role policies; do not silently execute anonymously.
- Retain open read-only endpoints where currently intended.
- Remove access-token audience bypass.

### 3. Make Web authentication provider-selectable and extensible

**Files/components**

- `CookBook.Web.App/Program.cs`
- `CookBook.Web.BL/Installers/WebBLInstaller.cs`
- `CookBook.Web.BL/MessageHandlers/CustomAuthorizationMessageHandler.cs`
- `CookBook.Web.App/Pages/Authentication/Authentication.razor`
- Navigation/login display components using `AuthorizeView` or login links
- `CookBook.Web.App/wwwroot/appsettings*.json`
- Web project/package references

**Changes**

- Register remote authentication only when `IdentityOptions.IsEnabled` is true.
- Resolve the selected registration from the provider collection and dispatch to an authentication adapter:
  - Direct Entra: tenant-specific v2 authority, Web SPA client ID, authorization code response type, PKCE, exact login/logout callbacks, and the API delegated scope.
  - `CookBookIdentityProvider`: CookBook authority, `cookbookclient`, authorization code, PKCE, callbacks, and `cookbookapi` scope.
- Define the adapter boundary so future Microsoft/Google/Facebook implementations can supply their own packages, options binding, callback behavior, and API-token strategy. Only Entra and `CookBookIdentityProvider` adapters are implemented in this change.
- Register the API authorization message handler only when identity is enabled and request the selected provider's API scope.
- Remove token-value logging from `CustomAuthorizationMessageHandler`; retain only safe diagnostics such as token availability/result status.
- Preserve anonymous requests to intentionally public API endpoints without constructing a second unmanaged `HttpClient`. Clone/retry behavior must use the configured pipeline or attach a token only when available.
- Hide or disable login/logout and authorization-specific UI in `Disabled` mode. Keep the existing remote authenticator routes in authenticated modes.
- Validate Web settings before host startup and show configuration errors rather than broken redirect loops.

### 4. Add provider-neutral external login registration to CookBook IdentityProvider

**Files/components**

- `CookBook.IdentityProvider.App/Program.cs`
- `CookBook.IdentityProvider.App/HostingExtensions.cs`
- `CookBook.IdentityProvider.App/Installers/IdentityProviderAppInstaller.cs`
- `CookBook.IdentityProvider.App/Pages/Account/Login/Index.cshtml.cs`
- `CookBook.IdentityProvider.App/Pages/Account/Login/Index.cshtml`
- CookBook IdentityProvider options/configuration files
- CookBook IdentityProvider package references if required

**Changes**

- Add an external-provider registration abstraction that maps each configured provider type to its ASP.NET Core authentication handler. Implement the Entra OpenID Connect adapter now; leave documented extension points for Microsoft, Google, Facebook, and other handlers.
- Register the Entra OpenID Connect authentication scheme only when its external-provider registration is enabled:
  - Tenant-specific authority and confidential client credentials.
  - Authorization code flow.
  - `IdentityServerConstants.ExternalCookieAuthenticationScheme` as the temporary sign-in scheme.
  - ASP.NET Identity application scheme for sign-out integration.
  - Save the upstream ID token/session context needed for optional federated logout.
  - Request only necessary identity scopes and role-bearing ID-token claims.
  - Use IdentityServer's OIDC state formatter/cache if needed to keep state out of long callback URLs.
- Make `BuildModelAsync` honor `Local.IsEnabled` and list all enabled external schemes by display name.
- Keep existing behavior that immediately challenges the sole external provider when local login is disabled.
- In combined mode, show both the local form and a clearly named “Microsoft Entra ID” option.
- Ensure provider-neutral challenge/callback code uses the scheme and configured claim mapping rather than Entra-specific conditionals.
- Handle access denied and remote authentication errors through a safe login error surface; log useful correlation context without tokens or PII.

### 5. Keep TestUserStore and add a swappable persistent external-user store

**Files/components**

- `CookBook.IdentityProvider.App/Pages/ExternalLogin/Challenge.cshtml.cs`
- `CookBook.IdentityProvider.App/Pages/ExternalLogin/Callback.cshtml.cs`
- New external-user lookup/link/provision abstraction in CookBook IdentityProvider BL/App
- `IAppUserFacade` / `AppUserFacade` or direct `UserManager<AppUserEntity>` integration
- ASP.NET Identity login tables already provided by the Identity schema

**Changes**

- Keep `TestUsers.Users`, Duende `TestUserStore`, and `AddTestUsers(TestUsers.Users)` for the early teaching stage.
- Introduce one application-facing external-user service contract used by the callback, with:
  - A `TestUserStore` adapter that retains the current in-memory lookup/auto-provision demonstration.
  - An ASP.NET Identity adapter that persists external login links and supports safe local-account linking.
- Put the two registrations in one clearly labeled, adjacent block with exactly one active implementation. Switching from test users to persistent users must require commenting one registration and uncommenting the other, with no callback rewrite.
- Keep profile-service registration aligned with the selected store in the same teaching switch block so test-user claims and persistent-user claims are both emitted correctly.
- Validate the callback's authentication result, stored scheme, and return URL before mutation.
- Resolve the immutable external key through the provider's claim mapping. For Entra, use tenant ID plus `oid`; use protocol `sub` only as a documented fallback. The persistent adapter stores it through `UserManager.AddLoginAsync`/`UserLoginInfo`.
- For a returning login, find the user by external login and verify it is active.
- For an unlinked first login in persistent mode:
  - Read the provider's email and email-verification claims through its adapter.
  - Only if the provider positively asserts that the email is verified, normalize it through ASP.NET Identity's configured normalizer and query for local matches.
  - Link to an existing local account only when exactly one account has that normalized email. The database/query must not silently choose among duplicate emails.
  - Do not link by username. If email is absent, unverified, or ambiguous, continue to shadow-user provisioning (or fail with a clear manual-link-required result when a uniqueness collision prevents provisioning).
  - Create an active local shadow `AppUserEntity` with a new stable local subject.
  - Use the verified email as contact data when available, but never as the immutable external identity.
  - Derive a collision-safe user name/display name without treating either as identity.
  - Add the external login association.
  - Clean up the newly created user if association fails.
  - Fail closed on duplicate external keys or local uniqueness collisions that cannot be resolved deterministically.
- For a matched existing local account, add only the external login association; preserve its password, local roles, subject, and profile so the same account can use local username/password or the external provider.
- Capture only required external claims for the current session, including validated known app-role values, upstream identity provider, session ID, provider subject, and tokens needed when upstream logout is enabled.
- Issue the local IdentityServer cookie using the local subject, then delete the temporary external cookie and resume the validated authorization request.
- Raise success/failure events consistently with local login.

### 6. Merge local and external roles when CookBook IdentityProvider issues tokens

**Files/components**

- CookBook IdentityProvider profile service registered in `HostingExtensions.cs`
- CookBook IdentityProvider BL user/claims services
- `CookBook.IdentityProvider.App/Config.cs`
- Shared role constants

**Changes**

- Extend the profile service to load:
  - Persistent local ASP.NET Identity roles for the shadow/local user.
  - Session-scoped, provider-adapter-normalized external role claims for federated users.
- Allow only CookBook-known role values such as `UserRoles.Admin`; ignore and safely log unknown values.
- Merge with ordinal semantics, remove duplicates, and emit standard JWT `role` claims in the CookBook IdentityProvider access token.
- Continue emitting stable local subject and required profile claims.
- Ensure local-only users are unchanged and external users do not receive stale external roles from persistent local storage.
- Preserve equivalent role emission in TestUserStore mode through the test-user profile service.
- Confirm `cookbookapi` scope/API resource includes role claims and has an explicit audience matching API validation.

### 7. Make upstream logout optional

**Files/components**

- `CookBook.IdentityProvider.App/Pages/Account/Logout/Index.cshtml.cs`
- External callback token preservation
- Web OIDC logout configuration

**Changes**

- Always clear the local ASP.NET Identity/IdentityServer session first.
- Read the external provider's `UpstreamLogout.IsEnabled` option.
- When disabled, stop after clearing the local CookBook session. This intentionally leaves the user signed in at the upstream provider so the next challenge may complete without prompting.
- When enabled, challenge the upstream provider's sign-out endpoint only when the scheme supports sign-out and valid upstream context is available.
- Return through the registered signed-out callback to complete the CookBook IdentityProvider logout.
- If upstream logout is unavailable/fails, keep the local session terminated and present a safe logged-out/error result rather than restoring authentication.
- Apply an equivalent provider option to direct Web authentication: local SPA authentication state is always cleared, while upstream logout is invoked only when enabled and supported by that adapter.

### 8. Add automated authentication and configuration coverage

**Projects/components**

- Extend `CookBook.Api.App.EndToEndTests`
- Add focused CookBook IdentityProvider integration/unit test project(s)
- Add Web auth configuration/component test project only where behavior cannot be covered through extracted pure option builders
- Solution and central package configuration

**API cases**

- `IdentityOptions.IsEnabled=false` permits intended modifying operations without a bearer token.
- Validators for all configured issuers are registered whether `IsEnabled` is true or false.
- Entra tokens with correct tenant issuer/audience/scope route to the Entra scheme; CookBook tokens route to the `CookBookIdentityProvider` scheme.
- Wrong/unknown issuer, audience, expiry, or signature is rejected; an unvalidated `iss` value cannot bypass scheme validation.
- Multiple trusted issuers can authenticate concurrently when endpoint authorization is enabled.
- `Admin` app role satisfies admin policies; missing role yields `403`.
- Missing/malformed token yields `401`.

**CookBook IdentityProvider cases**

- Local-only mode shows local login and no external provider.
- External-only mode with one provider immediately redirects to that provider's challenge.
- Combined mode shows both paths.
- TestUserStore mode supports external login and auto-provisioning without a database.
- Persistent mode links a verified-email external identity to the single matching existing local account while preserving local password login.
- Unverified email and username-only matches never link.
- Ambiguous duplicate-email matches fail closed rather than selecting an account.
- A first callback without a matching verified email auto-provisions and links one shadow user.
- Repeated callback reuses the same user.
- Association failure cleans up a newly created shadow user.
- Duplicate provider key fails closed.
- Known external and local roles merge and de-duplicate; unknown roles are not issued.
- Cancellation/error does not provision a user.
- Local logout always clears the CookBook session.
- Disabled upstream logout leaves the external session intact; enabled upstream logout challenges the provider and returns correctly.

**Web/configuration cases**

- Each configured provider selects the expected adapter, authority, client ID, callback URLs, and API scope.
- `CookBookIdentityProvider` naming is used consistently.
- `IdentityOptions.IsEnabled=false` omits OIDC/token handlers and login controls.
- Enabled providers attach a token only to configured API URLs.
- Selecting an unknown/disabled provider fails startup.
- Invalid mode-specific configuration fails startup with named keys.

Use test authentication schemes and locally signed JWTs. Do not call live Entra endpoints in CI.

### 9. Add teaching-focused Entra setup and migration documentation

**Files/components**

- New authentication/Entra setup document under `src/docs/`
- Root or CookBook README links
- Redacted `appsettings` templates/examples for API, Web, and CookBook IdentityProvider

**Content**

- Portal walkthrough for all three single-tenant app registrations.
- Exact redirect and post-logout URI table for local development and placeholders for deployment.
- API “Expose an API” delegated scope setup and SPA delegated permission/consent steps.
- `Admin` app-role definitions:
  - On API registration for direct-mode access tokens.
  - On CookBook IdentityProvider registration for federated-mode ID tokens.
- Enterprise Application user/group role assignment steps for both registrations.
- User-secrets commands for the CookBook IdentityProvider confidential client secret.
- Four-stage configuration matrix with startup order and expected login UI/token issuer.
- Provider-extension guide describing the Web adapter, CookBook IdentityProvider external-handler adapter, claim mapping, and API-token requirements for adding Microsoft, Google, or Facebook.
- Explicit warning that direct social login does not automatically produce a CookBook API token; future providers must provide a verifiable API token strategy or route through CookBook IdentityProvider.
- One-block comment/uncomment instructions for switching between `TestUserStore` and persistent ASP.NET Identity.
- Verified-email linking rules and examples showing one account usable through local password and external login.
- Upstream logout enabled/disabled demonstrations and expected single-sign-on behavior.
- Expected claims (`iss`, `aud`, `tid`, `oid`/`sub`, `scp`, `roles`) and where each is consumed.
- Troubleshooting for consent, wrong tenant/issuer, wrong audience, missing roles, callback mismatch, CORS, expired secrets, and logout redirect mismatch.
- Migration:
  - Existing API `IsEnabled` remains the endpoint authorization switch; authority moves into `TrustedIssuers`.
  - Existing Web `IsEnabled=false` remains disabled.
  - Existing Web `IsEnabled=true` selects `CookBookIdentityProvider`.
- State explicitly that bad Entra configuration never falls back to anonymous access.

## Validation

1. Restore and build the complete `src/CookBook/CookBook.sln` using the repository's current .NET 10 SDK target.
2. Run focused new authentication/configuration tests.
3. Run the complete solution test suite.
4. Run all four application scenarios locally:
   - Identity disabled while trusted API validators remain registered.
   - Direct Entra with valid user and `Admin` app-role assignments.
   - Local CookBook IdentityProvider login.
   - Combined local + Entra CookBook IdentityProvider login.
5. In each authenticated scenario, verify read operations, protected modifications, admin-only behavior, token issuer/audience/role claims, login cancellation, and logout.
6. Verify external-only CookBook IdentityProvider mode separately.
7. Repeat federated logout with upstream logout disabled and enabled.
8. Exercise both the TestUserStore and persistent-user registration blocks.
9. Keep live Entra smoke testing manual and driven by developer-owned secrets; CI remains deterministic and offline.

## Important considerations

- The repository now targets .NET 10 despite older repository guidance mentioning .NET 9; implementation and validation must follow `Directory.Packages.props` and current project targets.
- The current Web message handler logs raw access tokens. Removing that disclosure is part of this change, not an unrelated cleanup.
- Current API audience validation is disabled. Enabling exact audience validation is required for safe direct Entra support and may expose stale deployment configuration that must be migrated.
- App-role assignments are specific to the resource registration that emits the token. The duplicated `Admin` role setup in API and CookBook IdentityProvider registrations is intentional and must be prominent in teaching docs.
- API `IsEnabled=false` only removes endpoint authorization requirements; it must not weaken or broaden any registered token validator.
- Supporting multiple trusted issuers requires explicit per-issuer validation and claim normalization. Merely listing issuer strings in one permissive validator is not sufficient.
- Provider-neutral configuration does not imply protocol equivalence. Entra and CookBook IdentityProvider can issue API access tokens; future social providers need an explicit API-token design.
- Automatic account linking by verified email is intentionally narrower than username/email matching. It requires positive verification and exactly one normalized local email match to limit account-takeover risk.
- Auto-provisioned shadow users need no password. A shadow user becomes locally sign-in-capable only through a separate explicit password/account-recovery flow; a pre-existing matched local account retains its existing password login.
