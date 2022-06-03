using System;
using System.Collections.Generic;

namespace Trading.Repository.Entity
{
    public partial class Permissions :BaseTable
    {
        public int IdPermission { get; set; }
        public int IdUser { get; set; }
        public int IdRoleAction { get; set; }
        public virtual Users Users { get; set; }
        public virtual RoleAction RoleAction{ get; set; }
    }
}
