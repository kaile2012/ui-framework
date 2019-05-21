using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiFramework.V2.Forms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnippetPage : ContentPage
    {
        public SnippetPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null && !(BindingContext is ISnippetPageViewModel))
                throw new ArgumentException("The binding context of SnippetPage must implement ISnippetPageViewModel");
        }
    }
}
