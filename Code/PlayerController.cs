using Microsoft.Xna.Framework;

public class PlayerController : Component, IUpdatable
{
    public float speed = 200;
    private KeyboardInputHandler keyboardInput;
    //private Wiggler wiggler;
    private Sprite sprite;
    private bool isFlipped = false;

    #region init
    public PlayerController(KeyboardInputHandler keyboardInput, Sprite playerSprite)
    {
        this.keyboardInput = keyboardInput;
        sprite = playerSprite;
    }
    #endregion


    #region update
    public void Update(GameTime gameTime)
    {
        Vector2 inputAxis = keyboardInput.arrowAxis;
        gameObject.transform.Position += inputAxis * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds) ;
        
        // if (wiggler == null)
        //     wiggler = gameObject.GetComponent<Wiggler>();
        // wiggler.enabled = inputAxis.Length() > 0;
        
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
    #endregion
}