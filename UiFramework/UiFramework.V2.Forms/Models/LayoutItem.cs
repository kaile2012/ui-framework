using System;
using MvvmHelpers;
using UiFramework.V2.Enums;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms.Models
{
    /// <summary>
    /// These models are only here for convenience.
    /// <para />
    /// This model is observable for use in apps. Use UiFramework.V2.Models.LayoutItem if you don't require INotifyPropertyChanged.
    /// <para />
    /// Each element of the grid that loads parameters into a snippet
    /// <para />
    /// <seealso cref="ILayoutItem"/>
    /// </summary>
    public class LayoutItem : ObservableObject, ILayoutItem
    {
        private Guid _id;
        private Guid _layoutId;
        private Guid _snippetId;
        private string _parameterModel;
        private Parameter _parameterType;
        private string _parameter;
        private string _onTappedMethodName;
        private int _column;
        private int _row;
        private int _columnSpan;
        private int _rowSpan;

        /// <summary>
        /// The primary key.
        /// <para />
        /// <seealso cref="IBase"/>
        /// </summary>
        public Guid Id
		{
			get => _id;
			set => SetProperty(ref _id, value);
		}

        /// <summary>
        /// The identifier of the layout this element is part of.
        /// <para />
        /// <seealso cref="ILayoutItem.LayoutId"/>
        /// </summary>
        public Guid LayoutId
		{
			get => _layoutId;
			set => SetProperty(ref _layoutId, value);
		}

        /// <summary>
        /// The identifier of the html snippet this element uses.
        /// <para />
        /// <seealso cref="ILayoutItem.SnippetId"/>
        /// </summary>
        public Guid SnippetId
		{
			get => _snippetId;
			set => SetProperty(ref _snippetId, value);
		}

        /// <summary>
        /// The name of the model being bound to this element. Leave this null if you are inserting the actual string in <see cref="Parameter" />.
        /// <para />
        /// Binding the User object from the demo app: <code>UiFramework.V2.DemoApp.Models.User</code>
        /// <para />
        /// <seealso cref="ILayoutItem.ParameterModel"/>
        /// </summary>
        public string ParameterModel
		{
			get => _parameterModel;
			set => SetProperty(ref _parameterModel, value);
		}

        /// <summary>
        /// The sort of item that is being bound to a snippet. (<see cref="Enums.Parameter"/> may not be fully implemented.)
        /// <para />
        /// <seealso cref="Enums.Parameter"/>
        /// <para />
        /// <seealso cref="ILayoutItem.ParameterType"/>
        /// </summary>
        public Parameter ParameterType
		{
			get => _parameterType;
			set => SetProperty(ref _parameterType, value);
		}

        /// <summary>
        /// The parameter being bound to the element. If a model is being bound, this is the primary key identifier.
        /// <para />
        /// <seealso cref="ILayoutItem.Parameter"/>
        /// </summary>
        public string Parameter
		{
			get => _parameter;
			set => SetProperty(ref _parameter, value);
		}

        /// <summary>
        /// The name of the method to call when tapping on this item.
        /// <para />
        /// <seealso cref="ILayoutItem.OnTappedMethodName"/>
        /// </summary>
        public string OnTappedMethodName
		{
			get => _onTappedMethodName;
			set => SetProperty(ref _onTappedMethodName, value);
		}

        /// <summary>
        /// The x position to place this element in.
        /// <para />
        /// <seealso cref="ILayoutItem.Column"/>
        /// </summary>
        public int Column
		{
			get => _column;
			set => SetProperty(ref _column, value);
		}

        /// <summary>
        /// The y position to place this element in.
        /// <para />
        /// <seealso cref="ILayoutItem.Row"/>
        /// </summary>
        public int Row
		{
			get => _row;
			set => SetProperty(ref _row, value);
		}

        /// <summary>
        /// The number of columns to span this element across.
        /// <para />
        /// <seealso cref="ILayoutItem.ColumnSpan"/>
        /// </summary>
        public int ColumnSpan
		{
			get => _columnSpan;
			set => SetProperty(ref _columnSpan, value);
		}

        /// <summary>
        /// The number of rows to span this element across.
        /// <para />
        /// <seealso cref="ILayoutItem.RowSpan"/>
        /// </summary>
        public int RowSpan
		{
			get => _rowSpan;
			set => SetProperty(ref _rowSpan, value);
		}
    }
}
