using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ResultModel
    {
        public ResultType? ResultType { get; set; }
        public string? GroupName { get; set; }
        public object? OutPutData { get;  set; }
        public string? ContentType { get; set; }
        public List<ResultColumnModel>? ResultColumnModels { get; set; }
    }
}
