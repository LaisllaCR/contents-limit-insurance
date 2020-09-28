using ContentsLimitInsurance.App.Dtos;
using System.Collections.Generic;

namespace ContentsLimitInsurance.App.Services
{
    public interface IHighValueItemsService
    {
        IEnumerable<HighValueItemDto> GetAllHighValueItemsByUser(int userId);
        HighValueItemDto GetHighValueItemDto(int id);
        IEnumerable<CategoryWithItemsDto> GetHighValueItemsPerCategories(int id);
        HighValueItemDto AddHighValueItem(HighValueItemDto dto);
        HighValueItemDto DeleteHighValueItem(int id);
        bool HighValueItemExists(int id);
    }
}
