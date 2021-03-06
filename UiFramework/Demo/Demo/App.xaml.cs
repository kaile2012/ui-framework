﻿using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using UiFramework.V2.Forms.Models;
using UiFramework.V2.Forms.Pages;
using UiFramework.V2.Interfaces;
using UiFramework.V2.Forms;
using UiFramework.V2.Enums;
using Xamarin.Forms;
using Demo.Models;
using Layout = UiFramework.V2.Forms.Models.Layout;

namespace Demo
{
    public partial class App : Application, ITbdApplication
    {
        // Just a quick solution to store some examples
        public IDictionary<Guid, Layout> Layouts = null;
        public IDictionary<Guid, Snippet> Snippets = null;
        public IDictionary<Guid, LayoutItem> LayoutItems = null;
        public IDictionary<Guid, User> Users = null;

        public App()
        {
            InitializeComponent();
            Seed();

            MainPage = new SnippetPage
            {
                BindingContext = new SnippetPageViewModel(),
                BackgroundColor = Color.Gray
            };

            ((ISnippetPageViewModel) MainPage.BindingContext).Methods.Add(nameof(Print), Print);
            ((ISnippetPageViewModel) MainPage.BindingContext).Methods.Add(nameof(NavigateToAnotherPage), NavigateToAnotherPage);
            ((ISnippetPageViewModel) MainPage.BindingContext).SnippetLayout = Layouts[Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756")];
        }
        
        public void Seed()
        {
            Layouts = new Dictionary<Guid, Layout>();
            Snippets = new Dictionary<Guid, Snippet>();
            LayoutItems = new Dictionary<Guid, LayoutItem>();
            Users = new Dictionary<Guid, User>();

            Layouts.Add(Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"), new Layout
            {
                Id = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                Name = "Example",
                Description = "",
                RowCount = 3,
                ColumnCount = 1,
                Items = null
            });

            Snippets.Add(Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"), new Snippet
            {
                Id = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                Name = "Text snippet",
                Description = "Just expects a string, no model.",
                Html = "<html><style>html, body { margin:0px; padding:0px; max-width:100%; min-width:100%; } p { font-size: 22px; }</style><body><p>{.}</p></body></html>"
            });
            Snippets.Add(Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"), new Snippet
            {
                Id = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                Name = "User snippet",
                Description = "Binds the properties of a User object.",
                Html = "<html><style>html, body { margin:0px; padding:0px; max-width:100%; min-width:100%; } p { font-size: 18px; }</style><body><img src=\"{ImageUri}\"><p>{Name} - {UserName}</p><p>{FollowerCount} followers</p></body></html>"
            });

            LayoutItems.Add(Guid.Parse("50ecebda-c817-4c03-9cd2-75a397e24417"), new LayoutItem
            {
                Id = Guid.Parse("50ecebda-c817-4c03-9cd2-75a397e24417"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = "",
                Parameter = "Users with 100 or more followers",
                OnTappedMethodName = "Print",
                Row = 0,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("e147e0af-4e63-433a-9685-72910209e3e5"), new LayoutItem
            {
                Id = Guid.Parse("e147e0af-4e63-433a-9685-72910209e3e5"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                ParameterType = Parameter.Many,
                ParameterModel = "Demo.Models.User",
                Parameter = "FollowerCount >= 100",
                OnTappedMethodName = "NavigateToAnotherPage",
                Row = 1,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("d0fdbbc7-c405-483b-bbd9-6edbc1db837f"), new LayoutItem
            {
                Id = Guid.Parse("d0fdbbc7-c405-483b-bbd9-6edbc1db837f"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = "",
                Parameter = "All users",
                OnTappedMethodName = "Print",
                Row = 2,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("db476a6e-4630-423e-9fc1-d02f2531d581"), new LayoutItem
            {
                Id = Guid.Parse("db476a6e-4630-423e-9fc1-d02f2531d581"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                ParameterType = Parameter.Many,
                ParameterModel = "Demo.Models.User",
                Parameter = "",
                OnTappedMethodName = "NavigateToAnotherPage",
                Row = 3,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });

            foreach (var layout in Layouts)
                layout.Value.Items = LayoutItems.Values.Where(li => li.LayoutId == layout.Key).ToArray();

            Users.Add(Guid.Parse("51294eb9-8852-4688-bdfa-30637e267a07"), new User
            {
                Id = Guid.Parse("51294eb9-8852-4688-bdfa-30637e267a07"),
                UserName = "bloggs",
                Name = "Joe Bloggs",
                ImageUri = "https://randomuser.me/api/portraits/men/65.jpg",
                FollowerCount = 550
            });
            Users.Add(Guid.Parse("c80e7da6-405d-4840-a344-2d08fa5e5ba4"), new User
            {
                Id = Guid.Parse("c80e7da6-405d-4840-a344-2d08fa5e5ba4"),
                UserName = "michelle.thompson41",
                Name = "Michelle Thompson",
                ImageUri = "https://randomuser.me/api/portraits/women/21.jpg",
                FollowerCount = 827
            });
            Users.Add(Guid.Parse("7ee559fa-aa91-4fe2-8d55-91388dc75746"), new User
            {
                Id = Guid.Parse("7ee559fa-aa91-4fe2-8d55-91388dc75746"),
                UserName = "g.brooks87",
                Name = "Gail Brooks",
                ImageUri = "https://randomuser.me/api/portraits/women/91.jpg",
                FollowerCount = 76
            });
        }

        public ISnippet GetSnippet(Guid id)
        {
            if (!Snippets.ContainsKey(id))
                return null;

            return Snippets[id];
        }

        public object GetModel(string model, Guid id)
        {
            switch (model)
            {
                case "Demo.Models.User":
                    return Users[id];

                default:
                    throw new ArgumentException($"Model {model} is not implemented in ITbdApplication.GetModel");
            }
        }

        public IEnumerable<object> GetModels(string model, string filterString)
        {
            // Until we can properly store a filtering expression as a string, we can just treat
            // the filter string as a key and have users implement the filter in their code.

            switch (model)
            {
                case "Demo.Models.User" when string.IsNullOrWhiteSpace(filterString):
                    return Users.Values;

                case "Demo.Models.User" when filterString == "FollowerCount >= 100":
                    return Users.Values.Where(u => u.FollowerCount >= 100);

                default:
                    throw new ArgumentException($"{model} \"{filterString}\" is not implemented in ITbdApplication.GetModels");
            }
        }

        public Func<object, string> GetModelKeySelector(string model)
        {
            switch (model)
            {
                case "Demo.Models.User":
                    return new Func<object, string>(u => ((User) u).Id.ToString("D"));

                default:
                    throw new ArgumentException($"Model {model} is not implemented in ITbdApplication.GetModels");
            }
        }

        public string GetAssemblyQualification()
        {
            Type type = typeof(App);
            return type.AssemblyQualifiedName.Replace(type.FullName, "");
        }

        public void Print(LayoutItemTappedArgs args)
        {
            Debug.WriteLine($"Print: {args.SnippetLayoutItem.Parameter}");
        }

        public void NavigateToAnotherPage(LayoutItemTappedArgs args)
        {
            Debug.WriteLine("This method could navigate to a different page. A more in depth demo can show the possibilities");
        }
    }
}
