using CombatPOC.Classes;
using CombatPOC.Item;
using CombatPOC.Logic;
using CombatPOC.Logic.Paths;
using CombatPOC.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Managers;

public class CombatManager
{
    internal TurnManager _TurnManager = new();
    internal static UIManager _CombatUI = new();
    internal static InputHandler _inputHandler = new();
    public BattleState _currentState = new();
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
        BaseCombatant hero1 = new PlayerCombatant("slime-animation", atlasString, 25, 10, 10, 1, 1, new(5, 5), pathfinder);
        Weapon sword = new()
        {
            MoneyValue = 55,
            Weight = 5,
            Attacks = [new BasicAttack(), new SwordHeavyAttack()]
        };
        BaseCombatant enemy1 = new BasicEnemy("bat-animation", atlasString, 15, 7.5f, 7.5f, .5f, .5f, new(4, 4), pathfinder);
        _currentState = new()
        {
            map = map
        };
        _currentState.AddMap(map);
        _currentState.AddCombatant(hero1);
        _currentState.AddCombatant(enemy1);
        hero1.Equip(sword, EquipableLocation.Right_Hand);
        enemy1.Equip(sword, EquipableLocation.Right_Hand);
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
        _CombatUI.Draw(spriteBatch);
    }
}