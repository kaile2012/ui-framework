using System.Reflection;

namespace UiFramework.V2.Forms.Interfaces
{
    public interface IAssemblySelector
    {
        Assembly Select(string model);
    }
}
