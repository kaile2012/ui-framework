using System;
using System.Collections.Generic;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Models
{
    /// <summary>
    /// These models are only here for convenience. It is not suggested that these are used within the app, as they do not implement INotifyPropertyChanged.
    /// <para />
    /// A layout represents each page of the application.
    /// <para />
    /// <seealso cref="ILayout"/>
    /// </summary>
    public class Layout : ILayout
    {
        /// <summary>
        /// The primary key.
        /// <para />
        /// <seealso cref="IBase"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// A short, non-unique identifier for this layout.
        /// <para />
        /// <seealso cref="ILayout.Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// More descriptive information of this layout.
        /// <para />
        /// <seealso cref="ILayout.Description"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The elements that make up this page.
        /// <para />
        /// <seealso cref="ILayout.Items"/>
        /// </summary>
        public ILayoutItem[] Items { get; set; }

        /// <summary>
        /// The number of items that can fit in this layout horizontally. This may not be needed.
        /// <para />
        /// <seealso cref="ILayout.ColumnCount"/>
        /// </summary>
        public int ColumnCount { get; set; }

        /// <summary>
        /// The number of items that can fit in this layout vertically. This may not be needed.
        /// <para />
        /// <seealso cref="ILayout.RowCount"/>
        /// </summary>
        public int RowCount { get; set; }
    }
}
