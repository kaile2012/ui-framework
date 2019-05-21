using System;
using System.Diagnostics;
using System.Collections.Generic;
using Xamarin.Forms;
using MvvmHelpers;
using UiFramework.V2.Forms.Models;
using Layout = UiFramework.V2.Forms.Models.Layout;

namespace UiFramework.V2.Forms.Pages
{
    public class SnippetPageViewModel : BaseViewModel, ISnippetPageViewModel
    {
        private Command<LayoutItemTappedArgs> _itemTappedCommand;
        public Command<LayoutItemTappedArgs> ItemTappedCommand
        {
            get => _itemTappedCommand ?? (_itemTappedCommand = new Command<LayoutItemTappedArgs>(OnTapped));
            set => throw new NotImplementedException();
        }

        public Layout _snippetLayout;
        public Layout SnippetLayout
        {
            get => _snippetLayout;
            set => SetProperty(ref _snippetLayout, value);
        }

        private IDictionary<string, Action<LayoutItemTappedArgs>> _methods;
        public IDictionary<string, Action<LayoutItemTappedArgs>> Methods
        {
            get => _methods ?? (_methods = new Dictionary<string, Action<LayoutItemTappedArgs>>());
            set => throw new NotImplementedException();
        }

        public void OnTapped(LayoutItemTappedArgs args)
        {
            try
            {
                if (args == null)
                    throw new ArgumentNullException(nameof(args));
                if (args.SnippetLayoutItem == null)
                    throw new ArgumentNullException(nameof(args.SnippetLayoutItem));
                if (string.IsNullOrWhiteSpace(args.SnippetLayoutItem.OnTappedMethodName))
                    return;
                if (!Methods.ContainsKey(args.SnippetLayoutItem.OnTappedMethodName))
                    throw new NotImplementedException($"Method '{args.SnippetLayoutItem.OnTappedMethodName}' is not implemented, or has not been registered in ISnippetPageViewModel.Methods");

                Methods[args.SnippetLayoutItem.OnTappedMethodName](args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
