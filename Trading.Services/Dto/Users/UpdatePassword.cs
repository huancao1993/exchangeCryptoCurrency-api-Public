using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Services.Dto.Users
{
    public class UpdatePassword
    {
        [Required]
        [MaxLength(100)]
        public string Email { set; get;}
        [Required]
        [MaxLength(100)]
        public string Password { set; get;}
        //[Required]
        //[MaxLength(100)]
        //public string NewPassword { set; get;}
    }
}
