using System;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace UiFramework.V2.Forms.Converters
{
    public class ContainerConverter : IValueConverter
    {
        public string ContainerModel { get; set; }

        public string ContainerProperty { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(ContainerModel))
                return value;
            if (string.IsNullOrWhiteSpace(ContainerProperty))
                return value;

            try
            {
                Type containerModel = Type.GetType(ContainerModel, true);
                object container = Activator.CreateInstance(containerModel);
                PropertyInfo property = containerModel.GetProperty(ContainerProperty);
                property.SetValue(container, value);

                return container;
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(ContainerModel))
                return value;
            if (string.IsNullOrWhiteSpace(ContainerProperty))
                return value;

            try
            {
                Type containerModel = Type.GetType(ContainerModel, true);
                PropertyInfo property = containerModel.GetProperty(ContainerProperty);
                object contained = property.GetValue(value);

                return contained;
            }
            catch
            {
                return value;
            }
        }
    }
}
