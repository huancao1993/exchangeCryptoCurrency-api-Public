using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Services.Dto
{
    public interface ISearchBase
    {
         string Keyword { set; get; }
         int PageNumber { set; get; }
         int PageSize { set; get; }
    }
}
