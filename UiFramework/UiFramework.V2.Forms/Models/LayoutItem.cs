using System;
using MvvmHelpers;
using UiFramework.V2.Enums;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms.Models
{
    /// <summary>
    /// These models are only here for convenience.
    /// <para />
    /// This model is observable for use in apps.
    /// <para />
    /// Each element of the grid that loads parameters into a snippet
    /// <para />
    /// <seealso cref="ILayoutItem"/>
    /// </summary>
    public class LayoutItem : ObservableObject, ILayoutItem
    {
        protected Guid _id;
        protected Guid _layoutId;
        protected Guid _snippetId;
        protected ReplicationType _type;
        protected ILayoutItemParameter[] _parameters;
        protected string _onTappedMethodName;
        protected int _column;
        protected int _row;
        protected int _columnSpan;
        protected int _rowSpan;

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
        /// The sort of item that is being bound to a snippet.
        /// <para />
        /// <seealso cref="ReplicationType"/>
        /// <para />
        /// <seealso cref="ILayoutItem.Type"/>
        /// </summary>
        public ReplicationType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// The parameters that are bound to this element.
        /// <para />
        /// <seealso cref="ILayoutItemParameter"/>
        /// <para />
        /// <seealso cref="ILayoutItem.Parameters"/>
        /// </summary>
        public ILayoutItemParameter[] Parameters
        {
            get => _parameters;
            set => SetProperty(ref _parameters, value);
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
