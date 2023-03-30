using System.Numerics;

namespace ChessGame.ZFW;

public static class Vector2Extensions
{
    const float Deg2Rad = MathF.PI / 180f;

    public static Vector2 Rotate(this Vector2 self, float angle)
    {
        Matrix3x2 matrix = Matrix3x2.CreateRotation(angle * Deg2Rad);
        return Vector2.Transform(self, matrix);
    }
}
