using AutoMapper;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Entities;

namespace ContentsLimitInsurance.App.Profiles
{
    public class HighValueItemProfile : Profile
    {
        public HighValueItemProfile()
        {
            CreateMap<HighValueItemDto, HighValueItem>();
            CreateMap<HighValueItem, HighValueItemDto>();
        }
    }
}