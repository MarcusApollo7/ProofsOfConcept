using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.UI.Animation;

public class AnimationManager
{
    private bool _animationFinished = true;
    private IAnimatable _animatable;
    private Act _act;
    private Queue<(IAnimatable, Act)> _animationQueue = new();
    public void AddToAnimationQueue(IAnimatable animatable, Act act)
    {
        Debug.WriteLine("Adding Animation");
        _animationQueue.Enqueue((animatable, act));
    }
    public bool Update(GameTime gameTime)
    {
        if (_animationQueue.Count > 0 && _animationFinished)
        {
            if (_animationFinished)
            {
                _animationFinished = false;
                var item =_animationQueue.Dequeue(); 
                _animatable = item.Item1;
                _act = item.Item2; 
                _act.StartAnimationTimer();
            }
        }
        if (_act != null)
        {
            _animationFinished = _act.Animate(_animatable, gameTime);
            if (_animationFinished)
                _act = null;
        }
        return _animationFinished;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _act?.Draw(spriteBatch);
    }
}