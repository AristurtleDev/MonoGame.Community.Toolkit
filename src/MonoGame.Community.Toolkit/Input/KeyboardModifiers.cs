using System;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Specifies keyboard modifier key states.
/// </summary>
[Flags]
public enum KeyboardModifiers
{
    /// <summary>
    /// No modifier keys are pressed.
    /// </summary>
    None = 0,

    /// <summary>
    /// One or both Control keys (Ctrl) are pressed.
    /// </summary>
    Control = 1,

    /// <summary>
    /// One or both Shift keys are pressed.
    /// </summary>
    Shift = 2,

    /// <summary>
    /// One or both Alt keys are pressed.
    /// </summary>
    Alt = 4
}
