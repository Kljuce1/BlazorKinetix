using Microsoft.AspNetCore.Components;

namespace BlazorKinetix.Components;

/// <summary>An entry in a navigation menu.</summary>
public sealed class NavMenuItem
{
    /// <summary>Identifies the entry in selection callbacks.</summary>
    public required string Key { get; init; }
    /// <summary>Displays the entry label.</summary>
    public required string Text { get; init; }
    /// <summary>Navigates to this URL when the entry is selected.</summary>
    public string? Href { get; init; }
    /// <summary>Displays custom icon markup.</summary>
    public RenderFragment? Icon { get; init; }
    /// <summary>Uses icon classes from the host application.</summary>
    public string? IconClass { get; init; }
    /// <summary>Displays an image icon.</summary>
    public string? ImageSrc { get; init; }
    /// <summary>Controls whether the entry can be selected.</summary>
    public bool Enabled { get; init; } = true;
    /// <summary>Controls whether the entry is displayed.</summary>
    public bool Visible { get; init; } = true;
    /// <summary>Displays nested entries.</summary>
    public IReadOnlyList<NavMenuItem> Children { get; init; } = [];
}
