using System;
using System.Reflection;
using UiFramework.V2.Forms.Interfaces;

namespace Demo.Services
{
    public class AssemblySelector : IAssemblySelector
    {
        public Assembly Select(string model)
        {
            return Assembly.GetAssembly(typeof(AssemblySelector));

            // Could also switch on the model, in case the models are in different assemblies
            // 
            // switch (model)
            // {
            //     case "Demo.Models.User":
            //         return Assembly.GetAssembly(typeof(Models.User));
            // 
            //     default:
            //         throw new ArgumentException($"Model {model} is not implemented in IAssemblySelector.Select");
            // }
        }
    }
}
