using System;
using UiFramework.V1.Enums;

namespace UiFramework.V1.Models
{
    public class HtmlSnippet
    {
        public Guid Id { get; set; }

        public Guid CreatorId { get; set; }

        public string CreatorName { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }

        public ExpectedType Type { get; set; }

        public string Parameter { get; set; }
    }
}
