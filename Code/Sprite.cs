using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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

    public void DrawToBuffer(List<RenderData> buffer)
    {
        Vector2 position = gameObject.transform.Position;
        Vector2 scale = gameObject.transform.Scale;
        buffer.Add(new RenderData(Texture, new Rectangle((int)position.X, (int)position.Y, (int)(dimensions.X * scale.X), (int)(dimensions.Y * scale.Y)), Tint, flipped, 0));
    }
}