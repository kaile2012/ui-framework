using System;
using System.Collections.Generic;
using System.Text;

namespace UiFramework.V2.Enums
{
    /// <summary>
    /// An enum to represent what sort of item is being bound to a snippet.
    /// </summary>
    public enum Parameter
    {
        /// <summary>
        /// A single item.
        /// </summary>
        Single = 0,
        /// <summary>
        /// A collection of items being bound to a snippet, to create a list of elements.
        /// </summary>
        Many = 1
    }
}
