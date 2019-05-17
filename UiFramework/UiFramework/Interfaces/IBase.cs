using System;
using System.Collections.Generic;

namespace UiFramework.V2.Interfaces
{
    /// <summary>
    /// A base interface.
    /// </summary>
    public interface IBase
    {
        /// <summary>
        /// The primary key.
        /// </summary>
        Guid Id { get; set; }
    }
}
