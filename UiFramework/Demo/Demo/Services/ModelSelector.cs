using System;
using System.Collections.Generic;
using UiFramework.V2.Forms.Interfaces;
using Demo.Models;
using Demo.Interfaces;

namespace Demo.Services
{
    public class ModelSelector : IModelSelector
    {
        private readonly IDataStore _dataStore;

        public ModelSelector(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public object SelectSingle(string model, Guid id)
        {
            switch (model)
            {
                case "Demo.Models.User":
                    return _dataStore.GetUser(id);

                case "Demo.Models.Post":
                    return _dataStore.GetPost(id);

                case "Demo.Models.PostResponse":
                    return _dataStore.GetPostResponse(id);

                default:
                    throw new ArgumentException($"Model {model} is not implemented in IModelSelector.SelectSingle");
            }
        }

        public IEnumerable<object> SelectMany(string model, string filter)
        {
            // Until we can properly store a filtering expression as a string, we can just treat
            // the filter string as a key and have users implement the filter in their code.

            switch (model)
            {
                case "Demo.Models.User" when string.IsNullOrWhiteSpace(filter):
                case "Demo.Models.User" when filter == "all":
                    return _dataStore.GetUsers(u => true);

                case "Demo.Models.Post" when string.IsNullOrWhiteSpace(filter):
                case "Demo.Models.Post" when filter == "all":
                    return _dataStore.GetPosts(p => true);

                case "Demo.Models.PostResponse" when string.IsNullOrWhiteSpace(filter):
                case "Demo.Models.PostResponse" when filter == "all":
                    return _dataStore.GetPostResponses(pr => true);

                case "Demo.Models.User" when filter == "FollowerCount >= 100":
                    return _dataStore.GetUsers(u => u.FollowerCount >= 100);

                default:
                    throw new ArgumentException($"{model} \"{filter}\" is not implemented in IModelSelector.SelectMany");
            }
        }

        public Func<object, string> GetKeySelector(string model)
        {
            switch (model)
            {
                case "Demo.Models.User":
                    return new Func<object, string>(u => ((User) u).Id.ToString("D"));

                case "Demo.Models.Post":
                    return new Func<object, string>(p => ((Post) p).Id.ToString("D"));

                case "Demo.Models.PostResponse":
                    return new Func<object, string>(pr => ((PostResponse) pr).Id.ToString("D"));

                default:
                    throw new ArgumentException($"Model {model} is not implemented in IModelSelector.GetKeySelector");
            }
        }
    }
}
