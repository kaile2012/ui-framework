using Xamarin.Forms;

namespace UiFramework.V2.Forms.Models
{
    public class LayoutItemTappedArgs : BindableObject
    {
        public static readonly BindableProperty SnippetLayoutItemProperty = BindableProperty.Create(
            nameof(SnippetLayoutItem),
            typeof(LayoutItem),
            typeof(LayoutItemTappedArgs)
        );

        public static readonly BindableProperty JavaScriptMethodReturnProperty = BindableProperty.Create(
            nameof(JavaScriptMethodReturn),
            typeof(object),
            typeof(LayoutItemTappedArgs)
        );

        public LayoutItem SnippetLayoutItem
        {
            get => (LayoutItem) GetValue(SnippetLayoutItemProperty);
            set => SetValue(SnippetLayoutItemProperty, value);
        }

        public object JavaScriptMethodReturn
        {
            get => GetValue(JavaScriptMethodReturnProperty);
            set => SetValue(JavaScriptMethodReturnProperty, value);
        }
    }
}
