using System;
using Microsoft.Xna.Framework;

namespace MonoGame.Community.Toolkit.Input;

public class MouseEventArgs : EventArgs
{
    public MouseStateEx MouseState { get; }
    public MouseButton Button { get; }
    public Point Position { get; }
    public TimeSpan Time { get; }

    public MouseEventArgs(MouseStateEx state, MouseButton button, TimeSpan time)
    {
        MouseState = state;
        Button = button;
        Position = state.Position;
        Time = time;
    }
}
