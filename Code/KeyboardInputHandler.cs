using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputHandler : Component, IUpdatable
{
    public Vector2 arrowAxis = new Vector2();

    public void LateUpdate(GameTime gameTime)
    {
        //
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        arrowAxis = new Vector2(
            (keyboardState.IsKeyDown(Keys.Right) ? 1 : 0) - (keyboardState.IsKeyDown(Keys.Left) ? 1 : 0),
            (keyboardState.IsKeyDown(Keys.Down) ? 1 : 0) - (keyboardState.IsKeyDown(Keys.Up) ? 1 : 0)
        );

        if (arrowAxis != Vector2.Zero)
            arrowAxis.Normalize();
    }
} 