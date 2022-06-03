using System.ComponentModel.DataAnnotations;
using Trading.Services.Constants;

namespace Trading.Services.Dto.Users
{
    public class LoginModel
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(RegexValidate.Email)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}