using System.Security.Claims;

namespace CookBook.IdentityProvider.BL.Models;

public class AppUserExternalCreateModel
{
    public required string? Email { get; set; }
    public required string Provider { get; set; }
    public required string ProviderIdentityKey { get; set; }
    public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();
}