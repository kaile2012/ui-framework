using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UiFramework.V2.DemoApp.ViewModels;

namespace UiFramework.V2.DemoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnippetPage : ContentPage
    {
        public new SnippetPageViewModel BindingContext
        {
            get => (SnippetPageViewModel) base.BindingContext;
            set => base.BindingContext = value;
        }

        public SnippetPage()
        {
            InitializeComponent();
            BindingContext = new SnippetPageViewModel();
        }
    }
}