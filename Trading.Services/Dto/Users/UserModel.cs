using System;
using System.Collections.Generic;

namespace Trading.Authen.Services.Dto.Users
{
  public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Guid? Code { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public int IdDepartment { set; get; }
        public string Phone { get; set; }
        public string DepartmentName { set; get; }
        public string Position { get; set; }
        public DateTime? DOB { get; set; }
        public byte? Status { get; set; }
        public Guid? IdAvatar { get; set; }
        public  IList<RoleGroupByUserModel> RoleGroups { get; set; }
    }
    public class RoleGroupByUserModel{
        public int IdRoleGroup { set; get; }
        public string Name { set; get; }
        public byte? Status { set; get; }
        public string Description { set; get; }
    }


}