using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using UiFramework.V2.Interfaces;
using System.Reflection;

namespace UiFramework.V2.DemoApp.Converters
{
    public class HtmlSourceConverter : IValueConverter
    {
        private const string ParameterModelNamespace = "UiFramework.V2.DemoApp.Models";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ILayoutItem item = value as ILayoutItem;
            if (item == null)
                return null;

            ISnippet snippet = App.Current.Snippets[item.SnippetId];
            if (snippet == null)
                throw new ArgumentNullException(nameof(snippet), $"Snippet {item.SnippetId:D}");

            string html = snippet.Html;

            try
            {
                if (string.IsNullOrWhiteSpace(item.Parameter))
                    throw new IgnoredException();
                if (string.IsNullOrWhiteSpace(item.ParameterModel))
                    throw new IgnoredException();

                //Type parameterType = Type.GetType($"{ParameterModelNamespace}.{item.ParameterModel}");
                Type parameterType = Type.GetType(item.ParameterModel, true);
                if (parameterType == null)
                    throw new ArgumentNullException(nameof(parameterType), $"Type {item.ParameterModel}");

                object parameterValue = null;
                switch (item.ParameterType)
                {
                    case Enums.Parameter.Single:
                        parameterValue = App.Current.Models[Guid.Parse(item.Parameter)];
                        break;

                    case Enums.Parameter.Many:
                        throw new NotImplementedException();

                    case Enums.Parameter.Random:
                        throw new NotImplementedException();
                }

                if (parameterValue == null)
                    throw new ArgumentNullException(nameof(parameterValue));

                foreach (PropertyInfo property in parameterType.GetProperties())
                    html = html.Replace($"{{{property.Name}}}", property.GetValue(parameterValue)?.ToString());
            }
            catch (IgnoredException)
            {
                if (!string.IsNullOrWhiteSpace(item.Parameter))
                    html = html.Replace("{.}", item.Parameter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

                if (!string.IsNullOrWhiteSpace(item.Parameter))
                    html = html.Replace("{.}", item.Parameter);
            }

            if (string.IsNullOrWhiteSpace(html))
                return null;

            return new HtmlWebViewSource { Html = html };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private class IgnoredException : Exception
        {
        }
    }
}
