using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.DAL.Entities;

public class AppUserEntity : IdentityUser<Guid>
{
    public bool Active { get; set; }
    public string Subject { get; set; }
}

public class AppRoleClaimEntity : IdentityRoleClaim<Guid>
{
}

public class AppRoleEntity : IdentityRole<Guid>
{
}

public class AppUserClaimEntity : IdentityUserClaim<Guid>
{
}

public class AppUserLoginEntity : IdentityUserLogin<Guid>
{
}

public class AppUserRoleEntity : IdentityUserRole<Guid>
{
}

public class AppUserTokenEntity : IdentityUserToken<Guid>
{
}
