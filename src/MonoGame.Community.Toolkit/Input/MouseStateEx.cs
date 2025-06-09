using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Represents an extended mouse state that tracks both current and previous frame mouse input, enabling detection of
/// button press and release events, position changes, and scroll wheel deltas.
/// </summary>
/// <remarks>
/// This structure extends the functionality of <see cref="MouseState"/> by maintaining both current and previous mouse
/// states, allowing for detection of button press and release transitions, mouse movement, and scroll wheel changes
/// that occur between frames.
/// </remarks>
public readonly struct MouseStateEx
{
    /// <summary>
    /// The current frame's mouse state.
    /// </summary>
    /// <value>
    /// A <see cref="MouseState"/> representing the mouse state for the current frame.
    /// </value>
    public readonly MouseState Current;

    /// <summary>
    /// The previous frame's mouse state.
    /// </summary>
    /// <value>
    /// A <see cref="MouseState"/> representing the mouse state for the previous frame.
    /// </value>
    public readonly MouseState Previous;

    /// <summary>
    /// Gets the horizontal position of the cursor in relation to the window.
    /// </summary>
    /// <value>
    /// The horizontal position of the cursor.
    /// </value>
    public readonly int X => Current.X;

    /// <summary>
    /// Gets the vertical position of the cursor in relation to the window.
    /// </summary>
    /// <value>
    /// The vertical position of the cursor.
    /// </value>
    public readonly int Y => Current.Y;

    /// <summary>
    /// Gets the current cursor position.
    /// </summary>
    /// <value>
    /// A <see cref="Point"/> representing the current cursor position.
    /// </value>
    public readonly Point Position => Current.Position;

    /// <summary>
    /// Gets a value indicating whether the mouse position has changed between the previous and current frames.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the mouse position has changed; otherwise, <see langword="false"/>.
    /// </value>
    public readonly bool HasPositionChanged => Current.Position != Previous.Position;

    /// <summary>
    /// Gets the change in mouse position between the previous and current frames.
    /// </summary>
    /// <value>
    /// A <see cref="Point"/> representing the position delta (previous position minus current position).
    /// </value>
    /// <remarks>
    /// This value represents how much the mouse has moved since the previous frame. Positive X values indicate movement
    /// to the right, negative X values indicate movement to the left. Positive Y values indicate movement down,
    /// negative Y values indicate movement up.
    /// </remarks>
    public readonly Point PositionDelta => Previous.Position - Current.Position;

    /// <summary>
    /// Gets the extended state information for the specified mouse button.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value that contains information about whether the button is currently down, up, was just pressed, or was just released.
    /// </value>
    /// <remarks>
    /// The returned <see cref="ButtonStateEx"/> value can contain multiple flags. For example, a button that was just
    /// pressed will have both <see cref="ButtonStateEx.Down"/> and <see cref="ButtonStateEx.Pressed"/> flags set.
    /// </remarks>
    public readonly ButtonStateEx this[MouseButton button]
    {
        get
        {
            bool currentlyDown = button switch
            {
                MouseButton.LeftButton => Current.LeftButton == ButtonState.Pressed,
                MouseButton.MiddleButton => Current.MiddleButton == ButtonState.Pressed,
                MouseButton.RightButton => Current.RightButton == ButtonState.Pressed,
                MouseButton.XButton1 => Current.XButton1 == ButtonState.Pressed,
                MouseButton.XButton2 => Current.XButton2 == ButtonState.Pressed,
                _ => false
            };

            bool previouslyDown = button switch
            {
                MouseButton.LeftButton => Previous.LeftButton == ButtonState.Pressed,
                MouseButton.MiddleButton => Previous.MiddleButton == ButtonState.Pressed,
                MouseButton.RightButton => Previous.RightButton == ButtonState.Pressed,
                MouseButton.XButton1 => Previous.XButton1 == ButtonState.Pressed,
                MouseButton.XButton2 => Previous.XButton2 == ButtonState.Pressed,
                _ => false
            };

            ButtonStateEx state = currentlyDown ? ButtonStateEx.Down : ButtonStateEx.Up;

            if (currentlyDown && !previouslyDown)
            {
                state |= ButtonStateEx.Pressed;
            }
            else if (!currentlyDown && previouslyDown)
            {
                state |= ButtonStateEx.Released;
            }

            return state;
        }
    }

    /// <summary>
    /// Gets the extended state information for the left mouse button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the left mouse button.
    /// </value>
    public readonly ButtonStateEx LeftButton => this[MouseButton.LeftButton];

    /// <summary>
    /// Gets the extended state information for the middle mouse button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the middle mouse button.
    /// </value>
    public readonly ButtonStateEx MiddleButton => this[MouseButton.MiddleButton];

    /// <summary>
    /// Gets the extended state information for the right mouse button.
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of the right mouse button.
    /// </value>
    public readonly ButtonStateEx RightButton => this[MouseButton.RightButton];

    /// <summary>
    /// Gets the extended state information for the first extended mouse button (XButton1).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of XButton1.
    /// </value>
    public readonly ButtonStateEx XButton1 => this[MouseButton.XButton1];

    /// <summary>
    /// Gets the extended state information for the second extended mouse button (XButton2).
    /// </summary>
    /// <value>
    /// A <see cref="ButtonStateEx"/> value representing the current state of XButton2.
    /// </value>
    public readonly ButtonStateEx XButton2 => this[MouseButton.XButton2];

    /// <summary>
    /// Gets the cumulative vertical scroll wheel value since the game start.
    /// </summary>
    /// <value>
    /// The current vertical scroll wheel value.
    /// </value>
    /// <remarks>
    /// This value is incremented when the wheel is scrolled up and decremented when scrolled down.
    /// To detect scroll movement between frames, use <see cref="ScrollWheelDelta"/>.
    /// </remarks>
    public readonly int ScrollWheelValue => Current.ScrollWheelValue;

    /// <summary>
    /// Gets the change in vertical scroll wheel value between the previous and current frames.
    /// </summary>
    /// <value>
    /// The vertical scroll wheel delta (previous value minus current value).
    /// </value>
    /// <remarks>
    /// Positive values indicate the wheel was scrolled up, negative values indicate the wheel was scrolled down.
    /// A value of zero indicates no scroll movement occurred.
    /// </remarks>
    public readonly int ScrollWheelDelta => Previous.ScrollWheelValue - Current.ScrollWheelValue;

    /// <summary>
    /// Gets the cumulative horizontal scroll wheel value since the game start.
    /// </summary>
    /// <value>
    /// The current horizontal scroll wheel value.
    /// </value>
    /// <remarks>
    /// This value is incremented when the wheel is scrolled left and decremented when scrolled right.
    /// To detect horizontal scroll movement between frames, use <see cref="HorizontalScrollWheelDelta"/>.
    /// </remarks>
    public readonly int HorizontalScrollWheelValue => Current.HorizontalScrollWheelValue;

    /// <summary>
    /// Gets the change in horizontal scroll wheel value between the previous and current frames.
    /// </summary>
    /// <value>
    /// The horizontal scroll wheel delta (previous value minus current value).
    /// </value>
    /// <remarks>
    /// Positive values indicate the wheel was scrolled left, negative values indicate the wheel was scrolled right.
    /// A value of zero indicates no horizontal scroll movement occurred.
    /// </remarks>
    public readonly int HorizontalScrollWheelDelta => Previous.HorizontalScrollWheelValue - Current.HorizontalScrollWheelValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="MouseStateEx"/> structure.
    /// </summary>
    /// <param name="current">The current frame's mouse state.</param>
    /// <param name="previous">The previous frame's mouse state.</param>
    public MouseStateEx(MouseState current, MouseState previous)
    {
        Current = current;
        Previous = previous;
    }

    /// <summary>
    /// Determines whether the specified mouse button is currently being pressed down.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button is currently down; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool IsButtonDown(MouseButton button) => button switch
    {
        MouseButton.LeftButton => Current.LeftButton == ButtonState.Pressed,
        MouseButton.MiddleButton => Current.MiddleButton == ButtonState.Pressed,
        MouseButton.RightButton => Current.RightButton == ButtonState.Pressed,
        MouseButton.XButton1 => Current.XButton1 == ButtonState.Pressed,
        MouseButton.XButton2 => Current.XButton2 == ButtonState.Pressed,
        _ => false
    };

    /// <summary>
    /// Determines whether the specified mouse button is currently up (not being pressed).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button is currently up; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool IsButtonUp(MouseButton button) => button switch
    {
        MouseButton.LeftButton => Current.LeftButton == ButtonState.Released,
        MouseButton.MiddleButton => Current.MiddleButton == ButtonState.Released,
        MouseButton.RightButton => Current.RightButton == ButtonState.Released,
        MouseButton.XButton1 => Current.XButton1 == ButtonState.Released,
        MouseButton.XButton2 => Current.XButton2 == ButtonState.Released,
        _ => false
    };

    /// <summary>
    /// Determines whether the specified mouse button was just pressed (transitioned from up to down between the
    /// previous and current frames).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A button is considered "pressed" when it is down in the current state but was up in the previous state.
    /// This method is useful for detecting single button press events rather than continuous button holding.
    /// </remarks>
    public readonly bool IsButtonPressed(MouseButton button) => button switch
    {
        MouseButton.LeftButton => Current.LeftButton == ButtonState.Pressed && Previous.LeftButton == ButtonState.Released,
        MouseButton.MiddleButton => Current.MiddleButton == ButtonState.Pressed && Previous.MiddleButton == ButtonState.Released,
        MouseButton.RightButton => Current.RightButton == ButtonState.Pressed && Previous.RightButton == ButtonState.Released,
        MouseButton.XButton1 => Current.XButton1 == ButtonState.Pressed && Previous.XButton1 == ButtonState.Released,
        MouseButton.XButton2 => Current.XButton2 == ButtonState.Pressed && Previous.XButton2 == ButtonState.Released,
        _ => false
    };

    /// <summary>
    /// Determines whether the specified mouse button was just released (transitioned from down to up between the
    /// previous and current frames).
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button was just released; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A button is considered "released" when it is up in the current state but was down in the previous state.
    /// This method is useful for detecting button release events.
    /// </remarks>
    public readonly bool IsButtonReleased(MouseButton button) => button switch
    {
        MouseButton.LeftButton => Current.LeftButton == ButtonState.Released && Previous.LeftButton == ButtonState.Pressed,
        MouseButton.MiddleButton => Current.MiddleButton == ButtonState.Released && Previous.MiddleButton == ButtonState.Pressed,
        MouseButton.RightButton => Current.RightButton == ButtonState.Released && Previous.RightButton == ButtonState.Pressed,
        MouseButton.XButton1 => Current.XButton1 == ButtonState.Released && Previous.XButton1 == ButtonState.Pressed,
        MouseButton.XButton2 => Current.XButton2 == ButtonState.Released && Previous.XButton2 == ButtonState.Pressed,
        _ => false
    };
}
