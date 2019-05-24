using System;
using System.Linq;
using System.Collections.Generic;
using UiFramework.V2.Forms.Pages;
using UiFramework.V2.Interfaces;
using Xamarin.Forms;
using Demo.Models;
using Demo.Interfaces;
using Layout = Demo.Models.Layout;
using UiFramework.V2.Forms.Models;

namespace Demo.ViewModels
{
    public class UserPageViewModel : SnippetPageViewModel
    {
        private readonly Guid _id = Guid.Parse("B4630BDE-BA63-42DF-8697-110B37CEEEF2");
        private readonly INavigation _navigation = null;
        private readonly IDataStore _dataStore = null;

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        private IList<Post> _userPosts;
        public IList<Post> UserPosts
        {
            get => _userPosts;
            set => SetProperty(ref _userPosts, value);
        }

        public UserPageViewModel(INavigation navigation, IDataStore dataStore, User user)
        {
            _navigation = navigation;
            _dataStore = dataStore;

            Methods.Add(nameof(Navigate), Navigate);
            Methods.Add(nameof(OpenImage), OpenImage);

            Layout layout = _dataStore.GetLayout(_id);
            layout.Items = _dataStore
                .GetLayoutItems(li => li.LayoutId == _id)
                .Select(li =>
                {
                    li.Parameters = _dataStore.GetLayoutItemParameters(lip => lip.LayoutItemId == li.Id).ToArray();
                    return li;
                }).ToArray();

            SelectedUser = user;
            UserPosts = dataStore.GetPosts(p => p.PosterId == SelectedUser.Id).ToList();
            SnippetLayout = layout;
        }

        public void Navigate(LayoutItemTappedArgs args)
        {
            ILayoutItemParameter parameter = args.SnippetLayoutItem.Parameters.FirstOrDefault(p => p.Model == "Demo.Models.Post");
            if (!Guid.TryParse(parameter.Value, out Guid postId))
                return;

            Post post = _dataStore.GetPost(postId);
            if (post == null)
                return;

            SnippetPage page = new SnippetPage();
            page.Title = post.Content;
            page.BindingContext = new PostPageViewModel(_dataStore, post);

            _navigation.PushAsync(page);
        }

        public void OpenImage(LayoutItemTappedArgs args)
        {
            string url = args.JavaScriptMethodReturn as string;
            if (string.IsNullOrWhiteSpace(url))
                return;

            Device.OpenUri(new Uri(url));
        }
    }
}
