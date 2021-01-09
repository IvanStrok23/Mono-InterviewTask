using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Core.Entities.Helpers
{
    public class PagingParams
    {
        public SortParams SortParams { get;private set; }
        public int  Page { get; set; }

        public PagingParams()
        {
            Page = 1;
            SortParams = new SortParams();
            SortParams.SortBy = SortbyEnum.Name;
            SortParams.SortOrder = SortOrderEnum.Desc;
            SortParams.SearchValue = "";
        }


        public PagingParams(int page, SortbyEnum sortBy, SortOrderEnum sortOrder,string searchValue)
        {
            Page = page <= 0 ? 1 : page; 
            SortParams = new SortParams();
            SortParams.SortBy = sortBy;
            SortParams.SortOrder = sortOrder;
            SortParams.SearchValue = searchValue;
        }
    }
}
