using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Provides event-driven mouse input handling with support for clicking, double-clicking, dragging, and wheel events.
/// </summary>
/// <remarks>
/// This class tracks mouse state changes between frames and generates appropriate events for various mouse interactions.
/// It includes configurable settings for double-click timing and drag sensitivity thresholds.
/// </remarks>
public class MouseListener : InputListener
{
    private readonly MouseButton[] _buttons =
    {
        MouseButton.LeftButton,
        MouseButton.MiddleButton,
        MouseButton.RightButton,
        MouseButton.XButton1,
        MouseButton.XButton2
    };

    private MouseStateEx _state;
    private bool _dragging;
    private GameTime _gameTime;
    private bool _hasDoubleClicked;
    private MouseEventArgs _mouseDownArgs;
    private MouseEventArgs _previousClickArgs;

    /// <summary>
    /// Gets the time threshold for detecting double-click events.
    /// </summary>
    /// <value>
    /// A <see cref="TimeSpan"/> representing the maximum time between clicks for a double-click to be detected.
    /// </value>
    public TimeSpan DoubleClickTime { get; }

    /// <summary>
    /// Gets the distance threshold for distinguishing between clicks and drags.
    /// </summary>
    /// <value>
    /// An <see cref="int"/> representing the minimum pixel distance the mouse must move before a drag operation begins.
    /// </value>
    public int DragThreshold { get; }

    /// <summary>
    /// Gets a value indicating whether the mouse position has changed between the current and previous frames.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if the mouse has moved; otherwise, <see langword="false"/>.
    /// </value>
    public bool HasMouseMoved => _state.HasPositionChanged;

    /// <summary>
    /// Occurs when a mouse button is pressed down.
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseDown;

    /// <summary>
    /// Occurs when a mouse button is released.
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseUp;

    /// <summary>
    /// Occurs when a mouse button is clicked (pressed and released without significant movement).
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseClicked;

    /// <summary>
    /// Occurs when a mouse button is double-clicked.
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseDoubleClicked;

    /// <summary>
    /// Occurs when the mouse position changes.
    /// </summary>
    public event EventHandler<MouseMovedEventArgs> MouseMoved;

    /// <summary>
    /// Occurs when the mouse wheel is scrolled.
    /// </summary>
    public event EventHandler<MouseScrollWheelEventArgs> MouseWheelMoved;

    /// <summary>
    /// Occurs when a drag operation begins.
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseDragStart;

    /// <summary>
    /// Occurs during an active drag operation when the mouse moves.
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseDrag;

    /// <summary>
    /// Occurs when a drag operation ends.
    /// </summary>
    public event EventHandler<MouseEventArgs> MouseDragEnd;

    /// <summary>
    /// Initializes a new instance of the <see cref="MouseListener"/> class with default settings.
    /// </summary>
    /// <remarks>
    /// This constructor uses a double-click time of 400 milliseconds and a drag threshold of 5 pixels.
    /// </remarks>
    public MouseListener()
        : this(TimeSpan.FromMilliseconds(400), 5)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MouseListener"/> class with custom settings.
    /// </summary>
    /// <param name="doubleClickTime">The maximum time between clicks for a double-click to be detected.</param>
    /// <param name="dragThreshold">The minimum pixel distance required to start a drag operation.</param>
    public MouseListener(TimeSpan doubleClickTime, int dragThreshold)
    {
        DoubleClickTime = doubleClickTime;
        DragThreshold = dragThreshold;
    }

    /// <summary>
    /// Updates the mouse listener and processes input events.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    /// <remarks>
    /// This method should be called once per frame, typically in the game's Update method.
    /// It captures the current mouse state, compares it with the previous state to detect changes,
    /// and raises appropriate events for button presses, releases, movement, and wheel scrolling.
    /// </remarks>
    public override void Update(GameTime gameTime)
    {
        _gameTime = gameTime;
        _state = MouseEx.GetState();

        CheckButtonsPressed(gameTime);
        CheckButtonsReleased(gameTime);

        if (HasMouseMoved)
        {
            MouseMovedEventArgs args = new MouseMovedEventArgs(_state);
            MouseMoved?.Invoke(this, args);

            CheckButtonsDragged(gameTime);
        }

        if (_state.ScrollWheelDelta != 0 || _state.HorizontalScrollWheelDelta != 0)
        {
            MouseScrollWheelEventArgs args = new MouseScrollWheelEventArgs(_state);
            MouseWheelMoved?.Invoke(this, args);
        }
    }

    private void CheckButtonsPressed(GameTime gameTime)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            MouseButton button = _buttons[i];
            ButtonStateEx buttonState = _state[button];

            if ((buttonState & ButtonStateEx.Pressed) == ButtonStateEx.Pressed)
            {
                MouseEventArgs args = new MouseEventArgs(_state, button, gameTime.TotalGameTime);
                MouseDown?.Invoke(this, args);
                _mouseDownArgs = args;

                // Check for double-click
                if (_previousClickArgs != null)
                {
                    var timeSinceLastClick = args.Time - _previousClickArgs.Time;
                    if (timeSinceLastClick <= DoubleClickTime)
                    {
                        MouseDoubleClicked?.Invoke(this, args);
                        _hasDoubleClicked = true;
                    }
                    _previousClickArgs = null;
                }
            }
        }
    }

    private void CheckButtonsReleased(GameTime gameTime)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            MouseButton button = _buttons[i];
            ButtonStateEx buttonState = _state[button];

            if ((buttonState & ButtonStateEx.Released) == ButtonStateEx.Released)
            {
                MouseEventArgs args = new MouseEventArgs(_state, button, gameTime.TotalGameTime);

                if(_mouseDownArgs?.Button == args.Button)
                {
                    int clickDistance = MathEx.ManhattanDistance(_state.Position, _mouseDownArgs.Position);

                    // Determine if this was a click or end of drag
                    if (clickDistance < DragThreshold)
                    {
                        if (!_hasDoubleClicked)
                        {
                            MouseClicked?.Invoke(this, args);
                        }
                    }
                    else if (_dragging)
                    {
                        MouseDragEnd?.Invoke(this, args);
                        _dragging = false;
                    }
                }

                MouseUp?.Invoke(this, args);
                _hasDoubleClicked = false;
                _previousClickArgs = args;
            }
        }
    }

    private void CheckButtonsDragged(GameTime gameTime)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            MouseButton button = _buttons[i];
            ButtonStateEx buttonState = _state[button];

            if ((buttonState & ButtonStateEx.Down) == ButtonStateEx.Down)
            {
                if (_mouseDownArgs != null && _mouseDownArgs.Button == button)
                {
                    MouseEventArgs args = new MouseEventArgs(_state, button, gameTime.TotalGameTime);

                    if (_dragging)
                    {
                        MouseDrag?.Invoke(this, args);
                    }
                    else
                    {
                        // TODO: Make mousedraggedeventargs
                        int dragDistance = MathEx.ManhattanDistance(_state.Position, _mouseDownArgs.Position);
                        if (dragDistance >= DragThreshold)
                        {
                            _dragging = true;
                            MouseDragStart?.Invoke(this, args);
                        }
                    }
                }
            }
        }
    }

    private void RaiseMouseWheelMovedEvent()
    {
        var args = new MouseEventArgs(_gameTime.TotalGameTime, _state);
        MouseWheelMoved?.Invoke(this, args);
    }
}
