using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class SaveInterfaceListModel
    {
        public string? Url { get; set; }
        public string? GroupName { get; set; }
        public string? TableId { get; set; }
        public string? Name { get; set; }
        public string? InterfaceCategoryId { get; set; }
        public ActionType? ActionType { get; set; }
    }
}
