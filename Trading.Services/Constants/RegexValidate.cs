using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Authen.Services.Constants
{
    public static class RegexValidate
    {
        public const string Email = "^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,50})+$";
        public const string Phone = "[0-9]*";
    }
}
