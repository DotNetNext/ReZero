using ReZero.DependencyInjection;
using ReZero.SuperAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
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

        /// <summary>
        /// Gets or sets the options for the DependencyInjection.
        /// </summary>
        public DependencyInjectionOptions DependencyInjectionOptions { get; set; } = new DependencyInjectionOptions();
    }
}
