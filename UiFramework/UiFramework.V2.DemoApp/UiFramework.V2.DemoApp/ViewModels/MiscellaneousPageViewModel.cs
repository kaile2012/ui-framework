using System;
using Xamarin.Forms;

namespace UiFramework.V2.DemoApp.ViewModels
{
    public class MiscellaneousPageViewModel : SnippetPageViewModel
    {
        public Command Button1Command => new Command(LoadLayout1);

        public Command Button2Command => new Command(LoadLayout2);

        public Command Button3Command => new Command(LoadLayout3);

        public MiscellaneousPageViewModel()
        {
            Title = "Miscellaneous";
            LoadLayout1();
        }

        public void LoadLayout1() => Layout = App.Current.Layouts[Guid.Parse("1b946ba7-0706-4755-9a42-97572be96de2")];

        public void LoadLayout2() => Layout = App.Current.Layouts[Guid.Parse("20fe03d6-029b-4746-90ce-d00f33d71b26")];

        public void LoadLayout3() => Layout = App.Current.Layouts[Guid.Parse("306293f2-b5cd-4113-a163-34eacd7ed756")];
    }
}
