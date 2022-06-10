using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trading.Authen.Services.Constants;

namespace Trading.Authen.Services.Dto.Users
{
    public class RegisterModel
    {
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(200)]
        [RegularExpression(RegexValidate.Email)]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string Position { get; set; }
        [Required]
        public byte? Status { get; set; }
        public Guid? IdAvatar { get; set; }
        [RegularExpression(RegexValidate.Phone,ErrorMessage ="Số điện thoại không đúng")]
        [MaxLength(13)]
        public string Phone { get; set; }
    }
    public class RegisterUserHasRoleGroupModel
    {
        public int IdRoleGroup { set; get; }
      
    }
}