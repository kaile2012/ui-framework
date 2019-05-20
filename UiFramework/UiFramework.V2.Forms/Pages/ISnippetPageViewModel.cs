using Xamarin.Forms;
using UiFramework.V2.Forms.Models;

namespace UiFramework.V2.Forms.Pages
{
    public interface ISnippetPageViewModel
    {
        Command<LayoutItemTappedArgs> TappedCommand { get; set; } // Need the implementation to call the ILayoutItem.OnTappedMethodName

        Models.Layout SnippetLayout { get; set; }
    }
}
