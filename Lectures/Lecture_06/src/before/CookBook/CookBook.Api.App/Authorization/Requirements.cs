using Microsoft.AspNetCore.Authorization;

namespace CookBook.Api.App.Authorization;

// The resource owner or an admin may perform the operation.
public sealed class OwnerOrAdminRequirement : IAuthorizationRequirement
{
    public static readonly OwnerOrAdminRequirement Instance = new();
}

// Only an admin may perform the operation, regardless of ownership.
public sealed class AdminOnlyRequirement : IAuthorizationRequirement
{
    public static readonly AdminOnlyRequirement Instance = new();
}
