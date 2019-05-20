using System;
using System.Collections;
using System.Diagnostics;
using Xamarin.Forms;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Forms.Controls
{
    public class DynamicGridView : Grid
    {
        public static readonly BindableProperty RowsProperty = BindableProperty.Create(
            nameof(Rows),
            typeof(int),
            typeof(DynamicGridView),
            1,
            BindingMode.OneWay,
            propertyChanged: RowsPropertyChanged
        );

        public static readonly BindableProperty ColumnsProperty = BindableProperty.Create(
            nameof(Columns),
            typeof(int),
            typeof(DynamicGridView),
            1,
            BindingMode.OneWay,
            propertyChanged: ColumnsPropertyChanged
        );

        public static readonly BindableProperty RowsLengthProperty = BindableProperty.Create(
            nameof(RowsLength),
            typeof(GridLength),
            typeof(DynamicGridView),
            GridLength.Auto,
            BindingMode.OneWay,
            propertyChanged: RowsLengthPropertyChanged
        );

        public static readonly BindableProperty ColumnsLengthProperty = BindableProperty.Create(
            nameof(ColumnsLength),
            typeof(GridLength),
            typeof(DynamicGridView),
            GridLength.Auto,
            BindingMode.OneWay,
            propertyChanged: ColumnsLengthPropertyChanged
        );

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            nameof(Items),
            typeof(IEnumerable),
            typeof(DynamicGridView),
            null,
            BindingMode.OneWay,
            propertyChanged: ItemsChanged
        );

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(DynamicGridView),
            propertyChanged: ItemTemplatePropertyChanged
        );

        public int Rows
        {
            get => (int)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public int Columns
        {
            get => (int)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        public GridLength RowsLength
        {
            get => (GridLength)GetValue(RowsLengthProperty);
            set => SetValue(RowsLengthProperty, value);
        }

        public GridLength ColumnsLength
        {
            get => (GridLength)GetValue(ColumnsLengthProperty);
            set => SetValue(ColumnsLengthProperty, value);
        }

        public object[] Items
        {
            get => (object[])GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Children == null || Children.Count < 1)
                return;
            if (Items == null || Items.Length < 1)
                return;

            foreach (object item in Items)
            {
                try
                {
                    ILayoutItem layoutItem = item as ILayoutItem;
                    if (layoutItem == null)
                        continue;

                    View child = GetItemAt(layoutItem.Row, layoutItem.Column);
                    if (child == null)
                        continue;

                    child.BindingContext = layoutItem;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            // foreach (View child in Children)
            // {
            //     if (child == null)
            //         continue;
            // 
            //     try
            //     {
            //         int x = Grid.GetColumn(child);
            //         int y = Grid.GetRow(child);
            //         
            //         child.BindingContext = Items[y, x];
            //     }
            //     catch (Exception ex)
            //     {
            //         Debug.WriteLine(ex.ToString());
            //     }
            // }
        }

        private View GetItemAt(int row, int column)
        {
            foreach (View child in Children)
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                    return child;

            return null;
        }

        private static void RowsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DynamicGridView grid = bindable as DynamicGridView;
            if (grid == null)
                return;

            int newValue = (int)newvalue;
            if (newValue < 1)
                newValue = 1;

            grid.RowDefinitions = new RowDefinitionCollection();
            for (int i = 0; i < newValue; ++i)
                grid.RowDefinitions.Add(new RowDefinition { Height = grid.RowsLength });
        }

        private static void ColumnsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DynamicGridView grid = bindable as DynamicGridView;
            if (grid == null)
                return;

            int newValue = (int)newvalue;
            if (newValue < 1)
                newValue = 1;

            grid.ColumnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < newValue; ++i)
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = grid.ColumnsLength });
        }

        private static void RowsLengthPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DynamicGridView grid = bindable as DynamicGridView;
            if (grid == null)
                return;

            GridLength newValue = (GridLength)newvalue;
            foreach (RowDefinition definition in grid.RowDefinitions)
                definition.Height = newValue;
        }

        private static void ColumnsLengthPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DynamicGridView grid = bindable as DynamicGridView;
            if (grid == null)
                return;

            GridLength newValue = (GridLength)newvalue;
            foreach (ColumnDefinition definition in grid.ColumnDefinitions)
                definition.Width = newValue;
        }

        private static void ItemsChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DynamicGridView grid = bindable as DynamicGridView;
            if (grid == null)
                return;

            Update(grid);
        }

        private static void ItemTemplatePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            DynamicGridView grid = bindable as DynamicGridView;
            if (grid == null)
                return;

            Update(grid);
        }

        private static void Update(DynamicGridView instance)
        {
            if (instance == null)
                return;

            try
            {
                //instance.DisableLayout = true;
                instance.Children.Clear();

                if (instance.ItemTemplate == null)
                    return;
                if (instance.Items == null || instance.Items.Length < 1)
                    return;

                foreach (object item in instance.Items)
                {
                    try
                    {
                        ILayoutItem layoutItem = item as ILayoutItem;
                        if (layoutItem == null)
                            continue;

                        DataTemplate template = instance.ItemTemplate;
                        if (template is DataTemplateSelector selector)
                            template = selector.SelectTemplate(item, null);

                        ViewCell view = template.CreateContent() as ViewCell;
                        if (view?.View != null)
                        {
                            instance.Children.Add(view.View, layoutItem.Column, layoutItem.Row);
                            Grid.SetColumnSpan(view.View, layoutItem.ColumnSpan);
                            Grid.SetRowSpan(view.View, layoutItem.RowSpan);

                            view.BindingContext = layoutItem;     // The binding context gets overwritten when adding it to a layout, so need to set it again after
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }
            finally
            {
                //instance.DisableLayout = false;
                //instance.ForceLayout();
            }
        }
    }
}