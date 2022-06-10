using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Repository.Entity
{
    public class RoleGroup:BaseTable
    {
        public RoleGroup()
        {
            RoleGroupActions = new HashSet<RoleGroupAction>();
            UserHasRoleGroups = new HashSet<UserHasRoleGroup>();
        }
        [Key]
        public int IdRoleGroup {set;get;}
        [MaxLength(200)]
        public string Name { set; get; }
        public ICollection<RoleGroupAction> RoleGroupActions {set; get;}
        public byte? Status {set; get;}
        [MaxLength(200)]
        public string Description {set;get;}
        public ICollection<UserHasRoleGroup> UserHasRoleGroups {set; get;}
    }
}
