using System;

namespace UiFramework.V1.Models
{
    public class PageLayoutMapping
    {
        public Guid Id { get; set; }

        public Guid CreatorId { get; set; }

        public string CreatorName { get; set; }

        public string PageName { get; set; }

        public Guid LayoutId { get; set; }
    }
}
