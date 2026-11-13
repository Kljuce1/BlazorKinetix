using Microsoft.AspNetCore.Components;

namespace BlazorKinetix.Components;

/// <summary>Defines a tab and its optional icon.</summary>
public sealed record TabOption(
    string Key,
    string Title,
    string? IconClass = null,
    RenderFragment? Icon = null,
    bool Enabled = true,
    bool Visible = true,
    string? ImageSrc = null);
