using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.UI.Animation;
using CombatPOC.Entities;
using CombatPOC.Logic;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.UI.Basics;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public class UIManager
{
    public Texture2D _whiteRectangle;
    private readonly AnimationManager _animationManager = new();
    public SpriteFont Font {get; set;}
    private BattleState _currentState;
    private List<ScreenElement> ScreenElements {get => _currentState.ScreenElements; }
    private List<BaseCombatant> Combatants {get => _currentState.Combatants; }
    private Queue<(IAnimatable, List<Act>)> AnimationQueue {get => _currentState._AnimationActionQueue; }
    public void Initialize(GraphicsDevice graphicsDevice)
    {
        _whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
        _whiteRectangle.SetData([Color.White]);
        Font = Content.Load<SpriteFont>("fonts/font");

    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(ScreenElement element in ScreenElements)
            element.Draw(spriteBatch);
        _currentState.SelectedAct?.Draw(spriteBatch);
        _animationManager.Draw(spriteBatch);
    }
    public BattleState Update(GameTime gameTime, BattleState state)
    {
        _currentState = state;
        while (AnimationQueue.Count > 0)
        {
            var item = AnimationQueue.Dequeue();
            SubmitActForAnimation(item.Item1, item.Item2);
        }
        foreach(BaseCombatant combatant in Combatants)
        {
            combatant.Update(gameTime);
        }
        if (_animationManager.Update(gameTime))
        {
            _currentState.State = BattleStateEnum.WaitingForInput;
        }
        return _currentState;
    }
    private void SubmitActForAnimation(IAnimatable animatable, List<Act> acts)
    {
        foreach(Act act in acts)
            _animationManager.AddToAnimationQueue(animatable, act);
    }
}