using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
using CombatPOC.Paths;
using CombatPOC.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Managers;

public class CombatManager
{
    internal static CombatHelper _CombatHelper = new();
    internal static TurnManager _TurnManager;
    internal static UIManager _UIManager;
    internal static EntityManager _EntityManager = new();
    internal static AStar _pathFinder = new();
    internal static SelectableManager _selectionManger = new();
    internal static InputHandler _inputHandler = new();
    internal static MapEntity map;
    private List<IRenderable> _renderables = new();
    public CombatManager()
    {
        
    }
    
    public void Initialize(GraphicsDevice graphicsDevice)
    {
        _UIManager = new();
        _UIManager.Initialize(graphicsDevice);
        _TurnManager = new(); 
        map = new ("tilemap-walk-definition.xml");
        _pathFinder.Initialize(map.LoadContent());
    }
    public void LoadContent()
    {
        Combatant hero1 = new(10, 10, 10, 10, 100, new(5, 5), TeamEnum.Player, "slime-animation");
        _TurnManager.AddCombatant(hero1);
        Combatant enemy1 = new(10, 10, 10, 10, 100, new(4, 4), TeamEnum.Enemy, "bat-animation");
        _TurnManager.AddCombatant(enemy1);
    }
    public void Update(GameTime gameTime)
    {
        _TurnManager.Update(gameTime);
        _renderables = _TurnManager.GetRenderables();
        _UIManager.Update(gameTime);
        _selectionManger.Update(gameTime);
        _inputHandler.Update(gameTime);
        foreach(IRenderable renderable in _renderables)
        {
            renderable.Update(gameTime);
        }
        map.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        map.Draw(spriteBatch);
        _UIManager.Draw(spriteBatch, _renderables);
    }
}