using AutoMapper;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Models;
using ContentsLimitInsurance.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentsLimitInsurance.App.Repositories
{
    public class HighValueItemsService : IHighValueItemsService
    {
        private readonly IMapper _mapper;
        private readonly dbContext _context;

        public HighValueItemsService(dbContext context,
            IMapper mapper)
        {

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public HighValueItemDto AddHighValueItem(HighValueItemDto dto)
        {
            try
            {

                HighValueItem newHighValueItem = _mapper.Map<HighValueItemDto, HighValueItem>(dto);

                _context.HighValueItem.Add(newHighValueItem);
                _context.SaveChanges();

                return GetHighValueItemDto(newHighValueItem);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HighValueItemDto DeleteHighValueItem(int id)
        {
            try
            {
                HighValueItem highValueItemToRemove = GetHighValueItem(id);
                _context.HighValueItem.Remove(highValueItemToRemove);
                _context.SaveChanges();
                
                return GetHighValueItemDto(highValueItemToRemove);

            }
            catch (Exception)
            { 
                throw;
            }
        }

        private HighValueItemDto GetHighValueItemDto(HighValueItem highValueItem)
        {
            try
            {
                HighValueItemDto highValueItemDto = _mapper.Map(highValueItem, new HighValueItemDto());
                highValueItemDto.Category = GetItemCategoryDto(highValueItemDto.ItemCategoryId);
                return highValueItemDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HighValueItemDto GetHighValueItemDto(int id)
        {
            try
            {
                HighValueItem highValueItem = GetHighValueItem(id);
                HighValueItemDto highValueItemDto = _mapper.Map(highValueItem, new HighValueItemDto());
                highValueItemDto.Category = GetItemCategoryDto(highValueItemDto.ItemCategoryId);
                return highValueItemDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private HighValueItem GetHighValueItem(int id)
        {
            try
            {
                HighValueItem highValueItem = _context.HighValueItem.Find(id);

                return highValueItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<HighValueItemDto> GetHighValueItemsByUser(int userId)
        {
            try
            {
                List<HighValueItem> allHighValueItemUser = _context.HighValueItem
                                                                        .Where(item => item.UserId == userId)
                                                                        .OrderBy(x => x.ItemCategoryId)
                                                                        .ToList();
                List<HighValueItemDto> allHighValueItemUserDtos = _mapper.Map<List<HighValueItem>, List<HighValueItemDto>>(allHighValueItemUser);

                return allHighValueItemUserDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool HighValueItemExists(int id)
        {
            try
            {
                HighValueItem highValueItem = GetHighValueItem(id);

                return (highValueItem == null) ? false : true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CategoryWithItemsDto> GetHighValueItemsPerCategoriesByUser(int userId)
        {
            try
            {
                List<HighValueItem> allHighValueItemUser = _context.HighValueItem
                                                                        .Where(item => item.UserId == userId)
                                                                        .OrderBy(x => x.Name)
                                                                        .ToList();

                List<HighValueItemDto> allHighValueItemUserDtos = _mapper.Map<List<HighValueItem>, List<HighValueItemDto>>(allHighValueItemUser);

                var categories = allHighValueItemUserDtos.GroupBy(x => x.ItemCategoryId).ToList();

                List<CategoryWithItemsDto> itemsByCategory = new List<CategoryWithItemsDto>();
                foreach (var item in categories)
                {
                    CategoryWithItemsDto userCategory = new CategoryWithItemsDto()
                    {
                        Category = GetItemCategoryDto(item.Key),
                        Items = allHighValueItemUserDtos.Where(x => x.ItemCategoryId == item.Key).ToList()
                    };

                    itemsByCategory.Add(userCategory); 
                }

                return itemsByCategory.OrderBy(x => x.Category.Name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ItemCategoryDto GetItemCategoryDto(int id)
        {
            try
            {
                ItemCategory itemCategory = GetItemCategory(id);

                return _mapper.Map(itemCategory, new ItemCategoryDto());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ItemCategory GetItemCategory(int id)
        {
            try
            {
                ItemCategory itemCategory = _context.ItemCategory.Find(id);

                return itemCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
