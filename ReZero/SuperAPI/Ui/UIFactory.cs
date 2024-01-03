using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Default UI usage, not used for Vue front-end and back-end separation
    /// </summary>
    public class UIFactory
    {
        // Although this method is not used for VUE, it is retained for compatibility with other users and secondary development
        public static IUiManager uiManager = new DefaultUiManager();
    }
}
