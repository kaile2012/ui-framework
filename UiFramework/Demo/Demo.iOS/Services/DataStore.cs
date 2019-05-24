using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using SQLite;
using Demo.Models;
using Demo.Interfaces;
using System.Linq.Expressions;
using Foundation;
using System.Text;

namespace Demo.iOS.Services
{
    public class DataStore : IDataStore
    {
        private SQLiteConnection _local;

        //
        // Initialisation
        //

        public DataStore()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            
            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);
            
            string file = Path.Combine(libFolder, "DemoAppDataStore.db3");
            string resource = NSBundle.MainBundle.PathForResource("DemoAppDataStore", "db3");
            File.Copy(resource, file, true);

            _local = new SQLiteConnection(file);

            //_local.CreateTables<Snippet, Layout, LayoutItem>();
            //_local.CreateTables<User, Post, PostResponse>();

            //Random rng = new Random();
            //IList<User> users = GetUsers(u => true).ToList();
            //
            //IList<Post> posts = new List<Post>();
            //foreach (User user in users)
            //{
            //    double d = rng.NextDouble();
            //    if (d < 0.2)
            //        continue;
            //
            //    int count = rng.Next(1, 10);
            //    for (int i = 0; i < count; ++i)
            //        posts.Add(new Post
            //        {
            //            Id = Guid.NewGuid(),
            //            PosterId = user.Id,
            //            Content = LoremIpsum(rng, 5, 30, 1, 5, 1)
            //        });
            //}
            //
            //_local.InsertAll(posts);
            //
            //IList<PostResponse> responses = new List<PostResponse>();
            //foreach (Post post in GetPosts(p => true))
            //{
            //    double d = rng.NextDouble();
            //    if (d < 0.1)
            //        continue;
            //
            //    int likeCount = rng.Next(1, 20);
            //    for (int i = 0; i < likeCount; ++i)
            //        responses.Add(new PostResponse
            //        {
            //            Id = Guid.NewGuid(),
            //            PostId = post.Id,
            //            CommenterId = GetRandomUserId(rng, users, post.PosterId),
            //            Content = null,
            //            IsLike = true
            //        });
            //
            //    if (d < 0.6)
            //    {
            //        int commentCount = rng.Next(1, 5);
            //        for (int i = 0; i < commentCount; ++i)
            //            responses.Add(new PostResponse
            //            {
            //                Id = Guid.NewGuid(),
            //                PostId = post.Id,
            //                CommenterId = GetRandomUserId(rng, users, post.PosterId),
            //                Content = LoremIpsum(rng, 5, 30, 1, 5, 1),
            //                IsLike = false
            //            });
            //    }
            //}
            //
            //_local.InsertAll(responses);
        }

        /// <summary>
        /// Method found at: https://stackoverflow.com/questions/4286487/is-there-any-lorem-ipsum-generator-in-c#answer-4286571
        /// By users: Greg and Chris Ballance
        /// </summary>
        private string LoremIpsum(Random rng, int minWords, int maxWords, int minSentences, int maxSentences, int numParagraphs)
        {
            string[] words = new[] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed",
                "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };

            int numSentences = rng.Next(maxSentences - minSentences) + minSentences + 1;
            int numWords = rng.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rng.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        private Guid GetRandomUserId(Random rng, IList<User> users, params Guid[] exceptions)
        {
            int i = rng.Next(0, users.Count);
            User user = users[i];
            return !exceptions.Contains(user.Id)
                ? user.Id
                : GetRandomUserId(rng, users, exceptions);
        }

        //
        // Specific
        //

        public Layout GetLayout(Guid id)
        {
            return this.GetItems<Layout>().FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Layout> GetLayouts(Func<Layout, bool> expession)
        {
            return this.GetItems<Layout>().Where(expession);
        }

        public LayoutItem GetLayoutItem(Guid id)
        {
            return this.GetItems<LayoutItem>().FirstOrDefault(li => li.Id == id);
        }

        public IEnumerable<LayoutItem> GetLayoutItems(Func<LayoutItem, bool> expession)
        {
            return this.GetItems<LayoutItem>().Where(expession);
        }

        public Snippet GetSnippet(Guid id)
        {
            return this.GetItems<Snippet>().FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Snippet> GetSnippets(Func<Snippet, bool> expession)
        {
            return this.GetItems<Snippet>().Where(expession);
        }

        public User GetUser(Guid id)
        {
            return this.GetItems<User>().FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> expession)
        {
            return this.GetItems<User>().Where(expession);
        }

        public Post GetPost(Guid id)
        {
            return this.GetItems<Post>().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetPosts(Func<Post, bool> expession)
        {
            return this.GetItems<Post>().Where(expession);
        }

        public PostResponse GetPostResponse(Guid id)
        {
            return this.GetItems<PostResponse>().FirstOrDefault(pr => pr.Id == id);
        }

        public IEnumerable<PostResponse> GetPostResponses(Func<PostResponse, bool> expession)
        {
            return this.GetItems<PostResponse>().Where(expession);
        }

        //
        // Generic
        //

        public bool DoesTableExist<T>()
        {
            string query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{typeof(T).Name}';";
            string result = _local.ExecuteScalar<string>(query);

            if (string.IsNullOrWhiteSpace(result))
                return false;

            if (result.Equals(typeof(T).Name))
                return true;

            return Convert.ToInt32(result) != 0;
        }

        public int Count<T>()
            where T : class, new()
        {
            bool tableExists = DoesTableExist<T>();
            if (!tableExists)
                throw new ArgumentNullException(nameof(tableExists), $"Table {typeof(T).Name} does not exist");

            return _local.Table<T>().Count();
        }

        public int Count<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            bool tableExists = DoesTableExist<T>();
            if (!tableExists)
                throw new ArgumentNullException(nameof(tableExists), $"Table {typeof(T).Name} does not exist");

            return _local.Table<T>().Count(expression);
        }

        public IList<T> GetItems<T>()
            where T : class, new()
        {
            bool tableExists = DoesTableExist<T>();
            if (!tableExists)
                throw new ArgumentNullException(nameof(tableExists), $"Table {typeof(T).Name} does not exist");

            IList<T> list = _local.Table<T>().ToList();
            return list;
        }

        public IList<T> GetItems<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            bool tableExists = DoesTableExist<T>();
            if (!tableExists)
                throw new ArgumentNullException(nameof(tableExists), $"Table {typeof(T).Name} does not exist");

            IList<T> list = _local.Table<T>().Where(expression).ToList();
            return list;
        }

        public T GetItem<T>(Guid id)
            where T : class, new()
        {
            bool tableExists = DoesTableExist<T>();
            if (!tableExists)
                throw new ArgumentNullException(nameof(tableExists), $"Table {typeof(T).Name} does not exist");

            T item = _local.Find<T>(id);
            return item;
        }

        public T GetItem<T>(Expression<Func<T, bool>> expression)
            where T : class, new()
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            bool tableExists = DoesTableExist<T>();
            if (!tableExists)
                throw new ArgumentNullException(nameof(tableExists), $"Table {typeof(T).Name} does not exist");

            T item = _local.Find(expression);
            return item;
        }

        public bool TryGetItems<T>(out IList<T> result)
            where T : class, new()
        {
            try
            {
                result = GetItems<T>();
                if (result == null)
                    throw new ArgumentNullException(nameof(result));

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                result = null;
                return false;
            }
        }

        public bool TryGetItems<T>(Expression<Func<T, bool>> expression, out IList<T> result)
            where T : class, new()
        {
            try
            {
                result = GetItems(expression);
                if (result == null)
                    throw new ArgumentNullException(nameof(result));

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                result = null;
                return false;
            }
        }

        public bool TryGetItem<T>(Guid id, out T result)
            where T : class, new()
        {
            try
            {
                result = GetItem<T>(id);
                if (result == null)
                    throw new ArgumentNullException(nameof(result));

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                result = default;
                return false;
            }
        }

        public bool TryGetItem<T>(Expression<Func<T, bool>> expression, out T result)
            where T : class, new()
        {
            try
            {
                result = GetItem(expression);
                if (result == null)
                    throw new ArgumentNullException(nameof(result));

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                result = default;
                return false;
            }
        }
    }
}
