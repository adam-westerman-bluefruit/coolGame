using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class Sprite : Component, IRenderable 
{
    public Vector2 dimensions;
    public Texture2D Texture;
    public Color Tint;
    public bool flipped = false;

    public Sprite(Texture2D texture)
    {
        this.Texture = texture;
        this.Tint = Color.White;
        this.dimensions = new Vector2(texture.Width, texture.Height);
    }

    public RenderData GetRenderData()
    {
        return new RenderData(Texture, new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y), Tint, flipped, 0);
    }
}