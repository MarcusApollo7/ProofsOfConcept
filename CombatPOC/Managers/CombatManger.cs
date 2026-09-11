using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
using CombatPOC.Paths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using POCLibrary.Graphics;
using POCLibrary.Input;
using static POCLibrary.Core;

namespace CombatPOC.Managers;

public class CombatManager
{
    private TurnManager _turnManager;
    public static UIManager _UIManager;
    public static Party _combatants;
    private Combatant _selectedCombatant;
    public static Tilemap _tileMap;
    public CombatManager(List<Combatant> combatants)
    {
        _combatants = new(combatants);
    }
    public CombatManager()
    {
        
    }
    public static int DistanceBetweenCombatants(Combatant a, Combatant b)
    {
        return Math.Abs(a._stats._tileLocation.X - b._stats._tileLocation.X) + Math.Abs(a._stats._tileLocation.Y - b._stats._tileLocation.Y);
    }
    public static int DistanceBetweenTiles(TileLocation a, TileLocation b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
    public void JumpCombatant(int actorID, TileLocation newposition)
    {
        ActorManager.GetActor(actorID).JumpToNewPosition(newposition);
    }
    public void SelectCombatant()
    {
        _selectedCombatant = null;
        foreach(Combatant combatant in _combatants)
        {
            if (combatant._sprite.Selected == true)
            {
                _selectedCombatant = combatant;           
                break;
            }
        }
    }
    public void Initialize(GraphicsDevice graphicsDevice)
    {
        _UIManager = new();
        _UIManager.Initialize(graphicsDevice);
        _turnManager = new();
        _selectedCombatant = null;
    }
    public void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _tileMap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
        _UIManager.AddRenderable(_tileMap);
        _tileMap.Scale = CombatPOC._scale;
        
        Combatant _hero = new Hero(10, 10, 10, "slime-animation", new(5, 5));
        Combatant _hero2 = new Grunt(10, 10, 10, "bat-animation", new(1, 1));
        _hero.Initialize();
        _hero2.Initialize();
        _UIManager.AddRenderable(_hero._sprite);
        _UIManager.AddRenderable(_hero2._sprite);
        List<Combatant> combatants = [_hero, _hero2];
        _combatants = new(combatants);
        foreach(Combatant combatant in _combatants)
        {
            combatant.LoadSpriteFromAtlas(atlas, CombatPOC._scale);
        }
        AStar pathFinder = new();
        pathFinder.Initialize(_tileMap);
    }
    public void Update(GameTime gameTime)
    {
        _turnManager.ExectueTurn();
        _UIManager.Update(gameTime, Input.Mouse);
        SelectCombatant();
        _selectedCombatant?.CheckForPlayerInput(Input);
        if (Input.Keyboard.WasKeyJustPressed(Keys.Space))
        {
            Random random = new();
            JumpCombatant(1, new(random.Next(4), random.Next(4)));
            _turnManager.TurnTeam = TeamEnum.Enemy;
            AStar.ResetPathFinder();
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _UIManager.Draw(spriteBatch);
    }

}