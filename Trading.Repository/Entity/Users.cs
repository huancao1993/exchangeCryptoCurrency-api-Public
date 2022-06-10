using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trading.Authen.Repository.Entity
{
    public partial class Users : BaseTable
    {
        public int Id { get; set; }
        public Guid? Code { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(200)]
        public byte Status { get; set; }
        public Guid? IdAvatar {get;set;}
        [MaxLength(100)]
        public string Phone {get;set;}
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Permissions> Permissions { set; get; } = new HashSet<Permissions>();
        public ICollection<UserHasRoleGroup> UserHasRoleGroups { set; get; } = new HashSet<UserHasRoleGroup>();
    }
}
