namespace UiFramework.V2.Interfaces
{
    /// <summary>
    /// A snippet of reusable html that is used in each element of the grid.
    /// </summary>
    public interface ISnippet : IBase
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
        /// The html (and css/js) markup. Parameters can be inserted into the code by surrounding it with curly brackets.
        /// <para />
        /// Insert the actual parameter object: <code>&lt;img src="{.}"&gt;</code>
        /// <para />
        /// Insert a property of the parameter object: <code>&lt;p&gt;{Username} has accepted your friend request!&lt;/p&gt;</code>
        /// </summary>
        string Html { get; set; }
    }
}
