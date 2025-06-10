using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

/// <summary>
/// Provides extended mouse functionality with automatic frame-to-frame state tracking and enhanced input detection
/// capabilities.
/// </summary>
/// <remarks>
/// This static class extends the functionality of the standard <see cref="Mouse"/> class by automatically maintaining
/// previous frame state information, enabling detection of mouse button press/release transitions and movement deltas.
/// It provides a drop-in replacement for <see cref="Mouse.GetState()"/> that returns <see cref="MouseStateEx"/> instances
/// with enhanced input detection capabilities. The class also includes additional utility methods for setting mouse position
/// with matrix transformations.
/// </remarks>
public static class MouseEx
{
    public static MouseState _previousState = new MouseState();

    /// <summary>
    /// Gets or sets the window handle for mouse input capture.
    /// </summary>
    /// <value>
    /// An <see cref="IntPtr"/> representing the window handle that will capture mouse input.
    /// </value>
    public static IntPtr WindowHandle
    {
        get => Mouse.WindowHandle;
        set => Mouse.WindowHandle = value;
    }

    /// <summary>
    /// Gets the current extended mouse state with automatic previous frame tracking.
    /// </summary>
    /// <returns>
    /// A <see cref="MouseStateEx"/> structure containing current and previous frame mouse state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's mouse state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static MouseStateEx GetState()
    {
        MouseState current = Mouse.GetState();
        MouseStateEx state = new MouseStateEx(current, _previousState);
        _previousState = current;
        return state;
    }

    /// <summary>
    /// Gets the current extended mouse state for a specific window with automatic previous frame tracking.
    /// </summary>
    /// <param name="window">The game window to get mouse state for.</param>
    /// <returns>
    /// A <see cref="MouseStateEx"/> structure containing current and previous frame mouse state information.
    /// </returns>
    /// <remarks>
    /// This method automatically maintains the previous frame's mouse state internally, eliminating the need
    /// to manually track previous states for input transition detection. Each call updates the internal
    /// previous state with the current state before returning the new extended state.
    /// </remarks>
    public static MouseStateEx GetState(GameWindow window)
    {
        MouseState current = Mouse.GetState(window);
        MouseStateEx state = new MouseStateEx(current, _previousState);
        _previousState = current;
        return state;
    }

    /// <summary>
    /// Sets the mouse cursor position to the specified coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate for the new cursor position.</param>
    /// <param name="y">The y-coordinate for the new cursor position.</param>
    public static void SetPosition(int x, int y) => Mouse.SetPosition(x, y);

    /// <summary>
    /// Sets the mouse cursor position to the specified coordinates after applying a matrix transformation.
    /// </summary>
    /// <param name="x">The x-coordinate before transformation.</param>
    /// <param name="y">The y-coordinate before transformation.</param>
    /// <param name="matrix">The transformation matrix to apply to the coordinates.</param>
    /// <remarks>
    /// This method applies the specified transformation matrix to the input coordinates before setting
    /// the mouse position. This is useful for scenarios involving coordinate system transformations,
    /// such as camera matrices or UI scaling. The transformed coordinates are rounded to the nearest
    /// integer values before setting the cursor position.
    /// </remarks>
    public static void SetPosition(int x, int y, Matrix matrix)
    {
        Vector2 pos = new Vector2(x, y);
        Vector2 transformed = Vector2.Transform(pos, matrix);
        SetPosition((int)Math.Round(transformed.X), (int)Math.Round(transformed.Y));
    }

    /// <summary>
    /// Sets the mouse cursor position to the specified point.
    /// </summary>
    /// <param name="pos">The point representing the new cursor position.</param>
    public static void SetPosition(Point pos) => Mouse.SetPosition(pos.X, pos.Y);

    /// <summary>
    /// Sets the mouse cursor position to the specified point after applying a matrix transformation.
    /// </summary>
    /// <param name="pos">The point representing the position before transformation.</param>
    /// <param name="matrix">The transformation matrix to apply to the coordinates.</param>
    /// <remarks>
    /// This method applies the specified transformation matrix to the input point before setting
    /// the mouse position. This is useful for scenarios involving coordinate system transformations,
    /// such as camera matrices or UI scaling. The transformed coordinates are rounded to the nearest
    /// integer values before setting the cursor position.
    /// </remarks>
    public static void SetPosition(Point pos, Matrix matrix) => SetPosition(pos.X, pos.Y, matrix);
}
