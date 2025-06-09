
using Microsoft.Xna.Framework;

namespace MonoGame.Community.Toolkit;

public static class MathEx
{
    public static readonly float MachineEpsilon = 1.1920929E-07f;
    public static readonly Vector2 MachineEpsilonVector2 = new Vector2(MachineEpsilon, MachineEpsilon);
}
