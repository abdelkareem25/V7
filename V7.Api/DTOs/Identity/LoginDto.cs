using System.ComponentModel.DataAnnotations;

namespace V7.Api.DTOs.Identity
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
