using System;
using UiFramework.V2.Forms.Models;
using UiFramework.V2.Forms.Interfaces;
using Demo.Interfaces;

namespace Demo.Services
{
    public class SnippetSelector : ISnippetSelector
    {
        private readonly IDataStore _dataStore;

        public SnippetSelector(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Snippet Select(Guid id)
        {
            return _dataStore.GetSnippet(id);
        }
    }
}
