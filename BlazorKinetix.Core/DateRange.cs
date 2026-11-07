namespace BlazorKinetix.Core;

/// <summary>A confirmed inclusive date range.</summary>
public readonly record struct DateRange(DateOnly Start, DateOnly End);
