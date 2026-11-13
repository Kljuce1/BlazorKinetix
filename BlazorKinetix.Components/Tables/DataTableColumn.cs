using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace BlazorKinetix.Components;

/// <summary>Defines a column in a DataTable.</summary>
public sealed class DataTableColumn<TItem>
{
    /// <summary>Identifies the column for sorting.</summary>
    public required string Key { get; init; }
    /// <summary>Displays the column heading.</summary>
    public required string Header { get; init; }
    /// <summary>Reads the cell value from an item.</summary>
    public required Func<TItem, object?> Value { get; init; }
    /// <summary>Renders custom cell content.</summary>
    public RenderFragment<DataTableCellContext<TItem>>? CellTemplate { get; init; }
    /// <summary>Sets a CSS width such as 12rem or 25%.</summary>
    public string? Width { get; init; }
    /// <summary>Shows the column.</summary>
    public bool Visible { get; init; } = true;
    /// <summary>Enables sorting by this column.</summary>
    public bool Sortable { get; init; }
    /// <summary>Shows the value immediately on mobile cards.</summary>
    public bool MobilePrimary { get; init; } = true;
    /// <summary>Adds a class to cells in this column.</summary>
    public string? CssClass { get; init; }
    /// <summary>Adds a class to the column heading.</summary>
    public string? HeaderCssClass { get; init; }
    /// <summary>Reads a separate value used for local sorting.</summary>
    public Func<TItem, IComparable?>? SortValue { get; init; }
    /// <summary>Formats numeric and date values in this column.</summary>
    public string? Format { get; init; }
    /// <summary>Overrides the table culture for this column.</summary>
    public CultureInfo? Culture { get; init; }
}

/// <summary>Provides the item, column, and value to a cell template.</summary>
public sealed record DataTableCellContext<TItem>(TItem Item, DataTableColumn<TItem> Column, object? Value);
