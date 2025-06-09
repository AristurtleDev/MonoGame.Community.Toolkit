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
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a direction that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Up;

    /// <summary>
    /// Gets the extended state information for the down direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the down direction.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a direction that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Down;

    /// <summary>
    /// Gets the extended state information for the left direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the left direction.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a direction that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Left;

    /// <summary>
    /// Gets the extended state information for the right direction on the D-pad.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the right direction.
    /// </value>
    /// <remarks>
    /// The returned value can contain multiple flags. For example, a direction that was just pressed
    /// will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx Right;

    /// <summary>
    /// Initializes a new instance of the <see cref="GamePadDPadEx"/> structure.
    /// </summary>
    /// <param name="up">The extended state of the up direction.</param>
    /// <param name="down">The extended state of the down direction.</param>
    /// <param name="left">The extended state of the left direction.</param>
    /// <param name="right">The extended state of the right direction.</param>
    public GamePadDPadEx(ButtonStateEx up, ButtonStateEx down, ButtonStateEx left, ButtonStateEx right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
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
