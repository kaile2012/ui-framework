using System;
using System.Linq;
using UiFramework.V2.Forms.Models;
using UiFramework.V2.Forms.Pages;
using Xamarin.Forms;
using Demo.Models;
using Demo.Interfaces;
using Layout = Demo.Models.Layout;

namespace Demo.ViewModels
{
    public class MainPageViewModel : SnippetPageViewModel
    {
        private readonly Guid _id = Guid.Parse("500BC899-7446-4367-957C-53451DDA3024");
        private readonly INavigation _navigation = null;
        private readonly IDataStore _dataStore = null;

        public MainPageViewModel(INavigation navigation, IDataStore dataStore)
        {
            _navigation = navigation;
            _dataStore = dataStore;

            Methods.Add(nameof(Navigate), Navigate);

            Layout layout = _dataStore.GetLayout(_id);
            layout.Items = _dataStore.GetLayoutItems(li => li.LayoutId == _id).ToArray();

            SnippetLayout = layout;
        }

        public void Navigate(LayoutItemTappedArgs args)
        {
            if (!Guid.TryParse(args.SnippetLayoutItem.Parameter, out Guid userId))
                return;

            User user = _dataStore.GetUser(userId);
            if (user == null)
                return;

            SnippetPage page = new SnippetPage();
            page.BindingContext = new UserPageViewModel(page.Navigation, _dataStore, user);

            _navigation.PushAsync(page);
        }
    }
}
