using System;
using System.Diagnostics;
using Android.Webkit;
using Xamarin.Forms;
using UiFramework.V2.Forms.Controls;
using UiFramework.V2.Android.Renderers;

[assembly: ExportRenderer(typeof(SnippetWebView), typeof(SnippetWebViewRenderer))]
namespace UiFramework.V2.Android.Renderers
{
    public class SnippetWebViewRenderer : WebViewClient
    {
        public static void Initialise()
        {
            Debug.WriteLine("SnippetWebViewRenderer initialised");
        }
    }
}
