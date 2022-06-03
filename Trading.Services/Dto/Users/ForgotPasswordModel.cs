using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Services.Dto.Users
{
    public class ForgotPasswordModel
    {
        [Required]
        [RegularExpression("^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,50})+$")]
        public string Email { set; get;}
    }
}
