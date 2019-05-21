using System;
using Xamarin.Forms;
using MvvmHelpers;
using UiFramework.V2.Forms.Models;
using Layout = UiFramework.V2.Forms.Models.Layout;

namespace UiFramework.V2.Forms.Pages
{
    public class SnippetPageViewModel : BaseViewModel, ISnippetPageViewModel
    {
        private Command<LayoutItemTappedArgs> _tappedCommand;
        public Command<LayoutItemTappedArgs> TappedCommand
        {
            get => _tappedCommand ?? (_tappedCommand = new Command<LayoutItemTappedArgs>(OnTapped));
            set => throw new NotImplementedException();
        }

        public Layout _snippetLayout;
        public Layout SnippetLayout
        {
            get => _snippetLayout;
            set => SetProperty(ref _snippetLayout, value);
        }

        private void OnTapped(LayoutItemTappedArgs args)
        {
        }
    }
}
