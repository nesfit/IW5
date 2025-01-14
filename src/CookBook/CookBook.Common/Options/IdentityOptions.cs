namespace CookBook.Common.Options;

public class IdentityOptions
{
    public bool IsEnabled { get; set; }
    public required string IdentityServerUrl { get; set; }
}
