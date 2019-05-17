using System;
using System.Collections.Generic;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Models
{
    public class Layout : ILayout
    {
        public Guid Id { get; set; }

        // public IDictionary<string, string> Metadata { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ILayoutItem[] Items { get; set; }

        public int ColumnCount { get; set; }

        public int RowCount { get; set; }
    }
}
