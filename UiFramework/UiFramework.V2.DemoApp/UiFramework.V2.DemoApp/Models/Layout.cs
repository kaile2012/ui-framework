using System;
using MvvmHelpers;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.DemoApp.Models
{
    public class Layout : ObservableObject, ILayout
    {
        private Guid            _id;
        private string          _name;
        private string          _description;
        private ILayoutItem[]   _items;
        private int             _columnCount;
        private int             _rowCount;

        public Guid Id
		{
			get => _id;
			set => SetProperty(ref _id, value);
		}

        public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

        public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

        public ILayoutItem[] Items
		{
			get => _items;
			set => SetProperty(ref _items, value);
		}

        public int ColumnCount
		{
			get => _columnCount;
			set => SetProperty(ref _columnCount, value);
		}

        public int RowCount
		{
			get => _rowCount;
			set => SetProperty(ref _rowCount, value);
		}
    }
}
