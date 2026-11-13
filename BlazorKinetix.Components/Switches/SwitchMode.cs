namespace BlazorKinetix.Components;

/// <summary>Selects a switch interaction style.</summary>
public enum SwitchMode
{
    /// <summary>Stretches the knob as it moves.</summary>
    Liquid,
    /// <summary>Changes the knob icon between a cross and a check.</summary>
    IconMorph,
    /// <summary>Draws a check in a square box.</summary>
    Checkbox,
    /// <summary>Selects one item from a set of options.</summary>
    Segmented
}
