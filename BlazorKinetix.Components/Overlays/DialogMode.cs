namespace BlazorKinetix.Components;

/// <summary>Sets the dialog's visual and semantic state.</summary>
public enum DialogMode
{
    /// <summary>Shows an informational message.</summary>
    Information,
    /// <summary>Asks the user to make a choice.</summary>
    Question,
    /// <summary>Warns about a condition.</summary>
    Warning,
    /// <summary>Confirms a risky action.</summary>
    Danger,
    /// <summary>Reports a failed action.</summary>
    Error,
    /// <summary>Uses custom content and styling.</summary>
    Custom
}
