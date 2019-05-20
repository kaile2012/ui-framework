using System;
using MvvmHelpers;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms.Models
{
    /// <summary>
    /// These models are only here for convenience.
    /// <para />
    /// This model is observable for use in apps. Use UiFramework.V2.Models.Snippet if you don't require INotifyPropertyChanged.
    /// <para />
    /// A snippet of reusable html that is used in each element of the grid.
    /// <para />
    /// <seealso cref="ISnippet"/>
    /// </summary>
    public class Snippet : ObservableObject, ISnippet
    {
        private Guid _id;
        private string _name;
        private string _description;
        private string _html;

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
        /// <seealso cref="ISnippet.Name"/>
        /// </summary>
        public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

        /// <summary>
        /// More descriptive information of this layout.
        /// <para />
        /// <seealso cref="ISnippet.Description"/>
        /// </summary>
        public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

        /// <summary>
        /// The html (and css/js) markup. Parameters can be inserted into the code by surrounding it with curly brackets.
        /// <para />
        /// Insert the actual parameter object: <code>&lt;img src="{.}"&gt;</code>
        /// <para />
        /// Insert a property of the parameter object: <code>&lt;p&gt;{Username} has accepted your friend request!&lt;/p&gt;</code>
        /// <para />
        /// <seealso cref="ISnippet.Html"/>
        /// </summary>
        public string Html
		{
			get => _html;
			set => SetProperty(ref _html, value);
		}
    }
}
