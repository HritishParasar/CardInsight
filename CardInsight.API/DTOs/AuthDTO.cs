using System.ComponentModel.DataAnnotations;

namespace CardInsight.API.DTOs
{
    public class AuthDTO
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
