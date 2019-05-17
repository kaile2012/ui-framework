using System;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.DemoApp.Models
{
    public class LayoutItemTappedArgs : EventArgs
    {
        public object CommandParameter { get; set; }

        public object JavaScriptMethodReturn { get; set; }
    }
}
