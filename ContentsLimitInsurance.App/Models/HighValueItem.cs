namespace ContentsLimitInsurance.App.Models
{
    public partial class HighValueItem
    {
        public int HighValueItemId { get; set; }
        public double Value { get; set; }
        public int ItemCategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string HighValueItemKey { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
    }
}
