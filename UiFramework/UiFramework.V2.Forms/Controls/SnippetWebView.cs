using System;
using Xamarin.Forms;

namespace UiFramework.V2.Forms.Controls
{
    public class SnippetWebView : WebView
    {
        public EventHandler VisibilityChanged;
        public EventHandler SourceChanged;

        public new HtmlWebViewSource Source
        {
            get => (HtmlWebViewSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SourceChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsVisible))
                VisibilityChanged?.Invoke(this, EventArgs.Empty);
            if (propertyName == nameof(Source))
                SourceChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}