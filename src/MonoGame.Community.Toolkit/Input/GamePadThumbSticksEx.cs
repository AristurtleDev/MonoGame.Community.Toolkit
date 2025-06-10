using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended gamepad thumbsticks state that tracks both current and previous frame analog stick positions,
/// enabling detection of stick movement and delta calculations.
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
    /// Initializes a new instance of the <see cref="GamePadThumbSticksEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's thumbsticks state.</param>
    /// <param name="previous">The previous frame's thumbsticks state.</param>
    public GamePadThumbSticksEx(GamePadThumbSticks current, GamePadThumbSticks previous)
    {
        _current = current;
        _previous = previous;
    }

    /// <summary>
    /// Gets the current position of the left thumbstick.
    /// </summary>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the left thumbstick, where X and Y components range
    /// from -1.0 to 1.0.
    /// </returns>
    public Vector2 Left() => Left(false);

    /// <summary>
    /// Gets the current position of the left thumbstick with optional Y-axis inversion.
    /// </summary>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis (make positive values indicate down); otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the left thumbstick, where X and Y components range
    /// from -1.0 to 1.0.
    /// </returns>
    /// <remarks>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </remarks>
    public Vector2 Left(bool invertY)
    {
        Vector2 value = _current.Left;

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets the current position of the left thumbstick with dead zone filtering.
    /// </summary>
    /// <param name="deadZone">The dead zone radius. Values with magnitude less than this will be treated as zero.</param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the left thumbstick after dead zone filtering.
    /// </returns>
    /// <remarks>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </remarks>
    public Vector2 Left(float deadZone) => Left(deadZone, false);

    /// <summary>
    /// Gets the current position of the left thumbstick with dead zone filtering and optional Y-axis inversion.
    /// </summary>
    /// <param name="deadZone">The dead zone radius. Values with magnitude less than this will be treated as zero.</param>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis (make positive values indicate down); otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the left thumbstick after dead zone filtering and
    /// Y-axis processing.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </para>
    /// <para>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </para>
    /// Dead zone filtering is applied first, then Y-axis inversion if requested.
    /// </remarks>
    public Vector2 Left(float deadZone, bool invertY)
    {
        Vector2 value = _current.Left;
        float lenSquared = value.LengthSquared();
        float deadZoneSquared = deadZone * deadZone;

        if (lenSquared < deadZoneSquared)
        {
            value = Vector2.Zero;
        }

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets the change in position of the left thumbstick between the previous and current frames.
    /// </summary>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the left thumbstick (current position minus previous
    /// position).
    /// </returns>
    /// <remarks>
    /// This value represents how much the left thumbstick has moved since the previous frame. A zero vector indicates
    /// no movement.
    /// </remarks>
    public Vector2 LeftDelta() => _current.Left - _previous.Left;

    /// <summary>
    /// Gets the change in position of the left thumbstick between the previous and current frames with optional Y-axis
    /// inversion.
    /// </summary>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis of the delta; otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the left thumbstick with Y-axis processing applied.
    /// </returns>
    /// <remarks>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </remarks>
    public Vector2 LeftDelta(bool invertY)
    {
        Vector2 value = _current.Left - _previous.Left;

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets the change in position of the left thumbstick between the previous and current frames with dead zone
    /// filtering.
    /// </summary>
    /// <param name="deadZone">The dead zone radius applied to both current and previous positions.</param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the left thumbstick after dead zone filtering.
    /// </returns>
    /// <remarks>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </remarks>
    public Vector2 LeftDelta(float deadZone) => LeftDelta(deadZone, false);

    /// <summary>
    /// Gets the change in position of the left thumbstick between the previous and current frames with dead zone
    /// filtering and optional Y-axis inversion.
    /// </summary>
    /// <param name="deadZone">The dead zone radius applied to both current and previous positions.</param>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis of the delta; otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the left thumbstick after dead zone filtering and
    /// Y-axis processing.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </para>
    /// <para>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </para>
    /// Dead zone filtering is applied first, then Y-axis inversion if requested.
    /// </remarks>
    public Vector2 LeftDelta(float deadZone, bool invertY)
    {
        Vector2 current = _current.Left;
        Vector2 previous = _previous.Left;

        float currentLengthSquared = current.LengthSquared();
        float previousLengthSquared = previous.LengthSquared();
        float deadZoneSquared = deadZone * deadZone;

        if (currentLengthSquared < deadZoneSquared)
        {
            current = Vector2.Zero;
        }

        if (previousLengthSquared < deadZoneSquared)
        {
            previous = Vector2.Zero;
        }

        Vector2 value = current - previous;

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets a value indicating whether the left thumbstick position has changed between the previous and current frames.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the left thumbstick has moved; otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasLeftMoved() => LeftDelta() != Vector2.Zero;

    /// <summary>
    /// Gets a value indicating whether the left thumbstick position has changed between the previous and current
    /// frames, with dead zone filtering.
    /// </summary>
    /// <param name="deadZone">The dead zone radius applied when calculating movement.</param>
    /// <returns>
    /// <see langword="true"/> if the left thumbstick has moved beyond the dead zone; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method applies dead zone filtering when determining movement, helping to eliminate false positives
    /// from controller drift or very small unintentional movements.
    /// </remarks>
    public bool HasLeftMoved(float deadZone) => LeftDelta(deadZone) != Vector2.Zero;

    /// <summary>
    /// Gets the current position of the right thumbstick.
    /// </summary>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the right thumbstick, where X and Y components
    /// range from -1.0 to 1.0.
    /// </returns>
    public Vector2 Right() => Right(false);

    /// <summary>
    /// Gets the current position of the right thumbstick with optional Y-axis inversion.
    /// </summary>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis (make positive values indicate down); otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the right thumbstick, where X and Y components
    /// range from -1.0 to 1.0.
    /// </returns>
    /// <remarks>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </remarks>
    public Vector2 Right(bool invertY)
    {
        Vector2 value = _current.Right;

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets the current position of the right thumbstick with dead zone filtering.
    /// </summary>
    /// <param name="deadZone">The dead zone radius. Values with magnitude less than this will be treated as zero.</param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the right thumbstick after dead zone filtering.
    /// </returns>
    /// <remarks>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </remarks>
    public Vector2 Right(float deadZone) => Right(deadZone, false);

    /// <summary>
    /// Gets the current position of the right thumbstick with dead zone filtering and optional Y-axis inversion.
    /// </summary>
    /// <param name="deadZone">The dead zone radius. Values with magnitude less than this will be treated as zero.</param>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis (make positive values indicate down); otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the current position of the right thumbstick after dead zone filtering and
    /// Y-axis processing.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </para>
    /// <para>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </para>
    /// Dead zone filtering is applied first, then Y-axis inversion if requested.
    /// </remarks>
    public Vector2 Right(float deadZone, bool invertY)
    {
        Vector2 value = _current.Right;
        float lenSquared = value.LengthSquared();
        float deadZoneSquared = deadZone * deadZone;

        if (lenSquared < deadZoneSquared)
        {
            value = Vector2.Zero;
        }

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets the change in position of the right thumbstick between the previous and current frames.
    /// </summary>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the right thumbstick (current position minus previous
    ///  position).
    /// </returns>
    /// <remarks>
    /// This value represents how much the right thumbstick has moved since the previous frame. A zero vector indicates
    /// no movement.
    /// </remarks>
    public Vector2 RightDelta() => _current.Right - _previous.Right;

    /// <summary>
    /// Gets the change in position of the right thumbstick between the previous and current frames with optional Y-axis
    /// inversion.
    /// </summary>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis of the delta; otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the right thumbstick with Y-axis processing applied.
    /// </returns>
    /// <remarks>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </remarks>
    public Vector2 RightDelta(bool invertY)
    {
        Vector2 value = _current.Right - _previous.Right;

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets the change in position of the right thumbstick between the previous and current frames with dead zone
    /// filtering.
    /// </summary>
    /// <param name="deadZone">The dead zone radius applied to both current and previous positions.</param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the right thumbstick after dead zone filtering.
    /// </returns>
    /// <remarks>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </remarks>
    public Vector2 RightDelta(float deadZone) => RightDelta(deadZone, false);

    /// <summary>
    /// Gets the change in position of the right thumbstick between the previous and current frames with dead zone
    /// filtering and optional Y-axis inversion.
    /// </summary>
    /// <param name="deadZone">The dead zone radius applied to both current and previous positions.</param>
    /// <param name="invertY">
    /// <see langword="true"/> to invert the Y-axis of the delta; otherwise, <see langword="false"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Vector2"/> representing the movement delta of the right thumbstick after dead zone filtering and
    /// Y-axis processing.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Y-axis inversion is commonly used in MonoGame since the 2D coordinate system uses negative Y for up movement,
    /// making inversion necessary for natural input behavior. In 3D scenarios, it's also used for camera controls
    /// where pushing the stick up should look down, similar to aircraft controls.
    /// </para>
    /// <para>
    /// Dead zone filtering helps eliminate controller drift and unintended small movements. If the thumbstick's
    /// magnitude is less than the specified dead zone, <see cref="Vector2.Zero"/> is returned.
    /// </para>
    /// Dead zone filtering is applied first, then Y-axis inversion if requested.
    /// </remarks>
    public Vector2 RightDelta(float deadZone, bool invertY)
    {
        Vector2 current = _current.Right;
        Vector2 previous = _previous.Right;

        float currentLengthSquared = current.LengthSquared();
        float previousLengthSquared = previous.LengthSquared();
        float deadZoneSquared = deadZone * deadZone;

        if (currentLengthSquared < deadZoneSquared)
        {
            current = Vector2.Zero;
        }

        if (previousLengthSquared < deadZoneSquared)
        {
            previous = Vector2.Zero;
        }

        Vector2 value = current - previous;

        if (invertY)
        {
            value.Y = -value.Y;
        }

        return value;
    }

    /// <summary>
    /// Gets a value indicating whether the right thumbstick position has changed between the previous and current
    /// frames.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the right thumbstick has moved; otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasRightMoved() => RightDelta() != Vector2.Zero;

    /// <summary>
    /// Gets a value indicating whether the right thumbstick position has changed between the previous and current
    /// frames, with dead zone filtering.
    /// </summary>
    /// <param name="deadZone">The dead zone radius applied when calculating movement.</param>
    /// <returns>
    /// <see langword="true"/> if the right thumbstick has moved beyond the dead zone; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method applies dead zone filtering when determining movement, helping to eliminate false positives
    /// from controller drift or very small unintentional movements.
    /// </remarks>
    public bool HasRightMoved(float deadZone) => RightDelta(deadZone) != Vector2.Zero;

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
