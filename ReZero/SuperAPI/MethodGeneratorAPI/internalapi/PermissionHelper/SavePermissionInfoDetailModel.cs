using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class SavePermissionInfoDetailModel: ZeroPermissionInfo
    { 
        public List<string>? Users { get; set; }

        public List<PermissionInfoInterfaceItem>? items { get; set; }
    }
    public class PermissionInfoInterfaceItem 
    {
        public ZeroInterfaceList? ZeroInterfaceList { get; set; }
        public bool Checked { get; set; }
        public string? TypeName { get; set; }
    }
}
