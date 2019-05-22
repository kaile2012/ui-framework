using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
//using System.Linq.Dynamic.Core;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Xamarin.Forms;
using UiFramework.V2.Interfaces;
using UiFramework.V2.Forms.Models;

namespace UiFramework.V2.Forms.Converters
{
    public class HtmlSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ILayoutItem item = value as ILayoutItem;
            if (item == null)
                return null;

            if (!(Application.Current is ITbdApplication app))
                throw new InvalidCastException("App must implement ITbdApplication");
            
            switch (item.ParameterType)
            {
                case Enums.Parameter.Single:
                    return GetSingleSnippet(item, app);

                case Enums.Parameter.Many:
                    return GetManySnippets(item, app);

                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private HtmlWebViewSource GetSingleSnippet(ILayoutItem item, ITbdApplication app)
        {
            // Fetch the snippet from whatever datastore it is stored in.
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

                string assemblyQualification = app.GetAssemblyQualification();

                // Fetch the class being bound to this element
                Type parameterType = Type.GetType(item.ParameterModel + assemblyQualification, true);
                if (parameterType == null)
                    throw new ArgumentNullException(nameof(parameterType), $"Type {item.ParameterModel}");

                // Fetch the instance being bound to this element
                object parameterValue = app.GetModel(item.ParameterModel, Guid.Parse(item.Parameter));
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

        private IEnumerable<ILayoutItem> GetManySnippets(ILayoutItem item, ITbdApplication app)
        {
            IList<LayoutItem> list = null;

            try
            {
                if (string.IsNullOrWhiteSpace(item.ParameterModel))
                    throw new IgnoredException();

                string assemblyQualification = app.GetAssemblyQualification();

                // Fetch the class being bound to this list
                Type parameterType = Type.GetType(item.ParameterModel + assemblyQualification, true);
                if (parameterType == null)
                    throw new ArgumentNullException(nameof(parameterType), $"Type {item.ParameterModel}");

                // Fetch the instances being bound to this list
                IEnumerable<object> parameterValues = app.GetModels(item.ParameterModel, item.Parameter);
                if (parameterValues == null)
                    throw new ArgumentNullException(nameof(parameterValues));

                Func<object, string> selectKeyFrom = app.GetModelKeySelector(item.ParameterModel);
                list = parameterValues.Select(parameterValue => new LayoutItem
                {
                    Id = item.Id,
                    LayoutId = item.LayoutId,
                    SnippetId = item.SnippetId,
                    ParameterModel = item.ParameterModel,
                    ParameterType = Enums.Parameter.Single,
                    Parameter = selectKeyFrom(parameterValue),
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
