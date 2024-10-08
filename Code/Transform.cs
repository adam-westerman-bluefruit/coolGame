using Microsoft.Xna.Framework;

public class Transform
{
    public Vector2 Position;
    public Vector2 Scale;
    public float Rotation;
    public Rectangle Rect
    {
        get
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Scale.X, (int)Scale.Y);
        }
    }

    public Transform(Vector2 position, Vector2 scale, float rotation)
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }
}