using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

public readonly struct GamePadStateEx
{
    public readonly GamePadState CurrentState;
    public readonly GamePadState PreviousState;
    public readonly bool IsConnected => CurrentState.IsConnected;
    public readonly int PacketNumber => CurrentState.PacketNumber;
    public readonly GamePadButtonsEx Buttons;
    public readonly GamePadDPadEx DPad;

}
