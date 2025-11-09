using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.DAL.Entities;

public class AppUserEntity : IdentityUser<Guid>
{
    public bool Active { get; set; }
    public string Subject { get; set; }
}
