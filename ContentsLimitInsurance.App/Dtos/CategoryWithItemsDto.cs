using ContentsLimitInsurance.App.Models;
using System.Collections.Generic;

namespace ContentsLimitInsurance.App.Dtos
{
    public class CategoryWithItemsDto
    {
        public ItemCategoryDto Category { get; set; }
        public List<HighValueItemDto> Items { get; set; }
    }
}
