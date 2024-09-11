using AutoMapper;
using TManagement.AppServices.Loockups;

namespace TManagement
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Entities.Lookup, LoockupDto>();
        }
    }
}
