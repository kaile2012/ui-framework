using System;
using MvvmHelpers;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms.Models
{
    /// <summary>
    /// A layout represents each page of the application.
    /// <para />
    /// This model is observable for use in apps.
    /// <para />
    /// <seealso cref="ILayout"/>
    /// </summary>
    public class Layout : ObservableObject, ILayout
    {
        protected Guid _id;
        protected string _name;
        protected string _description;
        protected ILayoutItem[] _items;
        protected int _columnCount;
        protected int _rowCount;
        
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
        /// A short, non-unique identifier for this layout.
        /// <para />
        /// <seealso cref="ILayout.Name"/>
        /// </summary>
        public string Name 
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

        /// <summary>
        /// More descriptive information of this layout.
        /// <para />
        /// <seealso cref="ILayout.Description"/>
        /// </summary>
        public string Description 
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

        /// <summary>
        /// The elements that make up this page.
        /// <para />
        /// <seealso cref="ILayout.Items"/>
        /// </summary>
        public ILayoutItem[] Items 
		{
			get => _items;
			set => SetProperty(ref _items, value);
		}

        /// <summary>
        /// The number of items that can fit in this layout horizontally. This may not be needed.
        /// <para />
        /// <seealso cref="ILayout.ColumnCount"/>
        /// </summary>
        public int ColumnCount 
		{
			get => _columnCount;
			set => SetProperty(ref _columnCount, value);
		}

        /// <summary>
        /// The number of items that can fit in this layout vertically. This may not be needed.
        /// <para />
        /// <seealso cref="ILayout.RowCount"/>
        /// </summary>
        public int RowCount 
		{
			get => _rowCount;
			set => SetProperty(ref _rowCount, value);
		}
    }
}
