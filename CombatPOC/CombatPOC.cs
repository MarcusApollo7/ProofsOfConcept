using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Interfaces;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;

namespace CombatPOC;

public class CombatPOC : Core
{
    private Combatant _hero = new(10, 10, 10);
    private Tilemap _tileMap;
    private Rectangle _roomBounds;
    private Vector2 _scale = new(4.0f, 4.0f);
    private Vector2 _heroPosition;
    private CombatManager _combatManager;
    public CombatPOC() : base("CombatPOC", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();
        // TODO: Add your initialization logic here
        List<Combatant> combatants = [_hero];
        _combatManager = new(combatants, _tileMap);
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
        _hero.Initialize(_heroPosition);
        // Initial bat position will be in the top left corner of the room


    }
    protected override void LoadContent()
    {
        // Create the texture atlas from the XML configuration file
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _hero.LoadSpriteFromAtlas(atlas, "slime-animation", _scale);
        _hero.JumpToNewPosition(_heroPosition);
        _tileMap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
        _tileMap.Scale = _scale;
    }
    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here

        base.Update(gameTime);
        _combatManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin();

        _tileMap.Draw(SpriteBatch);
        // Draw the slime sprite.
        _hero.Draw(SpriteBatch);

        // Always end the sprite batch when finished.
        SpriteBatch.End();
        base.Draw(gameTime);
    }

    
}
