using System.Collections.Generic;

namespace ContentsLimitInsurance.App.Dtos
{
    public class CategoryPerUserDto
    {
        public string Name { get; set; }
        public int ItemCategoryId { get; set; }
        public List<HighValueItemDto> Items { get; set; }
    }
}
