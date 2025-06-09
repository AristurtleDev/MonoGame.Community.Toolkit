using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended gamepad directional pad (D-pad) state that tracks both current and previous frame input,
/// enabling detection of D-pad press and release events.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="GamePadDPad"/> by maintaining both current and previous D-pad
/// states, allowing for detection of directional input press and release transitions that occur between frames.
/// </remarks>
public readonly struct GamePadDPadEx : IEquatable<GamePadDPadEx>
{
    private readonly GamePadDPad _current;
    private readonly GamePadDPad _previous;

    /// <summary>
    /// Gets the extended state information for the up direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the up direction.
    /// </value>
    public readonly ButtonStateEx Up
    {
        get
        {
            bool current = _current.Up == ButtonState.Pressed;
            bool previous = _previous.Up == ButtonState.Pressed;

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
    }

    /// <summary>
    /// Gets the extended state information for the down direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the down direction.
    /// </value>
    public readonly ButtonStateEx Down
    {
        get
        {
            bool current = _current.Down == ButtonState.Pressed;
            bool previous = _previous.Down == ButtonState.Pressed;

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
    }

    /// <summary>
    /// Gets the extended state information for the left direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the left direction.
    /// </value>
    public readonly ButtonStateEx Left
    {
        get
        {
            bool current = _current.Left == ButtonState.Pressed;
            bool previous = _previous.Left == ButtonState.Pressed;

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
    }

    /// <summary>
    /// Gets the extended state information for the right direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the right direction.
    /// </value>
    public readonly ButtonStateEx Right
    {
        get
        {
            bool current = _current.Right == ButtonState.Pressed;
            bool previous = _previous.Right == ButtonState.Pressed;

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
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GamePadDPadEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's dpad state.</param>
    /// <param name="previous">The previous frame's dpad state.</param>
    public GamePadDPadEx(GamePadDPad current, GamePadDPad previous)
    {
        _current = current;
        _previous = previous;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is GamePadDPadEx other && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(GamePadDPadEx other) => _current.Equals(other._current) && _previous.Equals(other._previous);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(_current, _previous);

    /// <inheritdoc/>
    public static bool operator ==(GamePadDPadEx lhs, GamePadDPadEx rhs) => lhs.Equals(rhs);

    /// <inheritdoc/>
    public static bool operator !=(GamePadDPadEx lhs, GamePadDPadEx rhs) => !lhs.Equals(rhs);
}
