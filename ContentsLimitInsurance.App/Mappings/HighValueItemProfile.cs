using AutoMapper;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Models;

namespace ContentsLimitInsurance.App.Mappings
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