using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CoolGame;

public class GameLoop : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public List<RenderData> RenderBuffer = new List<RenderData>();
    public Color backgroundColor = new Color(0x9beca6);

    public static event Action<GameTime> UpdateRequest;
    public static event Action<List<RenderData>> RenderBufferRequest;
    public static event Action<GameTime> PostRenderUpdateRequest;

    #region init
    public GameLoop()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        CreatePlayer();
        CreateStones(20);
    }
    #endregion


    #region update
    protected override void Update(GameTime gameTime)
    {
        if (UserIsRequestingExit)
            Exit();

       UpdateRequest?.Invoke(gameTime);

        base.Update(gameTime);
    }
    bool UserIsRequestingExit => GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
    #endregion


    #region draw
    protected override void Draw(GameTime gameTime)
    {
        RenderBufferRequest?.Invoke(RenderBuffer);

        SortRenderBuffer();

        GraphicsDevice.Clear(backgroundColor);
        DrawRenderBuffer();

        base.Draw(gameTime);

        PostRenderUpdateRequest?.Invoke(gameTime);
    }

    private void SortRenderBuffer()
    {
        RenderBuffer.Sort((data1, data2) => data1.rect.Y.CompareTo(data2.rect.Y));
    }

    private void DrawRenderBuffer()
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        foreach(RenderData renderData in RenderBuffer)
        {
            _spriteBatch.Draw(
                renderData.texture2D, 
                renderData.rect, 
                null, 
                renderData.tint, 
                renderData.rotation, 
                Vector2.Zero, 
                renderData.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
                0f
            );
        }
        _spriteBatch.End();
        RenderBuffer.Clear();
    }
    #endregion


    #region lilOldFunctionsIMade
    void CreatePlayer()
    {
        //Sprite shadowSprite = new Sprite(Content.Load<Texture2D>("shadow"));
        Sprite playerSprite = new Sprite(Content.Load<Texture2D>("yellowman"));
        // Wiggler wiggler = new Wiggler(2, 10, true);
        KeyboardInputHandler keyboardInput = new KeyboardInputHandler();
        PlayerController playerController = new PlayerController(keyboardInput, playerSprite);
        List<Component> components = new List<Component>{keyboardInput, playerSprite, playerController};
        GameObject player = new GameObject(centreScreenPos(), components);
    }

    void CreateStones(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Sprite sprite = new Sprite(Content.Load<Texture2D>("rock"));
            GameObject stone = new GameObject(randomScreenPos(), sprite);
        }
    }

    Vector2 centreScreenPos()
    {
        return new Vector2(0.5f * _graphics.PreferredBackBufferWidth, 0.5f * _graphics.PreferredBackBufferHeight);
    }

    Vector2 randomScreenPos()
    {
        return new Vector2((float)Rand.Value()*_graphics.PreferredBackBufferWidth, (float)Rand.Value()*_graphics.PreferredBackBufferHeight);
    }
    #endregion
}
