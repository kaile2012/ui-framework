using System;
using System.Diagnostics;
using Foundation;
using UIKit;
using Demo.Interfaces;
using Demo.iOS.Services;

namespace Demo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private App _app = null;
        private IDataStore _dataStore = null;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UiFramework.V2.iOS.Renderers.SnippetWebViewRenderer.Initialise();

            _dataStore = new DataStore();

            _app = new App(_dataStore);
            LoadApplication(_app);

            return base.FinishedLaunching(app, options);
        }
    }
}
