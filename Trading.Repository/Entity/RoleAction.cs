using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Repository.Entity
{
    public class RoleAction
    {
        public RoleAction()
        {
            Permissions = new HashSet<Permissions>();
            RoleGroupActions = new HashSet<RoleGroupAction>();
        }
        [Key]
        public int IdRoleAction { get; set; }
        public string RoleActionName {get; set;}
        public string Code { get; set; }
        public int IdRole { get; set; }
        public virtual Roles Role { get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
        public ICollection<RoleGroupAction> RoleGroupActions { set; get; }
    }
}
