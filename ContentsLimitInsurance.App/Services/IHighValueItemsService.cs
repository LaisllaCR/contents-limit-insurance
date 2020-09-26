using ContentsLimitInsurance.App.Dtos;
using System.Collections.Generic;

namespace ContentsLimitInsurance.App.Services
{
    public interface IHighValueItemsService
    {
        IEnumerable<HighValueItemDto> GetAllHighValueItemsByUser(int userId);
        HighValueItemDto GetHighValueItemDto(int id);
        HighValueItemDto AddHighValueItem(HighValueItemDto dto);
        void DeleteHighValueItem(int id);
        bool HighValueItemExists(int id);
    }
}
