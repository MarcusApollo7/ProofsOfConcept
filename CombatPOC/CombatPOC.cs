using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Managers;
using CombatPOC.Paths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;

namespace CombatPOC;

public class CombatPOC : Core
{
    private Combatant _hero = new Hero("Test1");
    private Combatant _hero2 = new Hero("Test2");
    private Tilemap _tileMap;
    private Rectangle _roomBounds;
    private Vector2 _scale = new(4.0f, 4.0f);
    private Vector2 _heroPosition;
    private Texture2D _solidRectangle; 
    private CombatManager _combatManager;
    public CombatPOC() : base("CombatPOC", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();
        AStar aStar = new(_tileMap);
        aStar.Initialize();
        List<Location> path = aStar.FindPath();
        foreach(Location location in path)
        {
            Debug.WriteLine($"Tile: {location.X}, {location.Y}");
        }
        // TODO: Add your initialization logic here
        List<Combatant> combatants = [_hero, _hero2];
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
        _hero2.Initialize(new(0, 0));
    }
    protected override void LoadContent()
    {
        base.LoadContent();
        // Create the texture atlas from the XML configuration file
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _hero.LoadSpriteFromAtlas(atlas, "slime-animation", _scale);
        _hero2.LoadSpriteFromAtlas(atlas, "bat-animation", _scale);
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
        _solidRectangle = new Texture2D(GraphicsDevice, 1, 1);
        _solidRectangle.SetData([Color.White]);
        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin();

        _combatManager.Draw(SpriteBatch, _solidRectangle);

        // Always end the sprite batch when finished.
        SpriteBatch.End();
        base.Draw(gameTime);
    }

    
}
