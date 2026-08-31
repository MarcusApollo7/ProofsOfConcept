using System;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;

namespace CombatPOC;

public class Game1 : Core
{
    private TurnManager _turnManager;
    private AnimatedSprite _hero;
    private AnimatedSprite _enemy;
    private Tilemap _tileMap;
    private Rectangle _roomBounds;
    private Vector2 _heroPosition;
    private Vector2 _enemyPosition;

    public Game1() : base("CombatPOC", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();
        // TODO: Add your initialization logic here
        // ADD MOCK PARTIES TO TURNMANAGER
        Rectangle screenBounds = GraphicsDevice.PresentationParameters.Bounds;

       _roomBounds = new Rectangle(
            (int)_tileMap.TileWidth,
            (int)_tileMap.TileHeight,
            screenBounds.Width - (int)_tileMap.TileWidth * 2,
            screenBounds.Height - (int)_tileMap.TileHeight * 2
        );

        // Initial slime position will be the center tile of the tile map.
        int centerRow = _tileMap.Rows / 2;
        int centerColumn = _tileMap.Columns / 2;
        _heroPosition = new Vector2(centerColumn * _tileMap.TileWidth, centerRow * _tileMap.TileHeight);

        // Initial bat position will be in the top left corner of the room
        _enemyPosition = new Vector2(_roomBounds.Left, _roomBounds.Top);
    }

    protected override void LoadContent()
    {
        // Create the texture atlas from the XML configuration file
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        // Create the slime animated sprite from the atlas.
        _hero = atlas.CreateAnimatedSprite("slime-animation");
        _hero.Scale = new Vector2(4.0f, 4.0f);

        // Create the bat animated sprite from the atlas.
        _enemy = atlas.CreateAnimatedSprite("bat-animation");
        _enemy.Scale = new Vector2(4.0f, 4.0f);
        // Create the tilemap from the XML configuration file.
        _tileMap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
        _tileMap.Scale = new Vector2(4.0f, 4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        base.Update(gameTime);
        // Update the slime animated sprite.
        _hero.Update(gameTime);

        // Update the bat animated sprite.
        _enemy.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin();

        _tileMap.Draw(SpriteBatch);
        // Draw the slime sprite.
        _hero.Draw(SpriteBatch, _heroPosition);

        // Draw the bat sprite
        _enemy.Draw(SpriteBatch, _enemyPosition);

        // Always end the sprite batch when finished.
        SpriteBatch.End();
        

        base.Draw(gameTime);
    }
}
