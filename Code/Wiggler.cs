using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TweenSharp;

public class Wiggler : Component, IUpdatable
{
    Vector2 _positionThisFrame;
    double angle = 0;
    public double offset = 0;
    public double speed = 1;
    public double amp = 10;
    public bool abs = false;

    public Wiggler(){}

    public Wiggler(double speed, double amp, bool isAbsolute)
    {
        this.speed = speed;
        this.amp = amp;
        abs = isAbsolute;
    }

    public override void Awake()
    {
        offset = Rand.Value() * (MathF.PI * 2);
    }

    public void Update(GameTime gameTime)
    {
        _positionThisFrame = gameObject.transform.Position;

        float motion = 0;
        if(enabled)
        {
            angle += ((float)gameTime.ElapsedGameTime.Milliseconds / 100) * speed;
            angle %= Math.PI * 2;
            motion = (float)Math.Sin(offset + angle) * (float)amp;
            if(abs)
                motion = Math.Abs(motion);
        }
        gameObject.transform.Position.Y = _positionThisFrame.Y - motion;
    }

    public void LateUpdate(GameTime gameTime)
    {
        gameObject.transform.Position = _positionThisFrame;
    }
}