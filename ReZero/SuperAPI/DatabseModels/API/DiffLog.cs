using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ZeroDiffLog : DbBase
    {
        public string? TableName { get; set; }
        [SugarColumn(IsJson = true)]
        public JArray? BeforeData { get; set; }
        [SugarColumn(IsJson = true)]
        public JArray? AfterData { get; set; }
    }
}
