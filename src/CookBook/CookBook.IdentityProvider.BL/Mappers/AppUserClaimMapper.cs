using System.Security.Claims;
using CookBook.IdentityProvider.BL.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.IdentityProvider.BL.Mappers;

[Mapper]
public partial class AppUserClaimMapper
{
    [MapperIgnoreSource(nameof(Claim.Issuer))]
    public partial AppUserClaimListModel ToListModel(Claim claim);
}
