using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using UiFramework.V2.Enums;
using UiFramework.V2.DemoApp.Views;
using UiFramework.V2.DemoApp.Models;
using UiFramework.V2.DemoApp.ViewModels;
using Layout = UiFramework.V2.DemoApp.Models.Layout;

namespace UiFramework.V2.DemoApp
{
    public partial class App : Application
    {
        public static new App Current => (App) Application.Current;

        // Just a quick solution to store some examples
        public IDictionary<Guid, Layout> Layouts = null;
        public IDictionary<Guid, Snippet> Snippets = null;
        public IDictionary<Guid, LayoutItem> LayoutItems = null;
        public IDictionary<Guid, object> Models = null;

        public App()
        {
            InitializeComponent();
            Seed();         // This just loads some examples into the dictionaries

            MainPage = new MiscellaneousPage();

            //MainPage = new SnippetPage();
            //((SnippetPage) MainPage).BindingContext.Layout = Layouts[Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756")];
        }

        public void Seed()
        {
            Layouts = new Dictionary<Guid, Layout>();
            Snippets = new Dictionary<Guid, Snippet>();
            LayoutItems = new Dictionary<Guid, LayoutItem>();
            Models = new Dictionary<Guid, object>();

            Layouts.Add(Guid.Parse("1b946ba7-0706-4755-9a42-97572be96de2"), new Layout
            {
                Id = Guid.Parse("1b946ba7-0706-4755-9a42-97572be96de2"),
                Name = "Example 1",
                Description = "A 2 x 2 grid, with the bottom element spanning across both columns.",
                RowCount = 2,
                ColumnCount = 2,
                Items = null
            });
            Layouts.Add(Guid.Parse("20fe03d6-029b-4746-90ce-d00f33d71b26"), new Layout
            {
                Id = Guid.Parse("20fe03d6-029b-4746-90ce-d00f33d71b26"),
                Name = "Example 2",
                Description = "A 2 x 2 grid.",
                RowCount = 2,
                ColumnCount = 2,
                Items = null
            });
            Layouts.Add(Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"), new Layout
            {
                Id = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                Name = "Example 3",
                Description = "Three rows of a user model.",
                RowCount = 3,
                ColumnCount = 1,
                Items = null
            });

            Snippets.Add(Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"), new Snippet
            {
                Id = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                Name = "Text snippet",
                Description = "Just expects a string, no model.",
                Html = "<html><style>html, body { margin:0px; padding:0px; max-width:100%; min-width:100%; height:auto; } p { font-size: 22px; }</style><body><p>{.}</p></body></html>"
            });
            Snippets.Add(Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"), new Snippet
            {
                Id = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                Name = "User snippet",
                Description = "Binds the properties of a User object.",
                Html = "<html><style>html, body { margin:0px; padding:0px; max-width:100%; min-width:100%; height:auto; } p { font-size: 18px; }</style><body><img src=\"{ImageUri}\"><p>{Name} - {UserName}</p><p>{FollowerCount} followers</p></body></html>"
            });

            LayoutItems.Add(Guid.Parse("a6260c9d-392e-445e-82b7-512bd864c48a"), new LayoutItem
            {
                Id = Guid.Parse("a6260c9d-392e-445e-82b7-512bd864c48a"),
                LayoutId = Guid.Parse("1b946ba7-0706-4755-9a42-97572be96de2"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = null,
                Parameter = "This is the first item in the grid",
                OnTappedMethodName = "",
                Row = 0,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("7a112bb6-b789-44ba-9ecb-4b69670b3ae5"), new LayoutItem
            {
                Id = Guid.Parse("7a112bb6-b789-44ba-9ecb-4b69670b3ae5"),
                LayoutId = Guid.Parse("1b946ba7-0706-4755-9a42-97572be96de2"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = null,
                Parameter = "And this is the second",
                OnTappedMethodName = "",
                Row = 0,
                Column = 1,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("793ec492-b604-421b-901e-c203b1ee76b4"), new LayoutItem
            {
                Id = Guid.Parse("793ec492-b604-421b-901e-c203b1ee76b4"),
                LayoutId = Guid.Parse("1b946ba7-0706-4755-9a42-97572be96de2"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = null,
                Parameter = "There is also a third, much longer item here, that spans across two rows",
                OnTappedMethodName = "",
                Row = 1,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 2
            });

            LayoutItems.Add(Guid.Parse("4239e290-b4bf-4550-96d0-0eb768f26b65"), new LayoutItem
            {
                Id = Guid.Parse("4239e290-b4bf-4550-96d0-0eb768f26b65"),
                LayoutId = Guid.Parse("20fe03d6-029b-4746-90ce-d00f33d71b26"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = null,
                Parameter = "This is the first item in the grid",
                OnTappedMethodName = "",
                Row = 0,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("7dffa871-439b-43db-9d0f-0e9fd35a92dc"), new LayoutItem
            {
                Id = Guid.Parse("7dffa871-439b-43db-9d0f-0e9fd35a92dc"),
                LayoutId = Guid.Parse("20fe03d6-029b-4746-90ce-d00f33d71b26"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = null,
                Parameter = "And this is the second",
                OnTappedMethodName = "",
                Row = 0,
                Column = 1,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("95349f7a-2b21-4bcc-86dc-5cb8941378b9"), new LayoutItem
            {
                Id = Guid.Parse("95349f7a-2b21-4bcc-86dc-5cb8941378b9"),
                LayoutId = Guid.Parse("20fe03d6-029b-4746-90ce-d00f33d71b26"),
                SnippetId = Guid.Parse("b24ed5d3-f084-4611-90a8-7aca58ae8e63"),
                ParameterType = Parameter.Single,
                ParameterModel = null,
                Parameter = "There is also a third, much longer item here, that spans across two rows",
                OnTappedMethodName = "",
                Row = 1,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });

            LayoutItems.Add(Guid.Parse("e147e0af-4e63-433a-9685-72910209e3e5"), new LayoutItem
            {
                Id = Guid.Parse("e147e0af-4e63-433a-9685-72910209e3e5"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                ParameterType = Parameter.Single,
                ParameterModel = "UiFramework.V2.DemoApp.Models.User",
                Parameter = "51294eb9-8852-4688-bdfa-30637e267a07",
                OnTappedMethodName = "",
                Row = 0,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("d0fdbbc7-c405-483b-bbd9-6edbc1db837f"), new LayoutItem
            {
                Id = Guid.Parse("d0fdbbc7-c405-483b-bbd9-6edbc1db837f"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                ParameterType = Parameter.Single,
                ParameterModel = "UiFramework.V2.DemoApp.Models.User",
                Parameter = "c80e7da6-405d-4840-a344-2d08fa5e5ba4",
                OnTappedMethodName = "",
                Row = 1,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });
            LayoutItems.Add(Guid.Parse("db476a6e-4630-423e-9fc1-d02f2531d581"), new LayoutItem
            {
                Id = Guid.Parse("db476a6e-4630-423e-9fc1-d02f2531d581"),
                LayoutId = Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756"),
                SnippetId = Guid.Parse("5e6064f7-0480-4b3b-9be1-f6952834fe09"),
                ParameterType = Parameter.Single,
                ParameterModel = "UiFramework.V2.DemoApp.Models.User",
                Parameter = "7ee559fa-aa91-4fe2-8d55-91388dc75746",
                OnTappedMethodName = "",
                Row = 2,
                Column = 0,
                RowSpan = 1,
                ColumnSpan = 1
            });

            foreach (var layout in Layouts)
                layout.Value.Items = LayoutItems.Values.Where(li => li.LayoutId == layout.Key).ToArray();

            Models.Add(Guid.Parse("51294eb9-8852-4688-bdfa-30637e267a07"), new User
            {
                UserName = "bloggs",
                Name = "Joe Bloggs",
                ImageUri = "https://randomuser.me/api/portraits/men/65.jpg",
                FollowerCount = 550
            });
            Models.Add(Guid.Parse("c80e7da6-405d-4840-a344-2d08fa5e5ba4"), new User
            {
                UserName = "michelle.thompson41",
                Name = "Michelle Thompson",
                ImageUri = "https://randomuser.me/api/portraits/women/21.jpg",
                FollowerCount = 827
            });
            Models.Add(Guid.Parse("7ee559fa-aa91-4fe2-8d55-91388dc75746"), new User
            {
                UserName = "g.brooks87",
                Name = "Gail Brooks",
                ImageUri = "https://randomuser.me/api/portraits/women/91.jpg",
                FollowerCount = 76
            });
        }
    }
}
