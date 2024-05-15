using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public enum DefaultValueType
    {
        None = 0,        // 无  
        FixedValue = 1,  // 固定值  
        DefaultValue = 2, // 默认值(int等于0，空字符串等于空等)  
        CurrentTime = 3,  // 当前时间  
        ClaimKey = 4     // ClaimKey  
    }
}
