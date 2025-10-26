using System.Security.Claims;
using CookBook.IdentityProvider.BL.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.IdentityProvider.BL.Mappers;

[Mapper]
public partial class AppUserClaimMapper
{
    [MapperIgnoreSource(nameof(Claim.Issuer))]
    [MapProperty(nameof(Claim.Type), nameof(AppUserClaimListModel.ClaimType))]
    [MapProperty(nameof(Claim.Value), nameof(AppUserClaimListModel.ClaimValue))]
    public partial AppUserClaimListModel ToListModel(Claim claim);
}
