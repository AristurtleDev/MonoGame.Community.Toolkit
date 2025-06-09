using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended gamepad thumbsticks state that tracks both current and previous frame analog stick positions,
//  enabling detection of stick movement and delta calculations.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="GamePadThumbSticks"/> by maintaining both current and
/// previous thumbstick states, allowing for detection of analog stick movement and calculation of movement deltas that
/// occur between frames.
/// </remarks>
public readonly struct GamePadThumbSticksEx : IEquatable<GamePadThumbSticksEx>
{
    private readonly GamePadThumbSticks _current;
    private readonly GamePadThumbSticks _previous;

    /// <summary>
    /// Gets the current position of the left thumbstick.
    /// </summary>
    /// <value>
    /// A <see cref="Vector2"/> representing the current position of the left thumbstick, where X and Y components range from -1.0 to 1.0.
    /// </value>
    /// <remarks>
    /// The X component represents horizontal movement (negative values indicate left, positive values indicate right).
    /// The Y component represents vertical movement (negative values indicate down, positive values indicate up).
    /// </remarks>
    public Vector2 Left => _current.Left;

    /// <summary>
    /// Gets the change in position of the left thumbstick between the previous and current frames.
    /// </summary>
    /// <value>
    /// A <see cref="Vector2"/> representing the movement delta of the left thumbstick (current position minus previous position).
    /// </value>
    public Vector2 LeftDelta => _current.Left - _previous.Left;

    /// <summary>
    /// Gets a value indicating whether the left thumbstick position has changed between the previous and current frames.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the left thumbstick has moved; otherwise, <see langword="false"/>.
    /// </value>
    public bool HasLeftMoved => LeftDelta != Vector2.Zero;

    /// <summary>
    /// Gets the current position of the right thumbstick.
    /// </summary>
    /// <value>
    /// A <see cref="Vector2"/> representing the current position of the right thumbstick, where X and Y components range from -1.0 to 1.0.
    /// </value>
    public Vector2 Right => _current.Right;

    /// <summary>
    /// Gets the change in position of the right thumbstick between the previous and current frames.
    /// </summary>
    /// <value>
    /// A <see cref="Vector2"/> representing the movement delta of the right thumbstick (current position minus previous position).
    /// </value>
    /// </remarks>
    public Vector2 RightDelta => _current.Right - _previous.Right;

    /// <summary>
    /// Gets a value indicating whether the right thumbstick position has changed between the previous and current frames.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the right thumbstick has moved; otherwise, <see langword="false"/>.
    /// </value>
    public bool HasRightMoved => RightDelta != Vector2.Zero;

    /// <summary>
    /// Initializes a new instance of the <see cref="GamePadThumbSticksEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's thumbsticks state.</param>
    /// <param name="previous">The previous frame's thumbsticks state.</param>
    public GamePadThumbSticksEx(GamePadThumbSticks current, GamePadThumbSticks previous)
    {
        _current = current;
        _previous = previous;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is GamePadThumbSticksEx other && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(GamePadThumbSticksEx other) => _current.Equals(other._current) && _previous.Equals(other._previous);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(_current, _previous);

    /// <inheritdoc/>
    public static bool operator ==(GamePadThumbSticksEx lhs, GamePadThumbSticksEx rhs) => lhs.Equals(rhs);

    /// <inheritdoc/>
    public static bool operator !=(GamePadThumbSticksEx lhs, GamePadThumbSticksEx rhs) => !lhs.Equals(rhs);
}
