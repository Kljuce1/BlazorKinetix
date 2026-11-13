namespace BlazorKinetix.Components;

/// <summary>Selects a toast appearance.</summary>
public enum ToastState
{
    /// <summary>Displays informational feedback.</summary>
    Info,
    /// <summary>Displays successful feedback.</summary>
    Success,
    /// <summary>Displays cautionary feedback.</summary>
    Warning,
    /// <summary>Displays error feedback.</summary>
    Danger,
    /// <summary>Uses host CSS variables.</summary>
    Custom
}
