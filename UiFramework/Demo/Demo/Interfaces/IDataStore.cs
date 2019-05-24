using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Demo.Models;

namespace Demo.Interfaces
{
    public interface IDataStore
    {
        Layout GetLayout(Guid id);

        IEnumerable<Layout> GetLayouts(Func<Layout, bool> expession);

        LayoutItem GetLayoutItem(Guid id);

        IEnumerable<LayoutItem> GetLayoutItems(Func<LayoutItem, bool> expession);

        LayoutItemParameter GetLayoutItemParameter(Guid id);

        IEnumerable<LayoutItemParameter> GetLayoutItemParameters(Func<LayoutItemParameter, bool> expession);

        Snippet GetSnippet(Guid id);

        IEnumerable<Snippet> GetSnippets(Func<Snippet, bool> expession);

        User GetUser(Guid id);

        IEnumerable<User> GetUsers(Func<User, bool> expession);

        Post GetPost(Guid id);

        IEnumerable<Post> GetPosts(Func<Post, bool> expession);

        PostResponse GetPostResponse(Guid id);

        IEnumerable<PostResponse> GetPostResponses(Func<PostResponse, bool> expession);

        // Generic

        bool DoesTableExist<T>();

        int Count<T>() where T : class, new();

        int Count<T>(Expression<Func<T, bool>> expression) where T : class, new();

        T GetItem<T>(Guid id) where T : class, new();

        T GetItem<T>(Expression<Func<T, bool>> expression) where T : class, new();

        IList<T> GetItems<T>() where T : class, new();

        IList<T> GetItems<T>(Expression<Func<T, bool>> expression) where T : class, new();

        bool TryGetItem<T>(Guid id, out T result) where T : class, new();

        bool TryGetItem<T>(Expression<Func<T, bool>> expression, out T result) where T : class, new();

        bool TryGetItems<T>(out IList<T> result) where T : class, new();

        bool TryGetItems<T>(Expression<Func<T, bool>> expression, out IList<T> result) where T : class, new();
    }
}
