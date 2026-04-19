namespace CookBook.Common.Options;

public class IdentityOptions
{
    public bool IsEnabled { get; set; }
    public required string Authority { get; set; }
    public IList<string> DefaultScopes { get; set; } = [];
}
