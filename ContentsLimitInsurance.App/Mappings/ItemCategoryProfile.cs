using AutoMapper;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Models;

namespace ContentsLimitInsurance.App.Mappings
{
    public class ItemCategoryProfile : Profile
    {
        public ItemCategoryProfile()
        {
            CreateMap<ItemCategoryDto, ItemCategory>();
            CreateMap<ItemCategory, ItemCategoryDto>();
        }
    }
}
