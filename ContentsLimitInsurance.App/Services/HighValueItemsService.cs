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

                return _mapper.Map<HighValueItem, HighValueItemDto>(newHighValueItem);
            }
            catch (Exception ex)
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
                return _mapper.Map<HighValueItem, HighValueItemDto>(highValueItemToRemove);
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

                return _mapper.Map(highValueItem, new HighValueItemDto());
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

        public IEnumerable<HighValueItemDto> GetAllHighValueItemsByUser(int userId)
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
            catch (Exception ex)
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
            catch
            {
                throw;
            }
        }

        public IEnumerable<CategoryPerUserDto> GetHighValueItemsPerCategories(int userId)
        {
            try
            {
                List<HighValueItem> allHighValueItemUser = _context.HighValueItem
                                                                        .Where(item => item.UserId == userId)
                                                                        .OrderBy(x => x.ItemCategoryId)
                                                                        .ToList();

                List<HighValueItemDto> allHighValueItemUserDtos = _mapper.Map<List<HighValueItem>, List<HighValueItemDto>>(allHighValueItemUser);

                var categories = allHighValueItemUserDtos.GroupBy(x => x.ItemCategoryId).ToList();

                List<CategoryPerUserDto> itemsByCategory = new List<CategoryPerUserDto>();
                foreach (var item in categories)
                {
                    var category = _context.ItemCategory.Find(item.Key);

                    CategoryPerUserDto userCategory = new CategoryPerUserDto();
                    userCategory.Name = category.Name;
                    userCategory.ItemCategoryId = category.ItemCategoryId;
                    userCategory.Items = allHighValueItemUserDtos.Where(x => x.ItemCategoryId == item.Key).ToList();

                    itemsByCategory.Add(userCategory); 
                }

                return itemsByCategory;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
