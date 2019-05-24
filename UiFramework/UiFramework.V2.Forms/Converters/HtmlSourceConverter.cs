using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using UiFramework.V2.Interfaces;
using Xamarin.Forms;
using UiFramework.V2.Forms.Models;
using UiFramework.V2.Forms.Interfaces;

namespace UiFramework.V2.Forms.Converters
{
    public class HtmlSourceConverter : IValueConverter
    {
        //
        // Properties
        //

        public ISnippetSelector Snippets { get; set; }

        public IAssemblySelector Assemblies { get; set; }

        public IModelSelector Models { get; set; }

        public BindableObject View { get; set; }

        //
        // Interface methods
        //

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LayoutItem item = value as LayoutItem;
            if (item == null)
                return value;
            
            switch (item.ParameterType)
            {
                case Enums.Parameter.Single:
                    return GetSingleSnippet(item);

                case Enums.Parameter.Many:
                    return GetManySnippets(item);

                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //
        // Private methods
        //

        private HtmlWebViewSource GetSingleSnippet(LayoutItem item)
        {
            // Fetch the snippet from whatever datastore it is stored in.
            ISnippet snippet = Snippets.Select(item.SnippetId);
            if (snippet == null)
                throw new ArgumentNullException(nameof(snippet), $"Snippet {item.SnippetId:D}");

            string html = snippet.Html;

            try
            {
                if (string.IsNullOrWhiteSpace(item.Parameter) && item.ParameterValue == null)
                    throw new IgnoredException();
                if (string.IsNullOrWhiteSpace(item.ParameterModel))
                    throw new IgnoredException();

                Assembly parameterModelAssembly = Assemblies.Select(item.ParameterModel);

                // Fetch the class being bound to this element
                Type parameterType = Type.GetType($"{item.ParameterModel}, {parameterModelAssembly.ToString()}", true);
                if (parameterType == null)
                    throw new ArgumentNullException(nameof(parameterType), $"Type {item.ParameterModel}");

                // Fetch the instance being bound to this element
                // If this LayoutItem has already been through the GetManySnippets then the parameterValue is already stored in item.ParameterValue 
                object parameterValue = item.ParameterValue == null
                    ? GetParameterValue(item.ParameterModel, item.Parameter, out bool isFromContext)
                    : item.ParameterValue;
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

        private IEnumerable<ILayoutItem> GetManySnippets(ILayoutItem item)
        {
            IList<LayoutItem> list = null;

            try
            {
                if (string.IsNullOrWhiteSpace(item.ParameterModel))
                    throw new IgnoredException();

                Assembly parameterModelAssembly = Assemblies.Select(item.ParameterModel);

                // Fetch the class being bound to this list
                Type parameterType = Type.GetType($"{item.ParameterModel}, {parameterModelAssembly.ToString()}", true);
                if (parameterType == null)
                    throw new ArgumentNullException(nameof(parameterType), $"Type {item.ParameterModel}");

                // Fetch the instances being bound to this list
                IEnumerable<object> parameterValues = GetParametersValues(item.ParameterModel, item.Parameter, out bool isFromContext);
                if (parameterValues == null)
                    throw new ArgumentNullException(nameof(parameterValues));

                Func<object, string> selectKeyFrom = Models.GetKeySelector(item.ParameterModel);
                list = parameterValues.Select(parameterValue => new LayoutItem
                {
                    Id = item.Id,
                    LayoutId = item.LayoutId,
                    SnippetId = item.SnippetId,
                    ParameterModel = item.ParameterModel,
                    ParameterType = Enums.Parameter.Single,
                    //Parameter = isFromContext ? null : selectKeyFrom(parameterValue),
                    Parameter = selectKeyFrom(parameterValue),
                    ParameterValue = isFromContext ? parameterValue : null,
                    OnTappedMethodName = item.OnTappedMethodName,
                }).ToList();
            }
            catch (IgnoredException)
            {
                list = new List<LayoutItem>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                list = new List<LayoutItem>();
            }

            return list;
        }

        private object GetParameterValue(string model, string parameter, out bool isFromContext)
        {
            object value = null;

            // Try to find the property in the binding context first
            PropertyInfo property = null;
            if (View.BindingContext != null)
            {
                Type bindingContextType = View.BindingContext.GetType();
                property = bindingContextType.GetProperty(parameter);
            }

            // If the binding context isn't null and the property exists, fetch the value of the property
            if (property != null)
            {
                value = property.GetValue(View.BindingContext);
                isFromContext = true;
                return value;
            }
            else if (Guid.TryParse(parameter, out Guid id))
            {
                // If the binding context is null or the property doesn't exist, and the parameter is an id, fetch the value from the IModelSelector
                value = Models.SelectSingle(model, id);
            }
                
            isFromContext = false;
            return value;
        }

        private IEnumerable<object> GetParametersValues(string model, string filter, out bool isFromContext)
        {
            IEnumerable<object> value = null;

            // Try to find the property in the binding context first
            PropertyInfo property = null;
            if (View.BindingContext != null)
            {
                Type bindingContextType = View.BindingContext.GetType();
                property = bindingContextType.GetProperty(filter);
            }

            // If the binding context isn't null and the property exists, fetch the value of the property
            if (property != null)
            {
                value = property.GetValue(View.BindingContext) as IEnumerable<object>;
                isFromContext = true;
                return value;
            }
            else if (!string.IsNullOrWhiteSpace(filter))
            {
                // If the binding context is null or the property doesn't exist, and the filter is set, fetch the value from the IModelSelector
                value = Models.SelectMany(model, filter);
            }
            
            isFromContext = false;
            return value;
        }

        //private IEnumerable<object> Get(IEnumerable<object> models, Type modelType, string filterString)
        //{
        //    try
        //    {
        //        IList<Func<object, bool>> actions = new List<Func<object, bool>>();
        //        if (!string.IsNullOrWhiteSpace(filterString))
        //        {
        //            //"FollowerCount >= 100|FollowerCount <= 1000|UserName == \"A username\""
        //
        //            string[] filters = filterString.Split(new[] { '|' });
        //            foreach (string filter in filters)
        //            {
        //                string[] filterParts = filter.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //                if (filterParts.Length < 3)
        //                    continue;
        //
        //                string left = filterParts[0].Trim();
        //                string operation = filterParts[1].Trim();
        //                string right = filterParts.Skip(2).Aggregate("", (a, b) => $"{a} {b}").Trim();
        //
        //                if (string.IsNullOrWhiteSpace(left))
        //                    continue;
        //                if (string.IsNullOrWhiteSpace(operation))
        //                    continue;
        //                if (string.IsNullOrWhiteSpace(right))
        //                    continue;
        //
        //                PropertyInfo property = modelType.GetProperty(left);
        //                object rightValue = System.Convert.ChangeType(right, property.PropertyType);
        //
        //                actions.Add(model =>
        //                {
        //                    object leftValue = property.GetValue(model);
        //
        //                    switch (operation)
        //                    {
        //                        case "==": return leftValue == rightValue;
        //                        case ">=": return leftValue >= rightValue;        // These are a problem
        //                        case "<=": return leftValue <= rightValue;        // These are a problem
        //                        case ">":  return leftValue >  rightValue;        // These are a problem
        //                        case "<":  return leftValue <  rightValue;        // These are a problem
        //                        default:   throw new NotImplementedException($"Operation {operation} is not implemented");
        //                    }
        //                });
        //            }
        //        }
        //    
        //        return models.Where(model =>
        //        {
        //            if (actions == null || actions.Count < 1)
        //                return true;
        //    
        //            foreach (var action in actions)
        //            {
        //                if (!action(model))
        //                    return false;
        //            }
        //
        //            return true;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        throw new Exception("Filter parse failed", ex);
        //    }
        //}

        private class IgnoredException : Exception
        {
        }
    }
}
