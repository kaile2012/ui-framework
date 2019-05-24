using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UiFramework.V2.Forms.Pages;
using Demo.Models;
using Demo.Interfaces;
using Layout = Demo.Models.Layout;
using UiFramework.V2.Forms.Models;

namespace Demo.ViewModels
{
    public class PostPageViewModel : SnippetPageViewModel
    {
        private readonly Guid _id = Guid.Parse("09F06B15-1DF9-4C1F-8571-F06A5EE86470");
        private readonly IDataStore _dataStore = null;

        private Post _selectedPost;
        public Post SelectedPost
        {
            get => _selectedPost;
            set => SetProperty(ref _selectedPost, value);
        }

        private IList<PostResponse> _responses;
        public IList<PostResponse> Responses
        {
            get => _responses;
            set => SetProperty(ref _responses, value);
        }

        private IList<User> _responseUsers;
        public IList<User> ResponseUsers
        {
            get => _responseUsers;
            set => SetProperty(ref _responseUsers, value);
        }

        public PostPageViewModel(IDataStore dataStore, Post post)
        {
            _dataStore = dataStore;

            Layout layout = _dataStore.GetLayout(_id);
            layout.Items = _dataStore
                .GetLayoutItems(li => li.LayoutId == _id)
                .Select(li =>
                {
                    li.Parameters = _dataStore.GetLayoutItemParameters(lip => lip.LayoutItemId == li.Id).ToArray();
                    return li;
                }).ToArray();

            IEnumerable<User> users = dataStore.GetUsers(u => true);
            SelectedPost = post;
            Responses = dataStore.GetPostResponses(pr => pr.PostId == SelectedPost.Id && !pr.IsLike).ToList();
            ResponseUsers = Responses.Select(r => users.FirstOrDefault(u => u.Id == r.CommenterId)).ToList();
            SnippetLayout = layout;
        }
    }
}
