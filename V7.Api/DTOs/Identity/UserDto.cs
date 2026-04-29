using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Identity
{
    public class UserDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }

    }
}
