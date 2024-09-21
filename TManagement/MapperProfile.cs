using AutoMapper;
using TManagement.AppServices.Account;
using TManagement.AppServices.Loockups;
using TManagement.Entities;

namespace TManagement
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Entities.Lookup, LoockupDto>();
            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserListItemDto>()
                .ForMember(d => d.Country, s => s.MapFrom(p => p.City!.FatherLookup!.Name ?? "-"))
                .ForMember(d => d.City, s => s.MapFrom(p => p.City!.Name ?? "-"))
                .ForMember(d => d.EducationLevel, s => s.MapFrom(p => p.EducationLevel!.Name ?? "-"));

        }
    }
}
