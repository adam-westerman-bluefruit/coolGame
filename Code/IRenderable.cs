using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public struct RenderData
{
    public Texture2D texture2D;
    public Rectangle rect;
    public Color tint;
    public bool flipped;
    public float rotation;
    public RenderData(Texture2D texture2D, Rectangle rectangle, Color tint, bool flipped, float rotation)
    {
        this.texture2D = texture2D;
        this.rect = rectangle;
        this.tint = tint;
        this.flipped = flipped;
        this.rotation = rotation;
    }
}

public interface IRenderable
{
    public RenderData GetRenderData();
}