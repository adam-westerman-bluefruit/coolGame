using Microsoft.Xna.Framework;

public interface IUpdatable
{
    public void Update(GameTime gameTime);
    public void LateUpdate(GameTime gameTime);
}