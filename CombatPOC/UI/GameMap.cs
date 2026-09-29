using System.Collections.Generic;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Graphics;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public class GameMap
{
    private string TileMapString {get; }
    public Tilemap TileMap {get; set; }
    private Tile[] _tiles;
    public int MapWidth {get => TileMap.Columns; }
    public int MapHeight {get => TileMap.Rows; }
    public GameMap(string tileMapString)
    {
        TileMapString = tileMapString;
    }
    public void LoadContent()
    {
        TileMap = Tilemap.FromFile(Content, "images/" + TileMapString);
        TileMap.Scale = Helper.Scale;
        List<Tile> tiles = [];
        for(int i = 0; i < MapHeight; i++)
        {
            for(int j = 0; j < MapWidth; j++)
            {
                TileLocation tileLocation = new(j, i);
                tiles.Add(new(tileLocation, TileMap.GetTile(j, i)));
            }
        }
        _tiles = [.. tiles];
    }
    public void Update(GameTime gameTime)
    {
        foreach(Tile tile in _tiles)
        {
            tile.OnHover();
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(Tile tile in _tiles)
        {
            tile.Draw(spriteBatch);
        }
    }
}