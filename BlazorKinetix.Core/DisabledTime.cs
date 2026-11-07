namespace BlazorKinetix.Core;

/// <summary>A time that cannot be selected, with an optional explanation.</summary>
public sealed record DisabledTime(TimeOnly Time, string? Message = null);
