using System;
using UiFramework.V2.Forms.Models;

namespace UiFramework.V2.Forms.Interfaces
{
    public interface ISnippetSelector
    {
        Snippet Select(Guid id);
    }
}
