using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Repository.Entity
{
    public class Department:BaseTable
    {
        public Department()
        {
            Users = new HashSet<Users>();
            Departments = new HashSet<Department>();
        }

        [Key]
        public int IdDepartment {set; get;} 
        public string Name {set; get; }
        public int? ParentId { set; get; }
        public byte?  Status { set; get; }
        public ICollection<Users> Users { set; get; }
        public ICollection<Department> Departments { set; get; }

    }
}
