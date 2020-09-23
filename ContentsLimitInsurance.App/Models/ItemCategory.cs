using System;
using System.Collections.Generic;

namespace ContentsLimitInsurance.App.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            HighValueItem = new HashSet<HighValueItem>();
        }

        public int ItemCategoryId { get; set; }
        public string Name { get; set; }
        public string ItemCategoryKey { get; set; }

        public virtual ICollection<HighValueItem> HighValueItem { get; set; }
    }
}
