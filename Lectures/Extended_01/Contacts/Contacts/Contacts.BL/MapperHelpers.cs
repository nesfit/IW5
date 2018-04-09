namespace Contacts.BL
{
    public static class MapperHelpers
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
    }
}