namespace BlazorKinetix.Core;

/// <summary>A time option with availability and an optional explanation.</summary>
public sealed record TimeSlotItem(TimeOnly Time, bool IsAvailable = true, string? Message = null);
