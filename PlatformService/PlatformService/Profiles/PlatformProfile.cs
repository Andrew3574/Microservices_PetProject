using AutoMapper;
using PlatformService.Models;
using PlatformService.Models.DTOs;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            //src -> dst
            //for get
            CreateMap<PlatformReadDto,Platform>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dst => dst.Cost, opt => opt.MapFrom(src => src.Cost));

            CreateMap<PlatformCreateDto, Platform>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dst => dst.Cost, opt => opt.MapFrom(src => src.Cost));

            //for post
            CreateMap<Platform, PlatformReadDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dst => dst.Cost, opt => opt.MapFrom(src => src.Cost));

            CreateMap<Platform, PlatformCreateDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dst => dst.Cost, opt => opt.MapFrom(src => src.Cost));
        }
    }
}
