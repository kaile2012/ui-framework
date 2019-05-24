using System;
using System.Collections.Generic;

namespace UiFramework.V2.Forms.Interfaces
{
    public interface IModelSelector
    {
        object SelectSingle(string model, Guid id);

        IEnumerable<object> SelectMany(string model, string filter);

        Func<object, string> GetKeySelector(string model);
    }
}
