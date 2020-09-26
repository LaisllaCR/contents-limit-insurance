namespace ContentsLimitInsurance.App.Dtos
{
    public class HighValueItemDto
    {
        public int HighValueItemId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int ItemCategoryId { get; set; }
        public int UserId { get; set; }

        public HighValueItemDto()
        {

        }
    }
}
