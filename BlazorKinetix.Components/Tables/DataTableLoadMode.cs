namespace BlazorKinetix.Components;

/// <summary>Controls how a DataTable displays its data.</summary>
public enum DataTableLoadMode
{
    /// <summary>Uses pages when page sizes or a provider are supplied.</summary>
    Auto,
    /// <summary>Shows all rows.</summary>
    Full,
    /// <summary>Shows one page at a time.</summary>
    Paged,
    /// <summary>Appends rows as the user scrolls.</summary>
    Infinite
}
