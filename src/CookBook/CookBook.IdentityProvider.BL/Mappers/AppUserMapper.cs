using CookBook.Common.Models.User;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.IdentityProvider.BL.Mappers;

[Mapper]
public partial class AppUserMapper
{
    public partial AppUserEntity ToEntity(AppUserCreateModel appUserCreateModel);

    public partial AppUserListModel ToListModel(AppUserEntity appUserEntity);
    public partial List<AppUserListModel> ToListModels(IEnumerable<AppUserEntity> appUserEntities);

    public partial AppUserDetailModel ToDetailModel(AppUserEntity appUserEntity);
}
