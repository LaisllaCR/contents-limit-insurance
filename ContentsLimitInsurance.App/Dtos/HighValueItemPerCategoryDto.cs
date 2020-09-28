using System.Collections.Generic;

namespace ContentsLimitInsurance.App.Dtos
{
    public class HighValueItemPerCategoryDto
    {
        public List<CategoryWithItemsDto> Categories { get; set; }
    }
}
