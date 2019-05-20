using System;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms.Converters
{
    public class HtmlSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ILayoutItem item = value as ILayoutItem;
            if (item == null)
                return null;

            // Fetch the snippet from whatever datastore it is stored in.
            if (!(Application.Current is ITbdApplication app))
                throw new InvalidCastException("App must implement ITbdApplication");
            string assemblyQualification = app.GetAssemblyQualification();
            
            ISnippet snippet = app.GetSnippet(item.SnippetId);
            if (snippet == null)
                throw new ArgumentNullException(nameof(snippet), $"Snippet {item.SnippetId:D}");

            string html = snippet.Html;

            try
            {
                if (string.IsNullOrWhiteSpace(item.Parameter))
                    throw new IgnoredException();
                if (string.IsNullOrWhiteSpace(item.ParameterModel))
                    throw new IgnoredException();

                // Fetch the class being bound to this element
                Type parameterType = Type.GetType(item.ParameterModel + assemblyQualification, true);
                if (parameterType == null)
                    throw new ArgumentNullException(nameof(parameterType), $"Type {item.ParameterModel}");

                object parameterValue = null;
                switch (item.ParameterType)
                {
                    case Enums.Parameter.Single:
                        // Fetch the instance being bound to this element
                        parameterValue = app.GetModel(item.ParameterModel, Guid.Parse(item.Parameter));
                        break;

                    case Enums.Parameter.Many:
                        throw new NotImplementedException();
                }

                if (parameterValue == null)
                    throw new ArgumentNullException(nameof(parameterValue));

                // Insert each parameter into the html
                foreach (PropertyInfo property in parameterType.GetProperties())
                    html = html.Replace($"{{{property.Name}}}", property.GetValue(parameterValue)?.ToString());
            }
            catch (IgnoredException)
            {
                // Insert just the parameter
                if (!string.IsNullOrWhiteSpace(item.Parameter))
                    html = html.Replace("{.}", item.Parameter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

                // Resort to inserting just the parameter
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
