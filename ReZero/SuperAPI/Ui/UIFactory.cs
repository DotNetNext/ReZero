using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Ui
{
    /// <summary>
    /// 默认UI使用，如果是Vue前后分离不会使用该类
    /// </summary>
    public class UIFactory
    { 
        //虽然VUE等不会用这个方法但是为了兼容其他用户等二次开发，所以还是保留足够扩展
        public  static  IUiManager uiManager = new DefaultUiManager();
    }
}
