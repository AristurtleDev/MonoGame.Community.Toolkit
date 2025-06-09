using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended gamepad buttons state that tracks both current and previous frame button input, enabling
/// detection of button press and release events.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="GamePadButtons"/> by maintaining both current and previous
/// button states, allowing for detection of button press and release transitions that occur between frames.
/// </remarks>
public readonly struct GamePadButtonsEx : IEquatable<GamePadButtonsEx>
{
    private readonly GamePadButtons _current;
    private readonly GamePadButtons _previous;

    /// <summary>
    /// Gets the extended state information for the A button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the A button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx A
    {
        get
        {
            bool current = _current.A == ButtonState.Pressed;
            bool previous = _previous.A == ButtonState.Pressed;

            ButtonStateEx state = current ? ButtonStateEx.Down : ButtonStateEx.Up;

            if (current & !previous)
            {
                state |= ButtonStateEx.Pressed;
            }
            else if (!current & previous)
            {
                state |= ButtonStateEx.Released;
            }

            return state;
        }
    }

    /// <summary>
    /// Gets the extended state information for the B button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the B button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx B
    {
        get
        {
            bool current = _current.B == ButtonState.Pressed;
            bool previous = _previous.B == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the Back button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the Back button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Back
    {
        get
        {
            bool current = _current.Back == ButtonState.Pressed;
            bool previous = _previous.Back == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the Big Button (Xbox Guide button).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the Big Button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// The Big Button is typically the Xbox Guide button or equivalent system button on other controllers.
    /// </remarks>
    public readonly ButtonStateEx BigButton
    {
        get
        {
            bool current = _current.BigButton == ButtonState.Pressed;
            bool previous = _previous.BigButton == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the left shoulder button (LB).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the left shoulder button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx LeftShoulder
    {
        get
        {
            bool current = _current.LeftShoulder == ButtonState.Pressed;
            bool previous = _previous.LeftShoulder == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the left stick button (clicking the left analog stick).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the left stick button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// This represents clicking down on the left analog stick, not the stick's position.
    /// </remarks>
    public readonly ButtonStateEx LeftStick
    {
        get
        {
            bool current = _current.LeftStick == ButtonState.Pressed;
            bool previous = _previous.LeftStick == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the right shoulder button (RB).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the right shoulder button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx RightShoulder
    {
        get
        {
            bool current = _current.RightShoulder == ButtonState.Pressed;
            bool previous = _previous.RightShoulder == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the right stick button (clicking the right analog stick).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the right stick button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// This represents clicking down on the right analog stick, not the stick's position.
    /// </remarks>
    public readonly ButtonStateEx RightStick
    {
        get
        {
            bool current = _current.RightStick == ButtonState.Pressed;
            bool previous = _previous.RightStick == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the Start button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the Start button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Start
    {
        get
        {
            bool current = _current.Start == ButtonState.Pressed;
            bool previous = _previous.Start == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the X button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the X button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx X
    {
        get
        {
            bool current = _current.X == ButtonState.Pressed;
            bool previous = _previous.X == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Gets the extended state information for the Y button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the Y button.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a button that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Y
    {
        get
        {
            bool current = _current.Y == ButtonState.Pressed;
            bool previous = _previous.Y == ButtonState.Pressed;
            return GetButtonStateEx(current, previous);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GamePadButtonsEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's gamepad buttons state.</param>
    /// <param name="previous">The previous frame's gamepad buttons state.</param>
    public GamePadButtonsEx(GamePadButtons current, GamePadButtons previous)
    {
        _current = current;
        _previous = previous;
    }

    private static ButtonStateEx GetButtonStateEx(bool current, bool previous)
    {
        ButtonStateEx state = current ? ButtonStateEx.Down : ButtonStateEx.Up;

        if (current && !previous)
        {
            state |= ButtonStateEx.Pressed;
        }
        else if (!current && previous)
        {
            state |= ButtonStateEx.Released;
        }

        return state;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is GamePadButtonsEx other && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(GamePadButtonsEx other) => _current.Equals(other._current) && _previous.Equals(other._previous);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(_current, _previous);

    /// <inheritdoc/>
    public static bool operator ==(GamePadButtonsEx lhs, GamePadButtonsEx rhs) => lhs.Equals(rhs);

    /// <inheritdoc/>
    public static bool operator !=(GamePadButtonsEx lhs, GamePadButtonsEx rhs) => !lhs.Equals(rhs);
}
