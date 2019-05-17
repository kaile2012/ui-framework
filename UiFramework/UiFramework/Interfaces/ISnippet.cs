namespace UiFramework.V2.Interfaces
{
    public interface ISnippet : IBase
    {
        string Name { get; set; }

        string Description { get; set; }

        string Html { get; set; }
    }
}
