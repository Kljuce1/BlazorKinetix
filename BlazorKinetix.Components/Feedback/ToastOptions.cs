using Microsoft.AspNetCore.Components;

namespace BlazorKinetix.Components;

/// <summary>Defines a notification shown by ToastService.</summary>
public sealed record ToastOptions
{
    /// <summary>Sets the toast appearance.</summary>
    public ToastState State { get; init; } = ToastState.Info;
    /// <summary>Displays a heading.</summary>
    public string? Title { get; init; }
    /// <summary>Displays the notification message.</summary>
    public string Message { get; init; } = "";
    /// <summary>Displays a button beside the message.</summary>
    public string? ActionText { get; init; }
    /// <summary>Runs when the action button is pressed.</summary>
    public Func<Task>? OnAction { get; init; }
    /// <summary>Sets the time before automatic dismissal; zero keeps it visible.</summary>
    public TimeSpan Duration { get; init; } = TimeSpan.FromSeconds(5);
    /// <summary>Shows a close button.</summary>
    public bool ShowClose { get; init; } = true;
    /// <summary>Labels the close button.</summary>
    public string CloseLabel { get; init; } = "Close";
    /// <summary>Displays custom icon markup.</summary>
    public RenderFragment? Icon { get; init; }
    /// <summary>Uses an icon class.</summary>
    public string? IconClass { get; init; }
    /// <summary>Adds CSS classes to the toast.</summary>
    public string? CssClass { get; init; }
}
