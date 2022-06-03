using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Repository.Entity
{
    public class BaseTable
    {
        [MaxLength(100)]
        public string CreateBy { get; set; }
        [MaxLength(100)]
        public string UpdateBy {get; set;}
        public DateTime? CreateAt {get; set;}
        public DateTime? UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
