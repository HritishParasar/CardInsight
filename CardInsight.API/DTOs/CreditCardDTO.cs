using System.ComponentModel.DataAnnotations;

namespace CardInsight.API.DTOs
{
    public class CreditCardDTO
    {
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
    }
    public record CreditCardVm(
    int Id, string Name, string Category, string Features, string? ImageUrl, string ApplyLink, DateTime CreatedAt
    );
}
