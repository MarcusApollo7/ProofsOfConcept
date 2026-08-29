using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;

namespace CombatPOC;

public class Game1 : Core
{
    // Logo START TEST
    private Texture2D _logo;
    // END TEST

    public Game1() : base("CombatPOC", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();

    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        _logo = Content.Load<Texture2D>("images/logo");
    }

    protected override void Update(GameTime gameTime)
    {
        CheckKeyBoardInput();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    private void CheckKeyBoardInput()
    {
        if (Input.Keyboard.IsKeyDown(Keys.Space))
        {
            Debug.WriteLine("Space has been pressed");
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin();

        // Draw the logo texture
        SpriteBatch.Draw(
            _logo,                      // texture
            new Vector2(                // position
                Window.ClientBounds.Width,
                Window.ClientBounds.Height) * 0.5f,
            null,                       // sourceRectangle
            Color.White,                // color
            0,                          // rotation
            new Vector2(                // origin
                _logo.Width,
                _logo.Height) * 0.5f,
            1.0f,                       // scale
            SpriteEffects.None,         // effects
            0.0f                        // layerDepth
        );

        // Always end the sprite batch when finished.
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
