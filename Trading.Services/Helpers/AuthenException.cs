using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Trading.Services.Helpers
{
    public class AuthenException : Exception
    {
        public AuthenException() : base() { }

        public AuthenException(string message) : base(message) { }

        public AuthenException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
