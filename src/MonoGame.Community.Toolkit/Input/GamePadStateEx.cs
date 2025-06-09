using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended gamepad state that tracks both current and previous frame gamepad input, providing
/// comprehensive access to all gamepad components with press and release detection capabilities.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="GamePadState"/> by maintaining both current and previous
/// gamepad states, enabling detection of input transitions across all gamepad components including buttons, D-pad,
/// thumbsticks, and triggers.
/// </remarks>
public readonly struct GamePadStateEx : IEquatable<GamePadStateEx>
{
    /// <summary>
    /// The current frame's gamepad state.
    /// </summary>
    /// <value>
    /// A <see cref="GamePadState"/> representing the gamepad state for the current frame.
    /// </value>
    public readonly GamePadState CurrentState;

    /// <summary>
    /// The previous frame's gamepad state.
    /// </summary>
    /// <value>
    /// A <see cref="GamePadState"/> representing the gamepad state for the previous frame.
    /// </value>
    public readonly GamePadState PreviousState;

    /// <summary>
    /// The player index associated with this gamepad state.
    /// </summary>
    /// <value>
    /// A <see cref="PlayerIndex"/> value indicating which player this gamepad state represents.
    /// </value>
    public readonly PlayerIndex PlayerIndex;

    /// <summary>
    /// Gets a value indicating whether the gamepad is currently connected.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the gamepad is connected; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool IsConnected => CurrentState.IsConnected;

    /// <summary>
    /// Gets the packet number which indicates the number of times the gamepad has been updated.
    /// </summary>
    /// <value>
    /// An <see cref="int"/> representing the current packet number from the gamepad driver.
    /// </value>
    public readonly int PacketNumber => CurrentState.PacketNumber;

    /// <summary>
    /// Gets the extended buttons state with press and release detection capabilities.
    /// </summary>
    /// <value>
    /// A <see cref="GamePadButtonsEx"/> structure providing extended button state information.
    /// </value>
    public readonly GamePadButtonsEx Buttons;

    /// <summary>
    /// Gets the extended directional pad state with press and release detection capabilities.
    /// </summary>
    /// <value>
    /// A <see cref="GamePadDPadEx"/> structure providing extended D-pad state information.
    /// </value>
    public readonly GamePadDPadEx DPad;

    /// <summary>
    /// Gets the extended thumbsticks state with movement detection and delta calculation capabilities.
    /// </summary>
    /// <value>
    /// A <see cref="GamePadThumbSticksEx"/> structure providing extended thumbstick state information.
    /// </value>
    public readonly GamePadThumbSticksEx ThumbSticks;

    /// <summary>
    /// Gets the extended triggers state with movement detection and delta calculation capabilities.
    /// </summary>
    /// <value>
    /// A <see cref="GamePadTriggersEx"/> structure providing extended trigger state information.
    /// </value>
    public readonly GamePadTriggersEx Triggers;

    /// <summary>
    /// Initializes a new instance of the <see cref="GamePadStateEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's gamepad state.</param>
    /// <param name="previous">The previous frame's gamepad state.</param>
    public GamePadStateEx(PlayerIndex playerIndex, GamePadState current, GamePadState previous)
    {
        PlayerIndex = playerIndex;
        CurrentState = current;
        PreviousState = previous;
        Buttons = new GamePadButtonsEx(current.Buttons, previous.Buttons);
        DPad = new GamePadDPadEx(current.DPad, previous.DPad);
        ThumbSticks = new GamePadThumbSticksEx(current.ThumbSticks, previous.ThumbSticks);
        Triggers = new GamePadTriggersEx(current.Triggers, previous.Triggers);
    }

    /// <summary>
    /// Determines whether the specified button is currently being pressed down.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button is currently down; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool IsButtonDown(Buttons button) => CurrentState.IsButtonDown(button);

    /// <summary>
    /// Determines whether the specified button is currently up (not being pressed).
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button is currently up; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool IsButtonUp(Buttons button) => CurrentState.IsButtonUp(button);

    /// <summary>
    /// Determines whether the specified button was just pressed (transitioned from up to down between the previous and current frames).
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A button is considered "pressed" when it is down in the current state but was up in the previous state.
    /// This method is useful for detecting single button press events rather than continuous button holding.
    /// </remarks>
    public readonly bool WasButtonPressed(Buttons button) => CurrentState.IsButtonDown(button) && PreviousState.IsButtonUp(button);

    /// <summary>
    /// Determines whether the specified button was just released (transitioned from down to up between the previous and current frames).
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button was just released; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A button is considered "released" when it is up in the current state but was down in the previous state.
    /// This method is useful for detecting button release events.
    /// </remarks>
    public readonly bool WasButtonReleased(Buttons button) => CurrentState.IsButtonUp(button) && PreviousState.IsButtonDown(button);

    /// <summary>
    /// Determines the directional input from gamepad buttons, return -1, 0, or 1;
    /// </summary>
    /// <param name="negative">The button representing negative direction.</param>
    /// <param name="positive">The button representing positive direction.</param>
    /// <param name="bothValue">The value to return when both buttons are pressed. Default is 0.</param>
    /// <returns>
    /// -1 if only the negative button is pressed, 1 if only the positive button is pressed,
    /// <paramref name="bothValue"/> if both are pressed, or 0 if neither is pressed.
    // </returns>
    public int GetAxis(Buttons negative, Buttons positive, int bothValue = 0)
    {
        bool negativeDown = IsButtonDown(negative);
        bool positiveDown = IsButtonDown(positive);
        if (negativeDown && positiveDown) { return bothValue; }
        if (negativeDown) { return -1; }
        if (positiveDown) { return 1; }
        return 0;
    }

    /// <summary>
    /// Sets the vibration motor speeds on the gamepad.
    /// </summary>
    /// <param name="leftMotor">The speed of the left motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightMotor">The speed of the right motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <returns>
    /// <see langword="true"/> if the vibration was set successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The left motor typically provides low-frequency rumble, while the right motor provides high-frequency rumble.
    /// Vibration will continue until explicitly stopped by calling this method with 0.0 values or until the gamepad is disconnected.
    /// </remarks>
    public readonly bool SetVibration(float leftMotor, float rightMotor) =>
        GamePad.SetVibration(PlayerIndex, leftMotor, rightMotor);


    /// <summary>
    /// Sets the vibration motor speeds on the gamepad, including trigger motors if supported.
    /// </summary>
    /// <param name="leftMotor">The speed of the left motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightMotor">The speed of the right motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="leftTrigger">The speed of the left trigger motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightTrigger">The speed of the right trigger motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <returns>
    /// <see langword="true"/> if the vibration was set successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The left motor typically provides low-frequency rumble, while the right motor provides high-frequency rumble.
    /// Trigger motors provide localized haptic feedback in the triggers and may not be supported on all gamepad models.
    /// Vibration will continue until explicitly stopped by calling this method with 0.0 values or until the gamepad is disconnected.
    /// </remarks>
    public readonly bool SetVibration(float leftMotor, float rightMotor, float leftTrigger, float rightTrigger) =>
        GamePad.SetVibration(PlayerIndex, leftMotor, rightMotor, leftTrigger, rightTrigger);


    /// <summary>
    /// Stops all vibration on the gamepad by setting all motor speeds to zero.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the vibration was stopped successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method provides a convenient way to immediately stop all gamepad vibration, including both main motors
    /// and trigger motors if supported. It is equivalent to calling <see cref="SetVibration(float, float, float, float)"/>
    /// with all parameters set to 0.0.
    /// </remarks>
    public readonly bool StopVibration() => SetVibration(0.0f, 0.0f, 0.0f, 0.0f);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is GamePadStateEx other && Equals(other);

    /// <inheritdoc/>
    public readonly bool Equals(GamePadStateEx other) => CurrentState.Equals(other.CurrentState) && PreviousState.Equals(other.PreviousState);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(CurrentState, PreviousState);

    /// <inheritdoc/>
    public static bool operator ==(GamePadStateEx lhs, GamePadState rhs) => lhs.Equals(rhs);

    /// <inheritdoc/>
    public static bool operator !=(GamePadStateEx lhs, GamePadState rhs) => !lhs.Equals(rhs);
}
