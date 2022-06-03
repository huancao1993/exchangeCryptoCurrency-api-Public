using System.ComponentModel.DataAnnotations;
using System;
namespace Trading.Services.Dto.Users
{
  public class UpdateUsersModel
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public byte? Status { get; set; }
        public int? IdDepartment { set; get; }
        [RegularExpression("[0-9]*", ErrorMessage = "Số điện thoại không đúng")]
        [MaxLength(13)]
        public string Phone { get; set; }
        public DateTimeOffset DOB { get; set; }

}
}