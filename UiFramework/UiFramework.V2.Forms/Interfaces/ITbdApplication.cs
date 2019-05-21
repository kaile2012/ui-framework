using System;
using System.Reflection;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms
{
    public interface ITbdApplication
    {
        ISnippet GetSnippet(Guid id);

        object GetModel(string model, Guid id);

        string GetAssemblyQualification();
    }
}
