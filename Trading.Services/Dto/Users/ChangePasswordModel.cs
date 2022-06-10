using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Services.Dto.Users
{
    public class ChangePasswordModel
    {
        public string OldPassWord { set; get; }
        public string NewPassWord{ set; get;}
    }
}
