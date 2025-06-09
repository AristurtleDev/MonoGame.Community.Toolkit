using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended gamepad triggers state that tracks both current and previous frame trigger positions,
/// enabling detection of trigger movement and delta calculations.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="GamePadTriggers"/> by maintaining both current and previous
/// trigger states, allowing for detection of analog trigger movement and calculation of movement deltas that occur
/// between frames.
/// </remarks>
public readonly struct GamePadTriggersEx : IEquatable<GamePadTriggersEx>
{
    private readonly GamePadTriggers _current;
    private readonly GamePadTriggers _previous;

    /// <summary>
    /// Gets the current position of the left trigger.
    /// </summary>
    /// <value>
    /// A <see cref="float"/> representing the current position of the left trigger, ranging from 0.0 (not pressed) to 1.0 (fully pressed).
    /// </value>
    public float Left => _current.Left;

    /// <summary>
    /// Gets the change in position of the left trigger between the previous and current frames.
    /// </summary>
    /// <value>
    /// A <see cref="float"/> representing the movement delta of the left trigger (current position minus previous position).
    /// </value>
    public float LeftDelta => _current.Left - _previous.Left;

    /// <summary>
    /// Gets a value indicating whether the left trigger position has changed between the previous and current frames.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the left trigger has moved; otherwise, <see langword="false"/>.
    /// </value>
    public bool HasLeftMoved => LeftDelta != 0.0f;

    /// <summary>
    /// Gets the current position of the right trigger.
    /// </summary>
    /// <value>
    /// A <see cref="float"/> representing the current position of the right trigger, ranging from 0.0 (not pressed) to 1.0 (fully pressed).
    /// </value>
    public float Right => _current.Right;

    /// <summary>
    /// Gets the change in position of the right trigger between the previous and current frames.
    /// </summary>
    /// <value>
    /// A <see cref="float"/> representing the movement delta of the right trigger (current position minus previous position).
    /// </value>
    public float RightDelta => _current.Right - _previous.Right;

    /// <summary>
    /// Gets a value indicating whether the right trigger position has changed between the previous and current frames.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the right trigger has moved; otherwise, <see langword="false"/>.
    /// </value>
    public bool HasRightMoved => RightDelta != 0.0f;

    /// <summary>
    /// Initializes a new instance of the <see cref="GamePadTriggersEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's triggers state.</param>
    /// <param name="previous">The previous frame's triggers state.</param>
    public GamePadTriggersEx(GamePadTriggers current, GamePadTriggers previous)
    {
        _current = current;
        _previous = previous;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is GamePadTriggersEx other && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(GamePadTriggersEx other) => _current.Equals(other._current) && _previous.Equals(other._previous);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(_current, _previous);

    /// <inheritdoc/>
    public static bool operator ==(GamePadTriggersEx lhs, GamePadTriggersEx rhs) => lhs.Equals(rhs);

    /// <inheritdoc/>
    public static bool operator !=(GamePadTriggersEx lhs, GamePadTriggersEx rhs) => !lhs.Equals(rhs);
}
