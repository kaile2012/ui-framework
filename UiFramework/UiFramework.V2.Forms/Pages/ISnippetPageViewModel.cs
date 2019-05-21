using System;
using System.Collections.Generic;
using Xamarin.Forms;
using UiFramework.V2.Forms.Models;

namespace UiFramework.V2.Forms.Pages
{
    public interface ISnippetPageViewModel
    {
        Command<LayoutItemTappedArgs> ItemTappedCommand { get; set; }

        Models.Layout SnippetLayout { get; set; }

        IDictionary<string, Action<LayoutItemTappedArgs>> Methods { get; set; }

        void OnTapped(LayoutItemTappedArgs args);
    }
}
