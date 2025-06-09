using System;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Extended Button states that include transition states.
/// Can be combined as flags since a key can be both Down and Pressed, or Up and Released simultaneously.
/// </summary>
[Flags]
public enum ButtonStateEx
{
    /// <summary>
    /// Button is not pressed.
    /// </summary>
    Up = 0,

    /// <summary>
    /// Button is current pressed.
    /// </summary>
    Down = 1,

    /// <summary>
    /// Button was just pressed this frame (transitioned from Up to Down).
    /// </summary>
    Pressed = 2,

    /// <summary>
    /// Button was just released this frame (transitioned from Down to Up).
    /// </summary>
    Released = 4
}
