namespace BlazorKinetix.Components;

/// <summary>Sets the dialog entrance animation.</summary>
public enum DialogAnimation
{
    /// <summary>Opens without an entrance animation.</summary>
    None,
    /// <summary>Enters from above.</summary>
    TopDrop,
    /// <summary>Swings into place.</summary>
    Swing,
    /// <summary>Unfolds vertically.</summary>
    Unfold,
    /// <summary>Comes into focus.</summary>
    Blur,
    /// <summary>Grows into place.</summary>
    Grow
}
