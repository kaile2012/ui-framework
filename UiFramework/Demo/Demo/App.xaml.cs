using System;
using System.Collections.Generic;
using UiFramework.V2.Forms.Interfaces;
using UiFramework.V2.Forms.Pages;
using UiFramework.V2.Interfaces;
using UiFramework.V2.Forms;
using Newtonsoft.Json;
using Xamarin.Forms;
using Demo.Models;
using Demo.Services;
using Demo.ViewModels;
using Demo.Interfaces;

namespace Demo
{
    public partial class App : Application
    {
        public new App Current => (App)Application.Current;
        
        // Services
        public IDataStore DataStore { get; set; }

        public ISnippetSelector SnippetSelector { get; set; }

        public IModelSelector ModelSelector { get; set; }

        public IAssemblySelector AssemblySelector { get; set; }

        public App(IDataStore dataStore)
        {
            InitializeComponent();

            DataStore = dataStore;
            SnippetSelector = new SnippetSelector(DataStore);
            AssemblySelector = new AssemblySelector();
            ModelSelector = new ModelSelector(DataStore);

            Resources.Add("SnippetSelector", SnippetSelector);
            Resources.Add("AssemblySelector", AssemblySelector);
            Resources.Add("ModelSelector", ModelSelector);

            SnippetPage page = new SnippetPage();
            page.BindingContext = new MainPageViewModel(page.Navigation, DataStore);
            page.Title = "Demo";

            MainPage = new NavigationPage(page);
        }
    }
}
