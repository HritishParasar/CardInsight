using System.ComponentModel.DataAnnotations;

namespace CardInsight.API.Models
{

    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [Required, MaxLength(20)]
        public string Role { get; set; } = "User"; // "Admin" or "User"
    }


}
