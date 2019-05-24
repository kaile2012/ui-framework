using System;

namespace Demo.Models
{
    public class LayoutItem : UiFramework.V2.Forms.Models.LayoutItem
    {
        [SQLite.PrimaryKey]
        public new Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [SQLite.Ignore]
        public new object ParameterValue
        {
            get => _parameterValue;
            set => SetProperty(ref _parameterValue, value);
        }
    }
}
