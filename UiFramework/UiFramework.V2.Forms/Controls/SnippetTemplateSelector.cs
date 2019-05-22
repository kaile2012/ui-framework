using System;
using System.Text;
using UiFramework.V2.Interfaces;
using Xamarin.Forms;

namespace UiFramework.V2.Forms.Controls
{
    public class SnippetTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Single { get; set; }

        public DataTemplate Many { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ILayoutItem layoutItem = (ILayoutItem) item;
            switch (layoutItem.ParameterType)
            {
                case Enums.Parameter.Single:
                    return Single;

                case Enums.Parameter.Many:
                    return Many;

                default:
                    throw new ArgumentOutOfRangeException(nameof(layoutItem.ParameterType));
            }
        }
    }
}
