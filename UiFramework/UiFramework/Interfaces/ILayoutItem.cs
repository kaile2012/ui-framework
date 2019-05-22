using System;
using UiFramework.V2.Enums;

namespace UiFramework.V2.Interfaces
{
    /// <summary>
    /// Each element of the grid that loads parameters into a snippet
    /// </summary>
    public interface ILayoutItem : IBase
    {
        /// <summary>
        /// The identifier of the layout this element is part of.
        /// </summary>
        Guid LayoutId { get; set; }

        /// <summary>
        /// The identifier of the html snippet this element uses.
        /// </summary>
        Guid SnippetId { get; set; }

        /// <summary>
        /// The name of the model being bound to this element. Leave this null if you are inserting the actual string in <see cref="Parameter" />.
        /// <para />
        /// Binding the User object from the demo app: <code>UiFramework.V2.DemoApp.Models.User</code>
        /// </summary>
        string ParameterModel { get; set; }

        /// <summary>
        /// The sort of item that is being bound to a snippet.
        /// <para />
        /// <seealso cref="Enums.Parameter"/>
        /// </summary>
        Parameter ParameterType { get; set; }

        /// <summary>
        /// The parameter being bound to the element. If a model is being bound, this is the primary key identifier.
        /// </summary>
        string Parameter { get; set; }

        /// <summary>
        /// The name of the method to call when tapping on this item.
        /// </summary>
        string OnTappedMethodName { get; set; }

        /// <summary>
        /// The x position to place this element in.
        /// </summary>
        int Column { get; set; }

        /// <summary>
        /// The y position to place this element in.
        /// </summary>
        int Row { get; set; }

        /// <summary>
        /// The number of columns to span this element across.
        /// </summary>
        int ColumnSpan { get; set; }

        /// <summary>
        /// The number of rows to span this element across.
        /// </summary>
        int RowSpan { get; set; }
    }
}
