using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Graphics;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public class MapEntity: IEntity, IRenderable
{
    public int EntityID {get; } = EntityManager.CreateNewEntityID();
    private string TileMapString {get; }
    private Tilemap _tileMap;
    private Tile[] _tiles;
    public List<IComponent> Components {get; } = new();
    public int MapWidth {get => _tileMap.Columns; }
    public int MapHeight {get => _tileMap.Rows; }
    public MapEntity(string tileMapString)
    {
        TileMapString = tileMapString;
    }
    public Tilemap LoadContent()
    {
        _tileMap = Tilemap.FromFile(Content, "images/" + TileMapString);
        _tileMap.Scale = Helper.Scale;
        List<Tile> tiles = [];
        for(int i = 0; i < _tileMap.Rows; i++)
        {
            for(int j = 0; j < _tileMap.Columns; j++)
            {
                TileLocation tileLocation = new(j, i);
                tiles.Add(new(tileLocation, _tileMap.GetTile(j, i)));
            }
        }
        _tiles = [.. tiles];
        return _tileMap;
    }
    public void Update(GameTime gameTime)
    {
        foreach(Tile tile in _tiles)
        {
            tile.Update();
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(Tile tile in _tiles)
        {
            tile.Draw(spriteBatch);
        }
    }
    public void UpdatePosition(TileLocation newPosition)
    {
        
    }
}