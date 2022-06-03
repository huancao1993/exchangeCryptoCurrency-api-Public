using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Repository.Entity
{
    public class Screen
    {    
        [Key]
        public int IdScree {set;get;}
        public string Name {set; get;}
        public int? ParentId {set; get;}
        public int? IdRole {set; get;}
        public Roles Role { set; get; }
        public ICollection<Screen> Screens { set; get; } = new HashSet<Screen>();

    }
}
