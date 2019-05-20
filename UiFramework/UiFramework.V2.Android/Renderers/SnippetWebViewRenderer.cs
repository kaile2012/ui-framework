using System;
using UiFramework.V2.Android;
using UiFramework.V2.Forms.Controls;
using Xamarin.Forms;
using Android.Webkit;
using Android.Graphics;

[assembly: ExportRenderer(typeof(SnippetWebView), typeof(SnippetWebViewRenderer))]
namespace UiFramework.V2.Android
{
    public class SnippetWebViewRenderer : WebViewClient
    {
    }
}
