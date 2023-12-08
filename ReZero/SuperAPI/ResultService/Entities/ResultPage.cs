using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ResultPage
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public  int TotalCount { get; set; }
        public int TotalPage { get;  set; }
    }
}
