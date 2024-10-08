using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CoolGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<GameObject> _allGameObjects = new List<GameObject>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        GameObject.Created += OnGameObjectCreated;
    }

    private void OnGameObjectCreated(GameObject gameObject)
    {
        _allGameObjects.Add(gameObject);
    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        CreatePlayer();
    }

    void CreatePlayer()
    {
        Sprite playerSprite = new Sprite(Content.Load<Texture2D>("cappy"));
        Wiggler wiggler = new Wiggler(2, 10, true);
        KeyboardInputHandler keyboardInput = new KeyboardInputHandler();
        PlayerController playerController = new PlayerController(keyboardInput);
        List<Component> components = new List<Component>{keyboardInput, playerSprite, playerController, wiggler};
        GameObject player = new GameObject(centreScreenPos(), components);
    }

    Vector2 centreScreenPos()
    {
        return new Vector2(0.5f * _graphics.PreferredBackBufferWidth, 0.5f * _graphics.PreferredBackBufferHeight);
    }

    Vector2 randomScreenPos()
    {
        return new Vector2((float)Rand.Value()*_graphics.PreferredBackBufferWidth, (float)Rand.Value()*_graphics.PreferredBackBufferHeight);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
       UpdateAllGameObejcts(gameTime);

        base.Update(gameTime);
    }

    private void UpdateAllGameObejcts(GameTime gameTime)
    {
        foreach(GameObject gameObject in _allGameObjects)
        {
            gameObject.Update(gameTime);
        }
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        RenderAllGameObjects();
        _spriteBatch.End();

        base.Draw(gameTime);
        LateUpdate(gameTime);
    }

    private void LateUpdate(GameTime gameTime)
    {
        foreach(GameObject gameObject in _allGameObjects)
        {
            gameObject.LateUpdate(gameTime);
        }
    }

    private void RenderAllGameObjects()
    {
        foreach(GameObject gameObject in _allGameObjects)
        {
            foreach(RenderData renderData in gameObject.GetRenderData())
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
        }
    }

}
