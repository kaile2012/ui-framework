using System;

namespace Demo.Models
{
    public class LayoutItemParameter : UiFramework.V2.Forms.Models.LayoutItemParameter
    {
        [SQLite.PrimaryKey]
        public new Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [SQLite.Ignore]
        public new object Reference
        {
            get => _reference;
            set => SetProperty(ref _reference, value);
        }
    }
}
