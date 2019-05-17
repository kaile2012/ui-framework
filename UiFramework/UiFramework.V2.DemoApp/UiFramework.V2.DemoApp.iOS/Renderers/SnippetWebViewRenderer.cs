﻿using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Foundation;
using UIKit;
using UiFramework.V2.DemoApp.Controls;
using UiFramework.V2.DemoApp.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using WebKit;
using CoreGraphics;
using System.Diagnostics;
using System.Runtime.CompilerServices;

[assembly: ExportRenderer(typeof(SnippetWebView), typeof(SnippetWebViewRenderer))]
namespace UiFramework.V2.DemoApp.iOS.Renderers
{
    public class SnippetWebViewRenderer : ViewRenderer<SnippetWebView, WKWebView>, IWKNavigationDelegate, IWKScriptMessageHandler
    {
        private string _html = null;
        private IDisposable _webviewObserver = null;

        protected override void OnElementChanged(ElementChangedEventArgs<SnippetWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                string viewportScript = "var meta = document.createElement('meta');" +
                                        "meta.setAttribute('name', 'viewport');" +
                                        "meta.setAttribute('content', 'width=device-width');" +
                                        "meta.setAttribute('initial-scale', '1');" +
                                        "meta.setAttribute('maximum-scale', '1.0');" +
                                        "meta.setAttribute('minimum-scale', '1.0');" +
                                        "meta.setAttribute('user-scalable', 'no');" +
                                        "document.getElementsByTagName('head')[0].appendChild(meta);";
                string invokeScript = "window.onload = function() {" +
                                        "var height = 0;" +
                                        "for (var i = 0; i < document.body.children.length; ++i) {" +
                                        "var child = document.body.children[i];" +
                                        "child = (typeof child === 'string')" +
                                        "? document.querySelector(child)" +
                                        ": child;" +
                                        "var styles = window.getComputedStyle(child);" +
                                        "var margins = parseFloat(styles['marginTop']) + parseFloat(styles['marginBottom']);" +
                                        "height += child.offsetHeight + (styles['display'] === 'none' ? 0 : margins);" +
                                        "}" +
                                        "var log = document.createElement('meta');" +
                                        "log.setAttribute('name', 'log');" +
                                        "log.setAttribute('content', height);" +
                                        "document.head.appendChild(log);" +
                                        "window.webkit.messageHandlers.Resize.postMessage(height);" +
                                        "}";

                WKUserContentController controller = new WKUserContentController();
                controller.AddUserScript(new WKUserScript(new NSString(viewportScript), WKUserScriptInjectionTime.AtDocumentEnd, true));
                controller.AddUserScript(new WKUserScript(new NSString(invokeScript), WKUserScriptInjectionTime.AtDocumentEnd, true));
                controller.AddScriptMessageHandler(this, "Resize");

                WKWebViewConfiguration config = new WKWebViewConfiguration { UserContentController = controller };
                WKWebView webView = new WKWebView(CGRect.Null, config);
                webView.ScrollView.ScrollEnabled = false;
                webView.ScrollView.Bounces = false;
                webView.ScrollView.UserInteractionEnabled = false;

                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                Control.NavigationDelegate = null;

                e.OldElement.VisibilityChanged -= OnVisibilityChanged;
                e.OldElement.SourceChanged -= OnSourceChanged;

                _webviewObserver?.Dispose();

                UIGestureRecognizer[] gestures = Control.GestureRecognizers;
                foreach (UIGestureRecognizer gesture in gestures ?? new UIGestureRecognizer[0])
                {
                    Control.RemoveGestureRecognizer(gesture);
                    gesture.Dispose();
                }
            }

            if (e.NewElement != null)
            {
                Control.NavigationDelegate = this;

                e.NewElement.VisibilityChanged += OnVisibilityChanged;
                e.NewElement.SourceChanged += OnSourceChanged;

                _webviewObserver = Control.AddObserver("scrollView.contentSize", NSKeyValueObservingOptions.New, ContentSizeObserved);

                _html = "<html><head><style>*{height:0px;}</style></head></html>";
                if (!string.IsNullOrWhiteSpace(e?.NewElement?.Source?.Html))
                    _html = e.NewElement.Source.Html;
                Control.LoadHtmlString(_html, null);

                foreach (GestureRecognizer gesture in e.NewElement.GestureRecognizers ?? new List<IGestureRecognizer>())
                    if (gesture is TapGestureRecognizer tapGesture)
                        Control.AddGestureRecognizer(new UITapGestureRecognizer(async recogniser =>
                        {
                            if (tapGesture?.Command == null)
                                return;

                            object parameter = tapGesture.CommandParameter;

                            try
                            {
                                Debug.WriteLine($"Tapped {Element.AutomationId}");

                                CGPoint point = recogniser.LocationInView(Control);
                                NSObject result = await Control.EvaluateJavaScriptAsync($"Clicked({point.X}, {point.Y})");
                                NSString resultString = result as NSString;

                                Debug.WriteLine(resultString?.ToString());
                            }
                            catch (NSErrorException ex)
                            {
                                Debug.WriteLine(ex?.Error?.Description);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex);
                            }
                            finally
                            {
                                if (tapGesture.Command.CanExecute(parameter))
                                    tapGesture.Command.Execute(parameter);
                            }
                        }));
            }
        }

        public void DidReceiveScriptMessage(WKUserContentController controller, WKScriptMessage message)
        {
            if (message.Name.Equals("Resize"))
                if (message.Body is NSNumber height)
                    Resize(height.Int32Value);
        }

        private void ContentSizeObserved(NSObservedChange change)
        {
            if (Element == null || change == null)
                return;

            if (!(change.NewValue is NSValue value))
                return;

            if (Element.HeightRequest == value.CGSizeValue.Height && Element.MinimumHeightRequest == value.CGSizeValue.Height)
                return;

            Resize(value.CGSizeValue.Height);
        }

        private void OnVisibilityChanged(object sender, EventArgs e)
        {
            if (Control == null)
                return;

            if (Element == null)
                return;

            if (!Element.IsVisible)
                return;

            Resize();
        }

        private void OnSourceChanged(object sender, EventArgs e)
        {
            if (Control == null)
                return;

            if (Element == null)
                return;

            string html = Element?.Source?.Html;
            if (string.IsNullOrWhiteSpace(html))
                return;

            _html = html;
            Control.LoadHtmlString(html, null);
        }

        private void Resize(double? height = null, [CallerMemberName] string caller = null)
        {
            try
            {
                if (height.HasValue && height.Value <= 0)
                    return;
                //if (height.HasValue && height.Value > 4000)
                //	return;

                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        if (Element == null)
                            return;
                        if (height == null && Control == null)
                            return;

                        //ViewBase page = Element.ParentUntil<ViewBase>();
                        //Debug.WriteLine($"{caller} {page?.ViewModel?.PageName}.{AttachedProperties.GetSnippetId(Element)} {AttachedProperties.GetObjectIds(Element)}: {height}, {Control.ScrollView.ContentSize.Width} x {Control.ScrollView.ContentSize.Height}, {Control.ScrollView.Frame.Width} x {Control.ScrollView.Frame.Height}");

                        Element.HeightRequest = Element.MinimumHeightRequest = height.Value;
                        //Control.Frame = CGRect.FromLTRB(Control.Frame.Left, Control.Frame.Top, Control.Frame.Right, Control.Frame.Top + (nfloat) height.Value);
                    }
                    catch (ObjectDisposedException ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        [Export("webView:didFailNavigation:withError:")]
        public void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            Debug.WriteLine(error.ToString());
        }
    }
}