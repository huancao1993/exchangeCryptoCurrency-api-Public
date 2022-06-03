using System;
using System.Collections.Generic;

namespace Trading.Repository.Entity
{
    public partial class Roles
    {
        public Roles()
        {
            RoleActions = new HashSet<RoleAction>();
            Screes = new HashSet<Screen>();
        }
        public int IdRole {get; set;}
        public string Code {get; set;}
        public string RoleName {get; set;}
        //public virtual ICollection<RoleHasAction> RoleHasAction {get; set;}
        public virtual ICollection<RoleAction> RoleActions { get; set; }
        public virtual ICollection<Screen> Screes { set; get;}
    }
}
