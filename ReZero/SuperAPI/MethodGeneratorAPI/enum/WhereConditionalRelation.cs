using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public enum WhereRelation
    {
        /// <summary>  
        /// And：并且-带OR参数NULL  
        /// </summary>  
        And,

        /// <summary>  
        /// And all：并且  
        /// </summary>  
        AndAll,

        /// <summary>  
        /// Or：或者-带OR参数NULL  
        /// </summary>  
        Or,

        /// <summary>  
        /// OrAll：或者  
        /// </summary>  
        OrAll,

        /// <summary>  
        /// Custom：自定义-带OR参数NULL  
        /// </summary>  
        Custom,

        /// <summary>  
        /// CustomAll：自定义  
        /// </summary>  
        CustomAll
    }
}
