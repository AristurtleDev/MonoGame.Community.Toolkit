using System;

namespace MonoGame.Community.Toolkit.Input;

public sealed class MouseScrollWheelEventArgs : EventArgs
{
    public MouseStateEx State { get; }
    public int ScrollWheelValue => State.ScrollWheelValue;
    public int ScrollWheelDelta => State.ScrollWheelDelta;

    public MouseScrollWheelEventArgs(MouseStateEx state)
    {
        State = state;
    }
}
