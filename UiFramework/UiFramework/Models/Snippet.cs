using System;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.Models
{
    public class Snippet : ISnippet
    {
        public Guid Id { get; set; }

        // public IDictionary<string, string> Metadata { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }
    }
}
