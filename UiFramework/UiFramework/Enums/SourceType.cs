namespace UiFramework.V2.Enums
{
    /// <summary>
    /// An enum to represent the source of the bound items, either straight from a IModelSelector or from the page's BindingContext
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// Data fetched straight from the IModelSelector
        /// </summary>
        Context = 0,
        /// <summary>
        /// Data fetched from the BindingContext
        /// </summary>
        Service = 1
    }
}
