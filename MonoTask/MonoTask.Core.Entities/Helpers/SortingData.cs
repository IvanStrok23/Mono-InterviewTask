using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoTask.UI.Web.Helper
{
    public class SortingData
    {
        public int Page { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public string SearchValue { get; set; }

        public SortingData()
        {
            Page = 1;
            SortBy = "Name";
            SortOrder = "asc";
            SearchValue = "";
            
        }
    }
}