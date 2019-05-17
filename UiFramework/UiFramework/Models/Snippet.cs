using System;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Models
{
    /// <summary>
    /// These models are only here for convenience. It is not suggested that these are used within the app, as they do not implement INotifyPropertyChanged.
    /// <para />
    /// A snippet of reusable html that is used in each element of the grid.
    /// <para />
    /// <seealso cref="ISnippet"/>
    /// </summary>
    public class Snippet : ISnippet
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
        /// <seealso cref="ISnippet.Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// More descriptive information of this layout.
        /// <para />
        /// <seealso cref="ISnippet.Description"/>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The html (and css/js) markup. Parameters can be inserted into the code by surrounding it with curly brackets.
        /// <para />
        /// Insert the actual parameter object: <code>&lt;img src="{.}"&gt;</code>
        /// <para />
        /// Insert a property of the parameter object: <code>&lt;p&gt;{Username} has accepted your friend request!&lt;/p&gt;</code>
        /// <para />
        /// <seealso cref="ISnippet.Html"/>
        /// </summary>
        public string Html { get; set; }
    }
}
