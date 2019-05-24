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
    /// Each parameter for an element.
    /// <para />
    /// <seealso cref="ILayoutItemParameter"/>
    /// </summary>
    public class LayoutItemParameter : ObservableObject, ILayoutItemParameter
    {
        protected Guid _id;
        protected Guid _layoutItemId;
        protected SourceType _type;
        protected string _model;
        protected string _value;
        protected int _flowIndex;
        protected object _reference;

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
        /// The identifier of the ILayoutItem this is a parameter for.
        /// <para />
        /// <seealso cref="ILayoutItemParameter.LayoutItemId"/>
        /// </summary>
        public Guid LayoutItemId
        {
            get => _layoutItemId;
            set => SetProperty(ref _layoutItemId, value);
        }

        /// <summary>
        /// The source of the item that is being bound to this ILayoutItem.
        /// <para />
        /// <seealso cref="SourceType"/>
        /// <para />
        /// <seealso cref="ILayoutItemParameter.Type"/>
        /// </summary>
        public SourceType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// The name of the model being bound to this element. Leave this null if you are inserting the actual contents of <see cref="Value" />.
        /// <para />
        /// Binding the User object from the demo app: <code>Demo.Models.User</code>
        /// <para />
        /// <seealso cref="ILayoutItemParameter.Model"/>
        /// </summary>
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        /// <summary>
        /// The parameter being bound to the element. If a model is being bound, this is the primary key identifier.
        /// <para />
        /// <seealso cref="ILayoutItemParameter.Value"/>
        /// </summary>
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        /// <summary>
        /// The index of the parameter. This decides whether a parameter is referenced in the snippet as <code>{0}</code>, <code>{1}</code>, <code>{2}</code> etc.
        /// <para />
        /// <seealso cref="ILayoutItemParameter.FlowIndex"/>
        /// </summary>
        public int FlowIndex
        {
            get => _flowIndex;
            set => SetProperty(ref _flowIndex, value);
        }

        /// <summary>
        /// This is the actual object being bound to the element. This is not stored in anyway, it is used internally in <see cref="Converters.HtmlSourceConverter"/>
        /// </summary>
        public object Reference
        {
            get => _reference;
            set => SetProperty(ref _reference, value);
        }
    }
}
