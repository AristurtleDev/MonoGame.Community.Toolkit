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
    /// Initializes a new instance of the <see cref="GamePadTriggersEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's triggers state.</param>
    /// <param name="previous">The previous frame's triggers state.</param>
    public GamePadTriggersEx(GamePadTriggers current, GamePadTriggers previous)
    {
        _current = current;
        _previous = previous;
    }

    /// <summary>
    /// Gets the current position of the left trigger.
    /// </summary>
    /// <returns>
    /// A <see cref="float"/> representing the current position of the left trigger, ranging from 0.0 (not pressed) to
    /// 1.0 (fully pressed).
    /// </returns>
    /// <remarks>
    /// The value represents the analog pressure applied to the left trigger. A value of 0.0 indicates the trigger is
    /// not pressed, while a value of 1.0 indicates the trigger is fully depressed.
    /// </remarks>
    public float Left() => _current.Left;

    /// <summary>
    /// Gets the current position of the left trigger with threshold filtering.
    /// </summary>
    /// <param name="threshold">The minimum trigger value required for activation. Values below this threshold return 0.0.</param>
    /// <returns>
    /// A <see cref="float"/> representing the current position of the left trigger after threshold filtering.
    /// </returns>
    /// <remarks>
    /// Threshold filtering helps create distinct trigger activation points and eliminates noise from very light trigger
    /// touches. If the trigger value is below the specified threshold, 0.0 is returned; otherwise, the actual trigger
    /// value is returned. This is useful for implementing digital-style trigger behavior or eliminating accidental
    /// trigger activation.
    /// </remarks>
    public float Left(float threshold)
    {
        if (_current.Left < threshold)
        {
            return 0.0f;
        }

        return _current.Left;
    }

    /// <summary>
    /// Gets the change in position of the left trigger between the previous and current frames.
    /// </summary>
    /// <returns>
    /// A <see cref="float"/> representing the movement delta of the left trigger (current position minus previous
    /// position).
    /// </returns>
    /// <remarks>
    /// This value represents how much the left trigger position has changed since the previous frame. A value of 0.0
    /// indicates no movement.
    /// </remarks>
    public float LeftDelta() => _current.Left - _previous.Left;

    /// <summary>
    /// Gets the change in position of the left trigger between the previous and current frames with threshold filtering.
    /// </summary>
    /// <param name="threshold">The minimum trigger value required for activation, applied to both current and previous positions.</param>
    /// <returns>
    /// A <see cref="float"/> representing the movement delta of the left trigger after threshold filtering.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Threshold filtering helps create distinct trigger activation points and eliminates noise from very light trigger
    /// touches. If the trigger value is below the specified threshold, 0.0 is returned; otherwise, the actual trigger
    /// value is returned. This is useful for implementing digital-style trigger behavior or eliminating accidental
    /// trigger activation.
    /// </para>
    /// <para>
    /// Threshold filtering is applied to both the current and previous trigger positions before calculating the delta.
    /// This ensures that small trigger movements below the threshold don't result in false movement detection,
    /// making it useful for detecting significant trigger state changes rather than minor pressure variations.
    /// </para>
    /// </remarks>
    public float LeftDelta(float threshold)
    {
        float current = _current.Left < threshold ? 0.0f : _current.Left;
        float previous = _previous.Left < threshold ? 0.0f : _previous.Left;
        return current - previous;
    }

    /// <summary>
    /// Gets a value indicating whether the left trigger position has changed between the previous and current frames.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the left trigger has moved; otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasLeftMoved() => LeftDelta() != 0.0f;

    /// <summary>
    /// Gets a value indicating whether the left trigger position has changed between the previous and current frames,
    /// with threshold filtering.
    /// </summary>
    /// <param name="threshold">The minimum trigger value required for activation when calculating movement.</param>
    /// <returns>
    /// <see langword="true"/> if the left trigger has moved beyond the threshold; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method applies threshold filtering when determining movement, helping to eliminate false positives
    /// from very light trigger touches or minor pressure variations that don't represent intentional input.
    /// </remarks>
    public bool HasLeftMoved(float threshold) => LeftDelta(threshold) != 0.0f;


    /// <summary>
    /// Gets the current position of the right trigger.
    /// </summary>
    /// <returns>
    /// A <see cref="float"/> representing the current position of the right trigger, ranging from 0.0 (not pressed) to
    /// 1.0 (fully pressed).
    /// </returns>
    /// <remarks>
    /// The value represents the analog pressure applied to the right trigger. A value of 0.0 indicates the trigger is
    /// not pressed,
    /// while a value of 1.0 indicates the trigger is fully depressed.
    /// </remarks>
    public float Right() => _current.Right;

    /// <summary>
    /// Gets the current position of the right trigger with threshold filtering.
    /// </summary>
    /// <param name="threshold">The minimum trigger value required for activation. Values below this threshold return 0.0.</param>
    /// <returns>
    /// A <see cref="float"/> representing the current position of the right trigger after threshold filtering.
    /// </returns>
    /// <remarks>
    /// Threshold filtering helps create distinct trigger activation points and eliminates noise from very light trigger
    /// touches. If the trigger value is below the specified threshold, 0.0 is returned; otherwise, the actual trigger
    /// value is returned. This is useful for implementing digital-style trigger behavior or eliminating accidental
    /// trigger activation.
    /// </remarks>
    public float Right(float threshold)
    {
        if (_current.Right < threshold)
        {
            return 0.0f;
        }

        return _current.Right;
    }

    /// <summary>
    /// Gets the change in position of the right trigger between the previous and current frames.
    /// </summary>
    /// <returns>
    /// A <see cref="float"/> representing the movement delta of the right trigger (current position minus previous
    /// position).
    /// </returns>
    /// <remarks>
    /// This value represents how much the right trigger position has changed since the previous frame. A value of 0.0
    /// indicates no movement.
    /// </remarks>
    public float RightDelta() => _current.Right - _previous.Right;

    /// <summary>
    /// Gets the change in position of the right trigger between the previous and current frames with threshold
    /// filtering.
    /// </summary>
    /// <param name="threshold">The minimum trigger value required for activation, applied to both current and previous positions.</param>
    /// <returns>
    /// A <see cref="float"/> representing the movement delta of the right trigger after threshold filtering.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Threshold filtering helps create distinct trigger activation points and eliminates noise from very light trigger
    /// touches. If the trigger value is below the specified threshold, 0.0 is returned; otherwise, the actual trigger
    /// value is returned. This is useful for implementing digital-style trigger behavior or eliminating accidental
    /// trigger activation.
    /// </para>
    /// <para>
    /// Threshold filtering is applied to both the current and previous trigger positions before calculating the delta.
    /// This ensures that small trigger movements below the threshold don't result in false movement detection,
    /// making it useful for detecting significant trigger state changes rather than minor pressure variations.
    /// </para>
    /// </remarks>
    public float RightDelta(float threshold)
    {
        float current = _current.Right < threshold ? 0.0f : _current.Right;
        float previous = _previous.Right < threshold ? 0.0f : _previous.Right;
        return current - previous;
    }

    /// <summary>
    /// Gets a value indicating whether the right trigger position has changed between the previous and current frames.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the right trigger has moved; otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasRightMoved() => RightDelta() != 0.0f;

    /// <summary>
    /// Gets a value indicating whether the right trigger position has changed between the previous and current frames,
    /// with threshold filtering.
    /// </summary>
    /// <param name="threshold">The minimum trigger value required for activation when calculating movement.</param>
    /// <returns>
    /// <see langword="true"/> if the right trigger has moved beyond the threshold; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method applies threshold filtering when determining movement, helping to eliminate false positives
    /// from very light trigger touches or minor pressure variations that don't represent intentional input.
    /// </remarks>
    public bool HasRightMoved(float threshold) => RightDelta(threshold) != 0.0f;

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
