using System;

namespace Demo.Models
{
    public class Layout : UiFramework.V2.Forms.Models.Layout
    {
        [SQLite.PrimaryKey]
        public new Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [SQLite.Ignore]
        public new UiFramework.V2.Interfaces.ILayoutItem[] Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
    }
}
