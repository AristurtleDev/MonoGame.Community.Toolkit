
using System;
using Microsoft.Xna.Framework;

namespace MonoGame.Community.Toolkit;

public static class MathEx
{
    public static readonly float MachineEpsilon = 1.1920929E-07f;
    public static readonly Vector2 MachineEpsilonVector2 = new Vector2(MachineEpsilon, MachineEpsilon);

    public static int ManhattanDistance(int x1, int y1, int x2, int y2)
    {
        int v1 = Math.Abs(x1 - x2);
        int v2 = Math.Abs(y1 - y2);
        return v1 + v2;
    }

    public static int ManhattanDistance(Point start, Point end) => ManhattanDistance(start.X, end.X, start.Y, end.Y);

    public static float ManhattanDistance(float x1, float y1, float x2, float y2)
    {
        float v1 = MathF.Abs(x1 - x2);
        float v2 = MathF.Abs(y1 - y2);
        return v1 + v2;
    }

    public static float ManhattanDistance(Vector2 start, Vector2 end) => ManhattanDistance(start.X, start.Y, end.X, end.Y);
}
