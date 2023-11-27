using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents the parameters for a tree structure.
    /// </summary>
    public class DataModelTreeParameter
    {
        /// <summary>
        /// Gets or sets the name of the property that contains the code.
        /// </summary>
        public string? CodePropertyName { get; set; }

        /// <summary>
        /// Gets or sets the root value of the tree.
        /// </summary>
        public object? RootValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that contains the parent code.
        /// </summary>
        public string? ParentCodePropertyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the property that contains the child nodes.
        /// </summary>
        public string? ChildPropertyName { get; set; }
    }
}
