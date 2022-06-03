using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Repository.Entity
{
    public class RoleGroupAction : BaseTable
    {
        [Key]
        public int IdRoleGroupAction  { set;get;}
        public int IdRoleGroup { set; get; }
        public int IdRoleAction { set; get; }
        public RoleAction RoleAction { set; get; }
        public RoleGroup RoleGroup { set; get; }
    }
}
