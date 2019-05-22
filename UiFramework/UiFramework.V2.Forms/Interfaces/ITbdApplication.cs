using System;
using System.Collections.Generic;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms
{
    public interface ITbdApplication
    {
        ISnippet GetSnippet(Guid id);

        object GetModel(string model, Guid id);

        IEnumerable<object> GetModels(string model, string filterString);

        Func<object, string> GetModelKeySelector(string model);

        string GetAssemblyQualification();
    }
}
