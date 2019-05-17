using System;
using System.Collections.Generic;

namespace UiFramework.V2.Interfaces
{
    public interface IBase
    {
        Guid Id { get; set; }

        // IDictionary<string, string> Metadata { get; set; }
    }
}
