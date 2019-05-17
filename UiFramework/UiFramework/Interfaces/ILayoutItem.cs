using System;
using UiFramework.V2.Enums;

namespace UiFramework.V2.Interfaces
{
    public interface ILayoutItem : IBase
    {
        Guid LayoutId { get; set; }

        Guid SnippetId { get; set; }

        string ParameterModel { get; set; }

        Parameter ParameterType { get; set; }

        string Parameter { get; set; }

        string OnTappedMethodName { get; set; }

        int Column { get; set; }

        int Row { get; set; }

        int ColumnSpan { get; set; }

        int RowSpan { get; set; }
    }
}
