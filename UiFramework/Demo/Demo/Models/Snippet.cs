using System;

namespace Demo.Models
{
    public class Snippet : UiFramework.V2.Forms.Models.Snippet
    {
        [SQLite.PrimaryKey]
        public new Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}
