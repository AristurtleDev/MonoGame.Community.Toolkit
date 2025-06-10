using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Provides extended gamepad functionality with automatic frame-to-frame state tracking and enhanced input detection
/// capabilities.
/// </summary>
/// <remarks>
/// This static class extends the functionality of the standard <see cref="GamePad"/> class by automatically maintaining
/// previous frame state information, enabling detection of button press and release transitions, analog input deltas,
/// and movement detection across all gamepad components. It provides drop-in replacements for <see cref="GamePad.GetState(PlayerIndex)"/>
/// methods that return <see cref="GamePadStateEx"/> instances with enhanced input detection capabilities, along with
/// convenient vibration control methods.
/// </remarks>
public static class GamePadEx
{
    private static GamePadState s_previousState = new GamePadState();

    /// <summary>
    /// Gets the maximum number of gamepads supported by the system.
    /// </summary>
    /// <value>
    /// An <see cref="int"/> representing the maximum number of gamepads that can be connected simultaneously.
    /// </value>
    public static int MaximumGamePadCount => GamePad.MaximumGamePadCount;

    /// <summary>
    /// Gets the capabilities of the gamepad associated with the specified player index.
    /// </summary>
    /// <param name="playerIndex">The player index to query capabilities for.</param>
    /// <returns>
    /// A <see cref="GamePadCapabilities"/> structure describing the gamepad's supported features.
    /// </returns>
    public static GamePadCapabilities GetCapabilities(PlayerIndex playerIndex) => GamePad.GetCapabilities(playerIndex);

    /// <summary>
    /// Gets the capabilities of the gamepad at the specified index.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to query capabilities for.</param>
    /// <returns>
    /// A <see cref="GamePadCapabilities"/> structure describing the gamepad's supported features.
    /// </returns>
    public static GamePadCapabilities GetCapabilities(int index) => GamePad.GetCapabilities(index);

    /// <summary>
    /// Gets the current extended gamepad state for the specified player with automatic previous frame tracking.
    /// </summary>
    /// <param name="playerIndex">The player index to get the gamepad state for.</param>
    /// <returns>
    /// A <see cref="GamePadStateEx"/> structure containing current and previous frame gamepad state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's gamepad state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static GamePadStateEx GetState(PlayerIndex playerIndex)
    {
        GamePadState current = GamePad.GetState(playerIndex);
        GamePadStateEx state = new GamePadStateEx(playerIndex, current, s_previousState);
        s_previousState = current;
        return state;
    }

    /// <summary>
    /// Gets the current extended gamepad state for the specified index with automatic previous frame tracking.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to get the state for.</param>
    /// <returns>
    /// A <see cref="GamePadStateEx"/> structure containing current and previous frame gamepad state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's gamepad state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static GamePadStateEx GetState(int index)
    {
        GamePadState current = GamePad.GetState(index);
        GamePadStateEx state = new GamePadStateEx((PlayerIndex)index, current, s_previousState);
        s_previousState = current;
        return state;
    }

    /// <summary>
    /// Gets the current extended gamepad state for the specified player with the specified dead zone mode and automatic
    /// previous frame tracking.
    /// </summary>
    /// <param name="playerIndex">The player index to get the gamepad state for.</param>
    /// <param name="deadZoneMode">The dead zone mode to apply to both thumbsticks.</param>
    /// <returns>
    /// A <see cref="GamePadStateEx"/> structure containing current and previous frame gamepad state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's gamepad state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static GamePadStateEx GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
    {
        GamePadState current = GamePad.GetState(playerIndex, deadZoneMode);
        GamePadStateEx state = new GamePadStateEx(playerIndex, current, s_previousState);
        s_previousState = current;
        return state;
    }

    /// <summary>
    /// Gets the current extended gamepad state for the specified index with the specified dead zone mode and automatic
    /// previous frame tracking.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to get the state for.</param>
    /// <param name="deadZoneMode">The dead zone mode to apply to both thumbsticks.</param>
    /// <returns>
    /// A <see cref="GamePadStateEx"/> structure containing current and previous frame gamepad state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's gamepad state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static GamePadStateEx GetState(int index, GamePadDeadZone deadZoneMode)
    {
        GamePadState current = GamePad.GetState(index, deadZoneMode);
        GamePadStateEx state = new GamePadStateEx((PlayerIndex)index, current, s_previousState);
        s_previousState = current;
        return state;
    }

    /// <summary>
    /// Gets the current extended gamepad state for the specified player with independent dead zone modes for each
    /// thumbstick and automatic previous frame tracking.
    /// </summary>
    /// <param name="playerIndex">The player index to get the gamepad state for.</param>
    /// <param name="leftDeadZoneMode">The dead zone mode to apply to the left thumbstick.</param>
    /// <param name="rightDeadZoneMode">The dead zone mode to apply to the right thumbstick.</param>
    /// <returns>
    /// A <see cref="GamePadStateEx"/> structure containing current and previous frame gamepad state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's gamepad state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static GamePadStateEx GetState(PlayerIndex playerIndex, GamePadDeadZone leftDeadZoneMode, GamePadDeadZone rightDeadZoneMode)
    {
        GamePadState current = GamePad.GetState(playerIndex, leftDeadZoneMode, rightDeadZoneMode);
        GamePadStateEx state = new GamePadStateEx(playerIndex, current, s_previousState);
        s_previousState = current;
        return state;
    }

