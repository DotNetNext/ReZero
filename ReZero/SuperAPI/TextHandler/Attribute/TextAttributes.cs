using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Represents an attribute for Chinese text.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ChineseTextAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChineseTextAttribute"/> class with the specified text.
        /// </summary>
        /// <param name="text">The Chinese text.</param>
        public ChineseTextAttribute(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the Chinese text.
        /// </summary>
        public string? Text { get; set; }
    }

    /// <summary>
    /// Represents an attribute for English text.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class EnglishTextAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishTextAttribute"/> class with the specified text.
        /// </summary>
        /// <param name="text">The English text.</param>
        public EnglishTextAttribute(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the English text.
        /// </summary>
        public string? Text { get; set; }
    }
}
