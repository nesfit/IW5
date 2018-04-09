using AutoMapper;
using Contacts.DAL.Entities;
using Contacts.ViewModels.Models;

namespace Contacts.MapperConfig
{
    public class MapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(Initialize);
        }

        private static void Initialize(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<ContactModel, ContactEntity>();
            mapper.CreateMap<ContactEntity, ContactModel>();

            mapper.CreateMap<ContactEntity, ContactListModel>().ForMember(model => model.Name, conf => conf.MapFrom(entity => entity.Firstname + " " + entity.Lastname));
            mapper.CreateMap<ContactModel, ContactListModel>().ForMember(model => model.Name, conf => conf.MapFrom(entity => entity.Firstname + " " + entity.Lastname));
        }
    }
}