    /// <summary>
    /// Gets the current extended gamepad state for the specified index with independent dead zone modes for each
    /// thumbstick and automatic previous frame tracking.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to get the state for.</param>
    /// <param name="leftDeadZoneMode">The dead zone mode to apply to the left thumbstick.</param>
    /// <param name="rightDeadZoneMode">The dead zone mode to apply to the right thumbstick.</param>
    /// <returns>
    /// A <see cref="GamePadStateEx"/> structure containing current and previous frame gamepad state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's gamepad state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static GamePadStateEx GetState(int index, GamePadDeadZone leftDeadZoneMode, GamePadDeadZone rightDeadZoneMode)
    {
        GamePadState current = GamePad.GetState(index, leftDeadZoneMode, rightDeadZoneMode);
        GamePadStateEx state = new GamePadStateEx((PlayerIndex)index, current, s_previousState);
        s_previousState = current;
        return state;
    }

    /// <summary>
    /// Sets the vibration motor speeds on the gamepad for the specified player.
    /// </summary>
    /// <param name="playerIndex">The player index to set vibration for.</param>
    /// <param name="leftMotor">The speed of the left motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightMotor">The speed of the right motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <returns><see langword="true"/> if the vibration was set successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// The left motor typically provides low-frequency rumble, while the right motor provides high-frequency rumble.
    /// </remarks>
    public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor) =>
        GamePad.SetVibration(playerIndex, leftMotor, rightMotor);

    /// <summary>
    /// Sets the vibration motor speeds on the gamepad for the specified player, including trigger motors if supported.
    /// </summary>
    /// <param name="playerIndex">The player index to set vibration for.</param>
    /// <param name="leftMotor">The speed of the left motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightMotor">The speed of the right motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="leftTrigger">The speed of the left trigger motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightTrigger">The speed of the right trigger motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <returns><see langword="true"/> if the vibration was set successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// The left motor typically provides low-frequency rumble, while the right motor provides high-frequency rumble.
    /// Trigger motors provide localized haptic feedback and may not be supported on all gamepad models.
    /// </remarks>
    public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor, float leftTrigger, float rightTrigger) =>
        GamePad.SetVibration(playerIndex, leftMotor, rightMotor, leftTrigger, rightTrigger);

    /// <summary>
    /// Sets the vibration motor speeds on the gamepad at the specified index.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to set vibration for.</param>
    /// <param name="leftMotor">The speed of the left motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightMotor">The speed of the right motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <returns><see langword="true"/> if the vibration was set successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// The left motor typically provides low-frequency rumble, while the right motor provides high-frequency rumble.
    /// </remarks>
    public static bool SetVibration(int index, float leftMotor, float rightMotor) =>
        GamePad.SetVibration(index, leftMotor, rightMotor);

    /// <summary>
    /// Sets the vibration motor speeds on the gamepad at the specified index, including trigger motors if supported.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to set vibration for.</param>
    /// <param name="leftMotor">The speed of the left motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightMotor">The speed of the right motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="leftTrigger">The speed of the left trigger motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <param name="rightTrigger">The speed of the right trigger motor, ranging from 0.0 (off) to 1.0 (maximum speed).</param>
    /// <returns><see langword="true"/> if the vibration was set successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// The left motor typically provides low-frequency rumble, while the right motor provides high-frequency rumble.
    /// Trigger motors provide localized haptic feedback and may not be supported on all gamepad models.
    /// </remarks>
    public static bool SetVibration(int index, float leftMotor, float rightMotor, float leftTrigger, float rightTrigger) =>
        GamePad.SetVibration(index, leftMotor, rightMotor, leftTrigger, rightTrigger);

    /// <summary>
    /// Stops all vibration on the gamepad for the specified player.
    /// </summary>
    /// <param name="playerIndex">The player index to stop vibration for.</param>
    /// <returns><see langword="true"/> if the vibration was stopped successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// This method provides a convenient way to immediately stop all gamepad vibration, including both main motors
    /// and trigger motors if supported. It is equivalent to calling <see cref="SetVibration(PlayerIndex, float, float, float, float)"/>
    /// with all parameters set to 0.0.
    /// </remarks>
    public static bool StopVibration(PlayerIndex playerIndex) => GamePad.SetVibration(playerIndex, 0.0f, 0.0f, 0.0f, 0.0f);

    /// <summary>
    /// Stops all vibration on the gamepad at the specified index.
    /// </summary>
    /// <param name="index">The zero-based gamepad index to stop vibration for.</param>
    /// <returns><see langword="true"/> if the vibration was stopped successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// This method provides a convenient way to immediately stop all gamepad vibration, including both main motors
    /// and trigger motors if supported. It is equivalent to calling <see cref="SetVibration(int, float, float, float, float)"/>
    /// with all parameters set to 0.0.
    /// </remarks>
    public static bool StopVibration(int index) => GamePad.SetVibration(index, 0.0f, 0.0f, 0.0f, 0.0f);

}
