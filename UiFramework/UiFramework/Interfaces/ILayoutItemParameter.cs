using System;
using UiFramework.V2.Enums;

namespace UiFramework.V2.Interfaces
{
    /// <summary>
    /// Each parameter for an element.
    /// </summary>
    public interface ILayoutItemParameter : IBase
    {
        /// <summary>
        /// The identifier of the ILayoutItem this is a parameter for.
        /// </summary>
        Guid LayoutItemId { get; set; }

        /// <summary>
        /// The source of the item that is being bound to this ILayoutItem.
        /// <para />
        /// <seealso cref="SourceType"/>
        /// </summary>
        SourceType Type { get; set; }
        
        /// <summary>
        /// The name of the model being bound to this element. Leave this null if you are inserting the actual contents of <see cref="Value" />.
        /// <para />
        /// Binding the User object from the demo app: <code>Demo.Models.User</code>
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// The parameter being bound to the element. If a model is being bound, this is the primary key identifier.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// The index of the parameter. This decides whether a parameter is referenced in the snippet as <code>{0}</code>, <code>{1}</code>, <code>{2}</code> etc.
        /// </summary>
        int FlowIndex { get; set; }
    }
}
