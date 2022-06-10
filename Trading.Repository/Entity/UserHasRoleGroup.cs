using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Repository.Entity
{
    public class UserHasRoleGroup:BaseTable
    {
        [Key]
        public int IdUserHasRoleGroup { set; get;}
        public int IdUser { get; set; }
        public int IdRoleGroup { set; get; }
        public Users User { set; get; }
        public RoleGroup RoleGroup { set; get; }
    }
}
