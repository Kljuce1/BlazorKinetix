namespace BlazorKinetix.Core;

/// <summary>A date that cannot be selected, with an optional explanation.</summary>
public sealed record DisabledDate(DateOnly Date, string? Message = null);
