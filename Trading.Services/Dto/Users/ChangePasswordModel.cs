using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Services.Dto.Users
{
    public class ChangePasswordModel
    {
        public string OldPassWord { set; get; }
        public string NewPassWord{ set; get;}
    }
}
