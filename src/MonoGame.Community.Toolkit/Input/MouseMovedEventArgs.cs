using System;
using Microsoft.Xna.Framework;

namespace MonoGame.Community.Toolkit.Input;

public sealed class MouseMovedEventArgs : EventArgs
{
    public MouseStateEx State { get; }
    public Point Position => State.Position;
    public Point PositionDelta => State.PositionDelta;
    public Point PreviousPosition => State.Previous.Position;

    public MouseMovedEventArgs(MouseStateEx state)
    {
        State = state;
    }
}
