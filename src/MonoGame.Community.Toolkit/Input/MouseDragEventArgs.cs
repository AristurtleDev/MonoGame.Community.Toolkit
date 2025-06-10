using System;

namespace MonoGame.Community.Toolkit.Input;

public class MouseDragEventArgs : EventArgs
{
    public MouseStateEx MouseState { get; set; }

    public MouseDragEventArgs(MouseStateEx state)
    {

    }
}
