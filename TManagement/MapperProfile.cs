using AutoMapper;
using TManagement.AppServices.Account;
using TManagement.AppServices.ETasks;
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


            CreateMap<CreateOrEditETaskDto, ETask>()
                .ForMember(s => s.Attachments, d => d.Ignore())
                .ForMember(s=>s.Users, d=>d.Ignore())
                ;

            CreateMap<ETask, CreateOrEditETaskDto>()
              .ForMember(s => s.Attachments, d => d.Ignore())
              .ForMember(s => s.Users, d => d.MapFrom(x => x.Users.Where(x => x.IsActive).Select(s => s.UserId)))
              .ForMember(s => s.OldAttachments, d => d.MapFrom(x => x.Attachments.Select(a => new AttachmentInfoDto() { Id = a.AttachmentId, Name = a.Attachments.OriginalName })))
              .ForMember(s => s.OldAttachmentIds, d => d.MapFrom(x => x.Attachments.Select(a =>a.AttachmentId )));


            CreateMap<ETask, TaskViewDto>()

          .ForMember(s => s.Users, d => d.MapFrom(x => x.Users.Where(x => x.IsActive).Select(s => s.AssignedTo.FullName)))
          .ForMember(s => s.Attachments, d => d.MapFrom(x => x.Attachments.Select(a => new AttachmentInfoDto() { Id = a.AttachmentId, Name = a.Attachments.OriginalName })));
          




            CreateMap<AppUser, TaskUserDto>().ForMember(s=>s.Name,d=>d.MapFrom(x=>x.FullName)).ReverseMap();

            CreateMap<ETask, TaskListItemDto>()
                .ForMember(s => s.HasAttachments, d => d.MapFrom(x => x.Attachments.Any()))
                .ForMember(s => s.Users, d => d.MapFrom(x => x.Users.Where(s=>s.IsActive==true).Select(u => u.AssignedTo.FullName)));


        }
    }
}
