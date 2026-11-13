namespace BlazorKinetix.Components;

/// <summary>Describes a page requested from a data source.</summary>
public sealed record DataTableRequest(int Offset, int Count, string? SortKey, bool Descending);

/// <summary>Returns rows and optional result counts from a data source.</summary>
public sealed record DataTableResult<TItem>(IReadOnlyList<TItem> Items, int? TotalCount = null, bool? HasMore = null);
