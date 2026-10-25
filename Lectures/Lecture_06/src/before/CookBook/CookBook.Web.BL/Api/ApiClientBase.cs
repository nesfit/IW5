namespace CookBook.Web.BL.Api;

public class ApiClientBase
{
    protected virtual Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, object urlBuilder, CancellationToken cancellationToken)
        => Task.CompletedTask;

    protected virtual Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        => Task.CompletedTask;
}
