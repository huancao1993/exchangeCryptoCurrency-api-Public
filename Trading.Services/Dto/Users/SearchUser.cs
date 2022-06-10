using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Services.Dto.Users
{
    public class SearchUser: SearchBase,ISearchBase
    {
        public byte? Status { set; get;}
        public int? IdDepartment { set; get; }
    }
}
