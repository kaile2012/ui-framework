using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UiFramework.V2.Forms.Models;

namespace UiFramework.V2.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SnippetView : ScrollView
    {
        public static readonly BindableProperty SnippetLayoutProperty = BindableProperty.Create(
            nameof(SnippetLayout),
            typeof(Interfaces.ILayout),
            typeof(SnippetView),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay
        );

        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
            nameof(ItemTappedCommand),
            typeof(Command<LayoutItemTappedArgs>),
            typeof(SnippetView),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay
        );

        public Interfaces.ILayout SnippetLayout
        {
            get => (Interfaces.ILayout) GetValue(SnippetLayoutProperty);
            set => SetValue(SnippetLayoutProperty, value);
        }

        public Command<LayoutItemTappedArgs> ItemTappedCommand
        {
            get => (Command<LayoutItemTappedArgs>) GetValue(ItemTappedCommandProperty);
            set => SetValue(ItemTappedCommandProperty, value);
        }

        public SnippetView()
        {
            InitializeComponent();
        }
    }
}
