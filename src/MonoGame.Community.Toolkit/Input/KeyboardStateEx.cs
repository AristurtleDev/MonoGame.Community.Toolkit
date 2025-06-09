using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended keyboard state that tracks both current and previous frame keyboard input, enabling detection
/// of key press and release events.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="KeyboardState"/> by maintaining both current and previous
/// keyboard states, allowing for detection of key press and release transitions that occur between frames.
/// </remarks>
public readonly struct KeyboardStateEx : IEquatable<KeyboardStateEx>
{
    /// <summary>
    /// The current frame's keyboard state.
    /// </summary>
    /// <value>
    /// A <see cref="KeyboardState"/> representing the keyboard state for the current frame.
    /// </value>
    public readonly KeyboardState CurrentState;

    /// <summary>
    /// The previous frame's keyboard state.
    /// </summary>
    /// <value>
    /// A <see cref="KeyboardState"/> representing the keyboard state for the previous frame.
    /// </value>
    public readonly KeyboardState PreviousState;

    /// <summary>
    /// Gets a value indicating whether the Caps Lock key is currently active.
    /// </summary>
    /// <value><see langword="true"/> if Caps Lock is active; otherwise, <see langword="false"/>.</value>
    public readonly bool CapsLock => CurrentState.CapsLock;

    /// <summary>
    /// Gets a value indicating whether the Num Lock key is currently active.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if Num Lock is active; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool NumLock => CurrentState.NumLock;

    /// <summary>
    /// Gets a value indicating whether the left Shift key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the left Shift key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool LeftShift => CurrentState.IsKeyDown(Keys.LeftShift);

    /// <summary>
    /// Gets a value indicating whether the right Shift key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the right Shift key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool RightShift => CurrentState.IsKeyDown(Keys.RightShift);

    /// <summary>
    /// Gets a value indicating whether either Shift key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if either the left or right Shift key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool Shift => LeftShift || RightShift;

    /// <summary>
    /// Gets a value indicating whether the left Alt key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the left Alt key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool LeftAlt => CurrentState.IsKeyDown(Keys.LeftAlt);

    /// <summary>
    /// Gets a value indicating whether the right Alt key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the right Alt key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool RightAlt => CurrentState.IsKeyDown(Keys.RightAlt);

    /// <summary>
    /// Gets a value indicating whether either Alt key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if either the left or right Alt key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool Alt => LeftAlt || RightAlt;

    /// <summary>
    /// Gets a value indicating whether the left Control key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the left Control key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool LeftControl => CurrentState.IsKeyDown(Keys.LeftControl);

    /// <summary>
    /// Gets a value indicating whether the right Control key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the right Control key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool RightControl => CurrentState.IsKeyDown(Keys.RightControl);

    /// <summary>
    /// Gets a value indicating whether either Control key is currently being pressed.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if either the left or right Control key is currently down; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool Control => LeftControl || RightControl;

    /// <summary>
    /// Gets the extended state information for the specified key.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <value>
    /// A <see cref="KeyStateEx"/> value that contains information about whether the key is currently down, up, was just
    ///  pressed, or was just released.
    /// </value>
    /// <remarks>
    /// The returned <see cref="KeyStateEx"/> value can contain multiple flags. For example, a key that was just pressed
    /// will have both <see cref="KeyStateEx.Down"/> and <see cref="KeyStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly KeyStateEx this[Keys key]
    {
        get
        {
            bool currentlyDown = CurrentState.IsKeyDown(key);
            bool previouslyDown = PreviousState.IsKeyDown(key);

            KeyStateEx state = currentlyDown ? KeyStateEx.Down : KeyStateEx.Up;

            if (currentlyDown && !previouslyDown)
            {
                state |= KeyStateEx.Pressed;
            }
            else if (!currentlyDown && previouslyDown)
            {
                state |= KeyStateEx.Released;
            }

            return state;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardStateEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's keyboard state.</param>
    /// <param name="previous">The previous frame's keyboard state.</param>
    public KeyboardStateEx(KeyboardState current, KeyboardState previous)
    {
        CurrentState = current;
        PreviousState = previous;
    }

    /// <summary>
    /// Determines whether the specified key is currently being pressed down.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified key is currently down; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool IsKeyDown(Keys key) => CurrentState.IsKeyDown(key);

    /// <summary>
    /// Determines whether the specified key is currently up (not being pressed).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified key is currently up; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool IsKeyUp(Keys key) => CurrentState.IsKeyUp(key);

    /// <summary>
    /// Determines whether the specified key was just pressed
    /// (transitioned from up to down between the previous and current frames).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified key was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A key is considered "pressed" when it is down in the current state but was up in the previous state.
    /// This method is useful for detecting single key press events rather than continuous key holding.
    /// </remarks>
    public readonly bool WasKeyPressed(Keys key) =>
        CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);

    /// <summary>
    /// Determines whether the specified key was just released
    /// (transitioned from down to up between the previous and current frames).
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified key was just released; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A key is considered "released" when it is up in the current state but was down in the previous state.
    /// This method is useful for detecting key release events.
    /// </remarks>
    public readonly bool WasKeyReleased(Keys key) =>
        CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);

    /// <summary>
    /// Gets the number of keys that are currently being pressed.
    /// </summary>
    /// <returns>
    /// The number of keys currently in the down state.
    /// </returns>
    /// <remarks>
    /// This method provides an efficient way to determine how many keys are pressed without allocating an array.
    /// </remarks>
    public readonly int GetPressedKeyCount() => CurrentState.GetPressedKeyCount();

    /// <summary>
    /// Gets an array of all keys that are currently being pressed.
    /// </summary>
    /// <returns>
    /// An array of <see cref="Keys"/> values representing all currently pressed keys.
    /// </returns>
    /// <remarks>
    /// This method returns a new array each time it is called. For better performance when called frequently,
    /// consider using the <see cref="GetPressedKeys(Keys[])"/> overload with a pre-allocated array.
    /// </remarks>
    public readonly Keys[] GetPressedKeys() => CurrentState.GetPressedKeys();

    /// <summary>
    /// Fills the specified array with all keys that are currently being pressed.
    /// </summary>
    /// <param name="keys">
    /// The array to fill with pressed keys. The array must be large enough to hold all pressed keys.
    /// </param>
    /// <remarks>
    /// This method provides a more efficient way to get pressed keys when called frequently, as it reuses the provided
    /// array instead of allocating a new one. Ensure the array is large enough to accommodate all possible pressed
    /// keys.
    /// </remarks>
    public readonly void GetPressedKeys(Keys[] keys) => CurrentState.GetPressedKeys(keys);

    /// <summary>
    /// Determines the directional input from keyboard keys, return -1, 0, or 1;
    /// </summary>
    /// <param name="negative">The key representing negative direction.</param>
    /// <param name="positive">The key representing positive direction.</param>
    /// <param name="bothValue">The value to return when both keys are pressed. Default is 0.</param>
    /// <returns>
    /// -1 if only the negative key is pressed, 1 if only the positive key is pressed, <paramref name="bothValue"/> if
    /// both are pressed, or 0 if neither is pressed.
    // </returns>
    public readonly int GetAxis(Keys negative, Keys positive, int bothValue = 0)
    {
        bool negativeDown = IsKeyDown(negative);
        bool positiveDown = IsKeyDown(positive);
        if (negativeDown && positiveDown) { return bothValue; }
        if (negativeDown) { return -1; }
        if (positiveDown) { return 1; }
        return 0;
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object obj) => obj is KeyboardStateEx other && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(KeyboardStateEx other) =>
        CurrentState.Equals(other.CurrentState) && PreviousState.Equals(other.PreviousState);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(CurrentState, PreviousState);

    public static bool operator ==(KeyboardStateEx lhs, KeyboardStateEx rhs) => lhs.Equals(rhs);
    public static bool operator !=(KeyboardStateEx lhs, KeyboardStateEx rhs) => !lhs.Equals(rhs);


}
