namespace CardInsight.API.Models
{
    // Models/CreditCard.cs
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(40)]
        public string Category { get; set; } = default!; // e.g., Travel, Shopping, Entertainment

        [Required, MaxLength(2000)]
        public string Features { get; set; } = default!; 

        [Url]
        public string? ImageUrl { get; set; }

        [Required, Url]
        public string ApplyLink { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
