namespace CardInsight.API.DTOs
{
    public class CreditCardDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Features { get; set; }
        public string ImageUrl { get; set; }
        public string ApplyLink { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
