using System;
using System.Linq;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace UiFramework.V2.Forms.Controls
{
    public class StackedListView : StackLayout
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            nameof(Items),
            typeof(IEnumerable),
            typeof(StackedListView),
            propertyChanged: ItemsPropertyChanged
        );

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(StackedListView),
            propertyChanged: ItemsPropertyChanged
        );

        public IEnumerable Items
        {
            get => (IEnumerable)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public int PageSize { get; set; } = 0;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Reset(this);
        }

        private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue is INotifyCollectionChanged oldObservable)
                oldObservable.CollectionChanged -= ObservableOnCollectionChanged;
            if (newValue is INotifyCollectionChanged newObservable)
                newObservable.CollectionChanged += ObservableOnCollectionChanged;

            if (bindable is StackedListView instance)
                Reset(instance);

            void ObservableOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Debug.WriteLine($"{e.Action:G}");
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:             // OldItems: null,			NewItems: added items,	OldIndex: -1,		NewIndex: index
                        for (int i = 0; i < e.NewItems.Count; ++i)
                            Add(bindable as StackedListView, e.NewItems[i], e.NewStartingIndex);
                        break;

                    case NotifyCollectionChangedAction.Remove:          // OldItems: removed items,	NewItems: null,			OldIndex: index,	NewIndex: -1
                        for (int i = 0; i < e.OldItems.Count; ++i)
                            Remove(bindable as StackedListView, e.OldItems[i], e.OldStartingIndex);
                        break;

                    case NotifyCollectionChangedAction.Move:            // OldItems: moved items,	NewItems: moved items,	OldIndex: oldIndex,	NewIndex: newIndex
                        Move(bindable as StackedListView, e.OldStartingIndex, e.NewStartingIndex);
                        break;

                    case NotifyCollectionChangedAction.Replace:         // OldItems: old items,		NewItems: new items,	OldIndex: index,	NewIndex: index
                        for (int i = 0; i < e.NewItems.Count; ++i)
                            Replace(bindable as StackedListView, e.NewItems[i], e.NewStartingIndex);
                        break;

                    case NotifyCollectionChangedAction.Reset:           // OldItems: null,			NewItems: null,			OldIndex: -1,		NewIndex: -1
                        Reset(bindable as StackedListView);
                        break;
                }
            }
        }

        private static void Add(StackedListView instance, object item, int index)
        {
            try
            {
                instance.DisableLayout = true;
                if (instance.Items == null)
                    return;

                DataTemplate template = instance.ItemTemplate;
                if (template is DataTemplateSelector selector)
                    template = selector.SelectTemplate(item, null);

                View view = null;
                object bindingContext = null;
                if (template == null)
                {
                    // Can expose some bindable properties for the default label view
                    view = new Label();
                    view.SetBinding(Label.TextProperty, ".");
                    bindingContext = item;
                }
                else
                {
                    ViewCell cell = template.CreateContent() as ViewCell;
                    view = cell.View;
                    bindingContext = item;
                }

                if (index > -1)
                    instance.Children.Insert(index, view);
                else
                    instance.Children.Add(view);
                view.BindingContext = bindingContext;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                instance.DisableLayout = false;
                instance.ForceLayout();
            }
        }

        private static void Remove(StackedListView instance, object item, int index)
        {
            try
            {
                instance.DisableLayout = true;
                if (instance.Items == null)
                    return;

                if (index < 0)
                {
                    View existing = instance.Children.FirstOrDefault(i => i.BindingContext?.Equals(item) ?? false);
                    index = instance.Children.IndexOf(existing);
                }

                if (index < 0)
                    throw new IndexOutOfRangeException();

                instance.Children.RemoveAt(index);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                instance.DisableLayout = false;
                instance.ForceLayout();
            }
        }

        private static void Move(StackedListView instance, int from, int to)
        {
            try
            {
                instance.DisableLayout = true;
                if (instance.Items == null)
                    return;

                View view = instance.Children[from];
                instance.Children.RemoveAt(from);
                instance.Children.Insert(to, view);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                instance.DisableLayout = false;
                instance.ForceLayout();
            }
        }

        private static void Replace(StackedListView instance, object item, int index)
        {
            try
            {
                instance.DisableLayout = true;
                if (instance.Items == null)
                    return;

                if (index < 0)
                {
                    View existing = instance.Children.FirstOrDefault(i => i.BindingContext?.Equals(item) ?? false);
                    index = instance.Children.IndexOf(existing);
                }

                if (index < 0)
                    throw new IndexOutOfRangeException();

                DataTemplate template = instance.ItemTemplate;
                if (template is DataTemplateSelector selector)
                    template = selector.SelectTemplate(item, null);

                View view = null;
                object bindingContext = null;
                if (template == null)
                {
                    // Can expose some bindable properties for the default label view
                    view = new Label();
                    view.SetBinding(Label.TextProperty, ".");
                    bindingContext = item;
                }
                else
                {
                    ViewCell cell = template.CreateContent() as ViewCell;
                    view = cell.View;
                    bindingContext = item;
                }

                instance.Children[index] = view;
                view.BindingContext = bindingContext;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                instance.DisableLayout = false;
                instance.ForceLayout();
            }
        }

        private static void Reset(StackedListView instance)
        {
            try
            {
                instance.DisableLayout = true;
                if (instance.Items == null)
                    return;

                IList<(View, object)> views = new List<(View, object)>();
                foreach (object item in instance.Items)
                {
                    if (item == null)
                        continue;

                    DataTemplate template = instance.ItemTemplate;
                    if (template is DataTemplateSelector selector)
                        template = selector.SelectTemplate(item, null);

                    if (template == null)
                    {
                        // Can expose some bindable properties for the default label view
                        Label view = new Label();
                        view.SetBinding(Label.TextProperty, ".");
                        views.Add((view, item));
                    }
                    else
                    {
                        ViewCell cell = template.CreateContent() as ViewCell;
                        if (cell != null)
                        {
                            // We don't want to use the converter if it's a split element, so the SplitElementTemplate
                            // has the ListingViewTypeSelector IsSplit attachable property set on the ViewCell, to true
                            views.Add((cell.View, item));
                        }
                    }
                }

                instance.Children.Clear();
                foreach ((View view, object context) in views)
                {
                    instance.Children.Add(view);
                    view.BindingContext = context;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                instance.DisableLayout = false;
                instance.ForceLayout();
            }
        }
    }
}