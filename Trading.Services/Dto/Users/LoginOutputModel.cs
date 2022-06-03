using System;
using System.ComponentModel.DataAnnotations;

namespace Trading.Services.Dto.Users
{
    public class LoginOutputModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {get { return ($"{FirstName} {LastName}");}}
        public string Token { get; set; }
    }
}