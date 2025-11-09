using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;

namespace CookBook.Web.BL.MessageHandlers;

public class CustomAuthorizationMessageHandler(
    ILogger<CustomAuthorizationMessageHandler> logger,
    IAccessTokenProvider provider,
    NavigationManager navigation)
    : AuthorizationMessageHandler(provider, navigation)
{
    private readonly IAccessTokenProvider _provider = provider;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Check if user is authenticated
        var accessTokenResult = await _provider.RequestAccessToken();

        if (accessTokenResult.TryGetToken(out var token))
        {
            logger.LogInformation("Access token: '{AccessToken}', expires: '{AcccessTokenExpires}', granted scopes: '{AccessTokenGrantedScopes}'",
                token.Value,
                token.Expires,
                token.GrantedScopes);

            // User is logged in, use base class implementation (with authorization)
            return await base.SendAsync(request, cancellationToken);
        }
        else
        {
            // User is not logged in, bypass authorization by not calling base implementation
            // Instead, we'll create a simple HTTP request without authentication
            using var httpClient = new HttpClient();

            // Copy the request to avoid disposal issues
            using var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);

            // Copy headers (except authorization-related ones)
            foreach (var header in request.Headers.Where(h => !h.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            // Copy content if present
            if (request.Content != null)
            {
                var contentBytes = await request.Content.ReadAsByteArrayAsync();
                newRequest.Content = new ByteArrayContent(contentBytes);

                // Copy content headers
                foreach (var header in request.Content.Headers)
                {
                    newRequest.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return await httpClient.SendAsync(newRequest, cancellationToken);
        }
    }
}
