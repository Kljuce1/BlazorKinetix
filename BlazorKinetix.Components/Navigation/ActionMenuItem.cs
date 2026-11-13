using Microsoft.AspNetCore.Components;

namespace BlazorKinetix.Components;

/// <summary>Defines an action in a menu.</summary>
public sealed record ActionMenuItem(string Key, string Text, string? Href = null,
    string? IconClass = null, RenderFragment? Icon = null, string? ImageSrc = null,
    bool Enabled = true);
