namespace UiFramework.V2.Interfaces
{
    /// <summary>
    /// A layout represents each page of the application.
    /// </summary>
    public interface ILayout : IBase
    {
        /// <summary>
        /// A short, non-unique identifier for this layout.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// More descriptive information of this layout.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The elements that make up this page.
        /// <para />
        /// <seealso cref="ILayout.Items"/>
        /// </summary>
        ILayoutItem[] Items { get; set; }

        /// <summary>
        /// The number of items that can fit in this layout horizontally. This may not be needed.
        /// </summary>
        int ColumnCount { get; set; }

        /// <summary>
        /// The number of items that can fit in this layout vertically. This may not be needed.
        /// </summary>
        int RowCount { get; set; }
    }
}
