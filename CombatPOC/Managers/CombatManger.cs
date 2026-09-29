using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Item;
using CombatPOC.Logic;
using CombatPOC.Logic.Paths;
using CombatPOC.UI;
using CombatPOC.UI.Basics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Managers;

public class CombatManager
{
    internal TurnManager _TurnManager = new();
    internal static UIManager _CombatUI = new();
    internal static InputHandler _inputHandler = new();
    private BattleState _currentState = new();
    internal GameMap map;
    public CombatManager()
    {
        
    }
    public void Initialize(GraphicsDevice graphicsDevice)
    {
        _CombatUI.Initialize(graphicsDevice);
        map = new ("tilemap-walk-definition.xml");
    }
    public void LoadContent()
    {
        string atlasString = "images/atlas-definition.xml";
        map.LoadContent();
        AStar pathfinder = new(map);
        BaseCombatant hero1 = new PlayerCombatant("slime-animation", atlasString, 25, 10, 10, 10, 10, new(5, 5), pathfinder);
        Weapon sword = new()
        {
            MoneyValue = 55,
            Weight = 5,
            Attacks = [new BasicAttack(), new SwordHeavyAttack()]
        };
        BaseCombatant enemy1 = new BasicEnemy("bat-animation", atlasString, 15, 7.5f, 7.5f, 7.5f, 7.5f, new(4, 4), pathfinder);
        TextBox testBox = new(new(0, 0), "Hello Gamers!", _CombatUI.Font);
        _currentState = new([hero1, enemy1, testBox])
        {
            map = map
        };

    }
    public void Update(GameTime gameTime)
    {
        _currentState = _TurnManager.UpdateState(_currentState);
        _currentState = _CombatUI.Update(gameTime, _currentState);
        _currentState = _inputHandler.Update(_currentState);
        map.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        map.Draw(spriteBatch);
        _CombatUI.Draw(spriteBatch);
    }
}