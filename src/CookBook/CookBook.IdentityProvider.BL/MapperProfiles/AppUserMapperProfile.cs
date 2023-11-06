using AutoMapper;
using CookBook.IdentityProvider.BL.Models;
using CookBook.IdentityProvider.DAL.Entities;

namespace CookBook.IdentityProvider.BL.MapperProfiles;

public class AppUserMapperProfile : Profile
{
    public AppUserMapperProfile()
    {
        CreateMap<AppUserCreateModel, AppUserEntity>();

        CreateMap<AppUserEntity, AppUserDetailModel>();
    }
}
