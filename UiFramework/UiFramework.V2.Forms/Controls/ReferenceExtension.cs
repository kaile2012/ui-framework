using System;
using System.Reflection;

namespace UiFramework.V2.Forms.Controls
{
    [Xamarin.Forms.ContentProperty("Property")]
    public class PropertyExtension : Xamarin.Forms.Xaml.IMarkupExtension
    {
        public object Source { get; set; }

        public string Property { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;
            if (string.IsNullOrWhiteSpace(Property))
                return Source;

            Type type = Source.GetType();
            PropertyInfo property = type.GetProperty(Property);
            if (property == null)
                return Source;

            object value = property.GetValue(Source);
            return value;
        }
    }
}
