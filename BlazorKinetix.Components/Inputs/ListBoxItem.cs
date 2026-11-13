using Microsoft.AspNetCore.Components;

namespace BlazorKinetix.Components;

/// <summary>Defines an option displayed by ListBox.</summary>
public sealed record ListBoxItem<TValue>(
    TValue Value,
    string Text,
    string? Description = null,
    string? IconClass = null,
    RenderFragment? Icon = null,
    string? ImageSrc = null,
    bool Enabled = true);
