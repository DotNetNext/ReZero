using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal class ActionTypeItemModel
    {
        /// <summary>
        /// Gets or sets the Chinese text.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Gets or sets the text group.
        /// </summary>
        public string? TextGroup { get; set; }
        /// <summary>
        /// Gets or sets the form elements.
        /// </summary>
        public object? FormElements { get;   set; }
    }
}
