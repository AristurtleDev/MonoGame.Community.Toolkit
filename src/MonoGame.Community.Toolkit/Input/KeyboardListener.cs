using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Provides event-driven keyboard input handling with optional key repeat functionality.
/// </summary>
public class KeyboardListener : InputListener
{
    private readonly Keys[] _keys = (Keys[])Enum.GetValues(typeof(Keys));

    private KeyboardStateEx _state;
    private Keys _previousKey;
    private TimeSpan _lastPressTime;
    private bool _isInitial;

    /// <summary>
    /// Gets a value indicating whether key repeat events are enabled.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if key repeat events will be generated for held keys; otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// When enabled, keys that are held down will generate <see cref="KeyRepeated"/> events after the initial delay,
    /// followed by additional repeat events at the specified repeat interval.
    /// </remarks>
    public bool RepeatPress { get; }

    /// <summary>
    /// Gets the time delay before the first repeat event is generated for a held key.
    /// </summary>
    /// <value>
    /// A <see cref="TimeSpan"/> representing the initial delay before key repeat begins.
    /// </value>
    /// <remarks>
    /// This delay applies only to the first repeat event after a key is initially pressed.
    /// Subsequent repeat events use the <see cref="RepeatDelay"/> interval.
    /// </remarks>
    public TimeSpan InitialDelay { get; }

    /// <summary>
    /// Gets the time interval between subsequent repeat events for a held key.
    /// </summary>
    /// <value>
    /// A <see cref="TimeSpan"/> representing the interval between repeat events.
    /// </value>
    /// <remarks>
    /// This delay is used for all repeat events after the initial repeat event has been generated.
    /// A shorter delay results in faster key repetition.
    /// </remarks>
    public TimeSpan RepeatDelay { get; }

    /// <summary>
    /// Occurs when a key that can be converted to a character is pressed.
    /// </summary>
    /// <remarks>
    /// This event is raised when a key is pressed that has a corresponding character representation,
    /// such as letters, numbers, punctuation, and whitespace characters. It is not raised for non-printable
    /// keys like function keys, arrow keys, or modifier keys alone.
    /// </remarks>
    public EventHandler<KeyboardEventArgs> KeyTyped;

    /// <summary>
    /// Occurs when a key is pressed (transitioned from up to down).
    /// </summary>
    /// <remarks>
    /// This event is raised once when a key transitions from the up state to the down state.
    /// It will not be raised again until the key is released and pressed again.
    /// </remarks>
    public EventHandler<KeyboardEventArgs> KeyPressed;

    /// <summary>
    /// Occurs when a key is released (transitioned from down to up).
    /// </summary>
    /// <remarks>
    /// This event is raised once when a key transitions from the down state to the up state.
    /// When a key is released, any pending repeat events for that key are cancelled.
    /// </remarks>
    public EventHandler<KeyboardEventArgs> KeyReleased;

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardListener"/> class with default repeat settings.
    /// </summary>
    /// <remarks>
    /// This constructor enables key repeat with an initial delay of 800 milliseconds and a repeat delay of 50 milliseconds.
    /// These values match typical keyboard repeat behavior in most operating systems.
    /// </remarks>
    public KeyboardListener()
        : this(true, TimeSpan.FromMilliseconds(800), TimeSpan.FromMilliseconds(50)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeyboardListener"/> class with custom repeat settings.
    /// </summary>
    /// <param name="repeatPress">
    /// <see langword="true"/> to enable key repeat events; <see langword="false"/> to disable them.
    /// </param>
    /// <param name="initialDelay">The time delay before the first repeat event is generated.</param>
    /// <param name="repeatDelay">The time interval between subsequent repeat events.</param>
    /// <remarks>
    /// Use this constructor to customize the key repeat behavior or to disable key repeat entirely.
    /// Setting <paramref name="repeatPress"/> to <see langword="false"/> will prevent any <see cref="KeyRepeated"/>
    /// events from being generated, regardless of the delay values.
    /// </remarks>
    public KeyboardListener(bool repeatPress, TimeSpan initialDelay, TimeSpan repeatDelay)
    {
        RepeatPress = repeatPress;
        InitialDelay = initialDelay;
        RepeatDelay = repeatDelay;
    }

    /// <summary>
    /// Updates the keyboard listener and processes input events.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    /// <remarks>
    /// This method should be called once per frame, typically in the game's Update method.
    /// It captures the current keyboard state, compares it with the previous state to detect changes,
    /// and raises appropriate events for key presses, releases, and repeats.
    /// </remarks>
    public override void Update(GameTime gameTime)
    {
        _state = KeyboardEx.GetState();
        RaisedPressedEvents(gameTime);
        RaiseReleasedEvents();

        if (RepeatPress)
        {
            RaiseRepeatEvents(gameTime);
        }
    }

    private void RaisedPressedEvents(GameTime gameTime)
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            Keys key = _keys[i];
            if (_state.WasKeyPressed(key))
            {
                KeyboardEventArgs args = new KeyboardEventArgs(key, _state);
                KeyPressed?.Invoke(this, args);

                if (key.ToChar().HasValue)
                {
                    KeyTyped?.Invoke(this, args);
                }

                _previousKey = key;
                _lastPressTime = gameTime.TotalGameTime;
                _isInitial = true;
            }
        }
    }

    private void RaiseReleasedEvents()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            Keys key = _keys[i];
            if (_state.WasKeyReleased(key))
            {
                KeyboardEventArgs args = new KeyboardEventArgs(key, _state);
                KeyReleased?.Invoke(this, args);
            }
        }
    }

    private void RaiseRepeatEvents(GameTime gameTime)
    {
        if (!_state.IsKeyDown(_previousKey)) { return; }

        TimeSpan elapsed = gameTime.TotalGameTime - _lastPressTime;

        bool repeated = _isInitial && elapsed > InitialDelay ||
                        !_isInitial && elapsed > RepeatDelay;

        if (repeated)
        {
            KeyboardEventArgs args = new KeyboardEventArgs(_previousKey, _state);
            KeyPressed?.Invoke(this, args);

            if (_previousKey.ToChar().HasValue)
            {
                KeyTyped?.Invoke(this, args);
            }

            _lastPressTime = gameTime.TotalGameTime;
            _isInitial = false;
        }
    }
}
