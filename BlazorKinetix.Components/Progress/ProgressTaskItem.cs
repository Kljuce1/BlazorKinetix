namespace BlazorKinetix.Components;

/// <summary>Defines a task counted by a progress indicator.</summary>
public sealed record ProgressTaskItem(string Key, string Text, bool IsComplete = false);

/// <summary>Reports a task selection change.</summary>
public sealed record ProgressTaskChange(string Key, bool IsComplete);
