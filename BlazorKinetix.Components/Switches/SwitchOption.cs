namespace BlazorKinetix.Components;

/// <summary>Defines an option in a segmented switch.</summary>
public sealed record SwitchOption(string Key, string Text, bool Enabled = true);
