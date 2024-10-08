using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerController : Component, IUpdatable
{
    public float speed = 100;
    private KeyboardInputHandler keyboardInput;
    private Wiggler wiggler;
    private Sprite sprite;
    private bool isFlipped = false;

    public PlayerController(KeyboardInputHandler keyboardInput)
    {
        this.keyboardInput = keyboardInput;
    }
    public void Update(GameTime gameTime)
    {
        Vector2 inputAxis = keyboardInput.arrowAxis;
        gameObject.transform.Position += inputAxis * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) ;
        
        if (wiggler == null)
            wiggler = gameObject.GetComponent<Wiggler>();
        wiggler.enabled = inputAxis.Length() > 0;

        if(sprite == null)
            sprite = gameObject.GetComponent<Sprite>();
        
        if(isFlipped && inputAxis.X > 0)
            isFlipped = false;
        
        if(!isFlipped && inputAxis.X < 0)
            isFlipped = true;
        
        sprite.flipped = isFlipped;
    }

    public void LateUpdate(GameTime gameTime)
    {
        //
    }
}