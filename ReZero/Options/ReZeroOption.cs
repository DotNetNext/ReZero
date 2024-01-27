using ReZero.SuperAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    /// <summary>
    /// Represents the options for the ReZero class.
    /// </summary>
    public class ReZeroOptions
    {
        /// <summary>
        /// Gets or sets the options for the SuperAPI.
        /// </summary>
        public SuperAPIOptions SuperApiOptions { get; set; } = new SuperAPIOptions();
    }
}
