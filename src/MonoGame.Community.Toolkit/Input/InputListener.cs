using Microsoft.Xna.Framework;

namespace MonoGame.Community.Toolkit.Input;

public abstract class InputListener
{
    protected InputListener() { }

    public abstract void Update(GameTime gameTime);
}
