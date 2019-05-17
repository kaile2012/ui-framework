using MvvmHelpers;
using Xamarin.Forms;
using System.Diagnostics;
using Layout = UiFramework.V2.DemoApp.Models.Layout;
using UiFramework.V2.DemoApp.Models;

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

        private Command<LayoutItemTappedArgs> _tappedCommand;
        public Command<LayoutItemTappedArgs> TappedCommand => _tappedCommand ?? (_tappedCommand = new Command<LayoutItemTappedArgs>(OnTapped));

        public virtual void OnTapped(LayoutItemTappedArgs args)
        {
            Debug.WriteLine(args.CommandParameter);
        }
    }
}
