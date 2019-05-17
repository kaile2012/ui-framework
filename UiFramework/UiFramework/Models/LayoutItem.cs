using System;
using UiFramework.V2.Enums;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Models
{
    public class LayoutItem : ILayoutItem
    {
        public Guid Id { get; set; }

        // public IDictionary<string, string> Metadata { get; set; }

        public Guid LayoutId { get; set; }

        public Guid SnippetId { get; set; }

        public string ParameterModel { get; set; }      // The name of the model table or null

        public Parameter ParameterType { get; set; }    // The type of parameter

        public string Parameter { get; set; }           // If ParameterModel is null, the parameter, otherwise the selector (i.e. primary key, filter string)

        public string OnTappedMethodName { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        public int ColumnSpan { get; set; }

        public int RowSpan { get; set; }
    }
}
