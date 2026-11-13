namespace BlazorKinetix.Core;

/// <summary>Specifies how an input value is parsed.</summary>
public enum InputBoxKind
{
    /// <summary>Plain text.</summary>
    Text,
    /// <summary>Whole number.</summary>
    Integer,
    /// <summary>Decimal number.</summary>
    Decimal,
    /// <summary>Masked password.</summary>
    Password
}

/// <summary>Specifies the field's visual treatment.</summary>
public enum InputBoxAppearance
{
    /// <summary>Standard border.</summary>
    Normal,
    /// <summary>Two-tone border.</summary>
    Gradient,
    /// <summary>Animated lower border.</summary>
    Underline
}

/// <summary>Specifies how the field label is displayed.</summary>
public enum InputBoxLabelMode
{
    /// <summary>Label above the field.</summary>
    Above,
    /// <summary>Label inside the field that moves on focus.</summary>
    Floating
}

/// <summary>Specifies the button interaction.</summary>
public enum ButtonMode
{
    /// <summary>Click action.</summary>
    Default,
    /// <summary>Press and hold action.</summary>
    HoldToConfirm,
    /// <summary>Click action with success feedback.</summary>
    ActionFeedback,
    /// <summary>Range slider action.</summary>
    SlideToConfirm
}

/// <summary>Specifies the button's color role.</summary>
public enum ColorRole
{
    /// <summary>Primary color.</summary>
    Primary,
    /// <summary>Secondary color.</summary>
    Secondary,
    /// <summary>Success color.</summary>
    Success,
    /// <summary>Warning color.</summary>
    Warning,
    /// <summary>Danger color.</summary>
    Danger
}

/// <summary>Specifies the button's fill treatment.</summary>
public enum ButtonVariant
{
    /// <summary>Filled background.</summary>
    Solid,
    /// <summary>Outlined background.</summary>
    Outline
}

/// <summary>Specifies where a tooltip appears.</summary>
public enum TooltipPosition
{
    /// <summary>Above the anchor.</summary>
    Top,
    /// <summary>Below the anchor.</summary>
    Bottom,
    /// <summary>Before the anchor.</summary>
    Left,
    /// <summary>After the anchor.</summary>
    Right
}

/// <summary>Specifies the modal width.</summary>
public enum ModalSize
{
    /// <summary>Narrow dialog.</summary>
    Compact,
    /// <summary>Standard dialog.</summary>
    Medium,
    /// <summary>Wide dialog.</summary>
    Large
}

