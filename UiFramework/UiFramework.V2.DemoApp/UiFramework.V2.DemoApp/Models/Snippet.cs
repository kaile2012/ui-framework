using System;
using MvvmHelpers;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.DemoApp.Models
{
    public class Snippet : ObservableObject, ISnippet
    {
        private Guid _id;
        private string _name;
        private string _description;
        private string _html;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Html
        {
            get => _html;
            set => SetProperty(ref _html, value);
        }
    }
}
