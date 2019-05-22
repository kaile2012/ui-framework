using System;
using UiFramework.V2.Enums;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Models
{
    /// <summary>
    /// These models are only here for convenience. It is not suggested that these are used within the app, as they do not implement INotifyPropertyChanged.
    /// <para />
    /// Each element of the grid that loads parameters into a snippet
    /// <para />
    /// <seealso cref="ILayoutItem"/>
    /// </summary>
    public class LayoutItem : ILayoutItem
    {
        /// <summary>
        /// The primary key.
        /// <para />
        /// <seealso cref="IBase"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The identifier of the layout this element is part of.
        /// <para />
        /// <seealso cref="ILayoutItem.LayoutId"/>
        /// </summary>
        public Guid LayoutId { get; set; }

        /// <summary>
        /// The identifier of the html snippet this element uses.
        /// <para />
        /// <seealso cref="ILayoutItem.SnippetId"/>
        /// </summary>
        public Guid SnippetId { get; set; }

        /// <summary>
        /// The name of the model being bound to this element. Leave this null if you are inserting the actual string in <see cref="Parameter" />.
        /// <para />
        /// Binding the User object from the demo app: <code>UiFramework.V2.DemoApp.Models.User</code>
        /// <para />
        /// <seealso cref="ILayoutItem.ParameterModel"/>
        /// </summary>
        public string ParameterModel { get; set; }

        /// <summary>
        /// The sort of item that is being bound to a snippet.
        /// <para />
        /// <seealso cref="Enums.Parameter"/>
        /// <para />
        /// <seealso cref="ILayoutItem.ParameterType"/>
        /// </summary>
        public Parameter ParameterType { get; set; }

        /// <summary>
        /// The parameter being bound to the element. If a model is being bound, this is the primary key identifier.
        /// <para />
        /// <seealso cref="ILayoutItem.Parameter"/>
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        /// The name of the method to call when tapping on this item.
        /// <para />
        /// <seealso cref="ILayoutItem.OnTappedMethodName"/>
        /// </summary>
        public string OnTappedMethodName { get; set; }

        /// <summary>
        /// The x position to place this element in.
        /// <para />
        /// <seealso cref="ILayoutItem.Column"/>
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// The y position to place this element in.
        /// <para />
        /// <seealso cref="ILayoutItem.Row"/>
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The number of columns to span this element across.
        /// <para />
        /// <seealso cref="ILayoutItem.ColumnSpan"/>
        /// </summary>
        public int ColumnSpan { get; set; }

        /// <summary>
        /// The number of rows to span this element across.
        /// <para />
        /// <seealso cref="ILayoutItem.RowSpan"/>
        /// </summary>
        public int RowSpan { get; set; }
    }
}
