using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Community.Toolkit.Input;

public class InputEx
{
    public KeyboardStateEx Keyboard { get; private set; }
    public MouseStateEx Mouse { get; private set; }
    public GamePadStateEx[] GamePads { get; private set; }

    public InputEx()
    {
        Keyboard = new KeyboardStateEx(new KeyboardState(), new KeyboardState());
        Mouse = new MouseStateEx(new MouseState(), new MouseState());

        GamePads = new GamePadStateEx[4];
        for (int i = 0; i < 4; i++)
        {
            PlayerIndex player = (PlayerIndex)i;
            GamePads[i] = new GamePadStateEx(player, new GamePadState(), new GamePadState());
        }
    }

    public void Update(GameTime gameTime)
    {
        UpdateKeyboard();
        UpdateMouse();
        UpdateGamePads();
    }

    private void UpdateKeyboard()
    {
        KeyboardState previous = Keyboard.CurrentState;
        KeyboardState current = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        Keyboard = new KeyboardStateEx(current, previous);
    }

    private void UpdateMouse()
    {
        MouseState previous = Mouse.Current;
        MouseState current = Microsoft.Xna.Framework.Input.Mouse.GetState();
        Mouse = new MouseStateEx(current, previous);
    }

    private void UpdateGamePads()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerIndex player = (PlayerIndex)i;
            GamePadState previous = GamePads[i].CurrentState;
            GamePadState current = Microsoft.Xna.Framework.Input.GamePad.GetState(player);
            GamePads[i] = new GamePadStateEx(player, current, previous);
        }
    }
}
