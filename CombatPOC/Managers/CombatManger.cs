using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
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
    private UIManager _UIManager;
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
    public void JumpCombatant(Combatant combatant, TileLocation newposition)
    {
        combatant.JumpToNewPosition(newposition);
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
    public void Initialize()
    {
        
        _turnManager = new();
        _UIManager = new();
        _selectedCombatant = null;

    }
    public void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _tileMap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
        _tileMap.Scale = CombatPOC._scale;
        Combatant _hero = new Hero(10, 10, 10, "slime-animation", new(5, 5));
        Combatant _hero2 = new Grunt(10, 10, 10, "bat-animation", new(5, 3));
        _hero.Initialize(new PlayerActor(_hero.ActorID));
        _hero2.Initialize(new GruntActor(_hero2.ActorID));
        List<Combatant> combatants = [_hero, _hero2];
        _combatants = new(combatants);
        foreach(Combatant combatant in _combatants)
        {
            combatant.LoadSpriteFromAtlas(atlas, CombatPOC._scale);
        }
    }
    public void Update(GameTime gameTime)
    {
        SelectCombatant();
        _tileMap.Update(Input.Mouse);
        foreach(Combatant combatant in _combatants)
        {
            combatant.Update(gameTime, Input.Mouse);
        }
        
        _selectedCombatant?.CheckForPlayerInput(Input);
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D _whiteRectangle)
    {
        _tileMap.Draw(spriteBatch);
        foreach(Combatant combatant in _combatants)
        {
            combatant.Draw(spriteBatch, _whiteRectangle);
        }
    }

}