using System;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

public class KeyboardEventArgs : EventArgs
{
    public Keys Key { get; }
    public KeyboardModifiers Modifiers { get; }

    public KeyboardEventArgs(Keys key, KeyboardStateEx state)
    {
        Key = key;
        Modifiers = KeyboardModifiers.None;

        if (state.Control)
        {
            Modifiers |= KeyboardModifiers.Control;
        }

        if (state.Shift)
        {
            Modifiers |= KeyboardModifiers.Shift;
        }

        if (state.Alt)
        {
            Modifiers |= KeyboardModifiers.Alt;
        }
    }
}
