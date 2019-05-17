using System.ComponentModel;
using Xamarin.Forms;
using UiFramework.V2.DemoApp.ViewModels;

namespace UiFramework.V2.DemoApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MiscellaneousPage : ContentPage
    {
        public MiscellaneousPage()
        {
            InitializeComponent();
            BindingContext = new MiscellaneousPageViewModel();
        }
    }
}
