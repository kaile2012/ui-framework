using MvvmHelpers;
using Layout = UiFramework.V2.DemoApp.Models.Layout;

namespace UiFramework.V2.DemoApp.ViewModels
{
    public class SnippetPageViewModel : BaseViewModel
    {
        private Layout _layout;
        public Layout Layout
        {
            get => _layout;
            set => SetProperty(ref _layout, value);
        }
    }
}
