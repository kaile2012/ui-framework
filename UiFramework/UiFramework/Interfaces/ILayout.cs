namespace UiFramework.V2.Interfaces
{
    public interface ILayout : IBase
    {
        string Name { get; set; }

        string Description { get; set; }

        ILayoutItem[] Items { get; set; }

        int ColumnCount { get; set; }

        int RowCount { get; set; }
    }
}
