using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ZeroSysSetting : DbBase, IDeleted
    {
        public int TypeId { get; set; } 
        public int ChildTypeId { get; set; }
        
        public bool BoolValue { get; set; }
    }
}
