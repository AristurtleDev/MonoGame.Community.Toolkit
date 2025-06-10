using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Provides extended keyboard functionality with automatic frame-to-frame state tracking and enhanced input detection
/// capabilities.
/// </summary>
/// <remarks>
/// This static class extends the functionality of the standard <see cref="Keyboard"/> class by automatically maintaining
/// previous frame state information, enabling detection of key press and release transitions. It provides a drop-in
/// replacement for <see cref="Keyboard.GetState()"/> that returns <see cref="KeyboardStateEx"/> instances with enhanced
/// input detection capabilities, including modifier key convenience properties and transition detection methods.
/// </remarks>
public static class KeyboardEx
{
    private static KeyboardState _previousState = new KeyboardState();

    /// <summary>
    /// Gets the current extended keyboard state with automatic previous frame tracking.
    /// </summary>
    /// <returns>
    /// A <see cref="KeyboardStateEx"/> structure containing current and previous frame keyboard state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's keyboard state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state. This enables immediate
    /// access to key press/release detection and modifier key combinations without additional state management.
    /// </remarks>
    public static KeyboardStateEx GetState()
    {
        KeyboardState current = Keyboard.GetState();
        KeyboardStateEx state = new KeyboardStateEx(current, _previousState);
        _previousState = current;
        return state;
    }
}
