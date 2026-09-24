using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Logic;
using CombatPOC.Managers;
using CombatPOC.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.UI;

public class AnimationManager
{
    private readonly Timer _movementTimer = new(1f);
    private bool _animationFinished = true;
    private IEntity _activeEntity;
    private Act _activeAct;
    private readonly List<Act> _renderActs = [];
    private Queue<(IEntity, Act)> _animationQueue = new();
    public void AddToMoveQueue(IMoveable sender, PathEventArgs e)
    {
        _animationQueue.Enqueue((e.Entity, e.PathAct));
        _movementTimer.Start();
    }
    public void AddToAttackQueue(IActor sender, AttackEventArgs e)
    {
        _animationQueue.Enqueue((e.Entity, e.Attack));
    }
    public void Update(GameTime gameTime)
    {
        if (CheckQueue())
        {
            SetActiveElements();
            if (_activeAct.Action is MoveAction)
            {
                List<TileLocation> path = _activeAct.TilesActedUpon;
                if (path.Count >= 1)
                {
                    bool timeEnded = _movementTimer.Update(gameTime);
                    if (timeEnded)
                    {
                        if (_activeEntity is Combatant combatant)
                        {
                            combatant.Move(path[0]);
                            _activeAct.TilesActedUpon.RemoveAt(0);
                            _movementTimer.Start();
                            return;   
                        }
                    }
                } 
                if (path.Count == 0)
                {
                    _movementTimer.Stop();
                    _animationFinished = true;
                    return;
                }
            }
            if (_activeAct.Action is Attack)
            {
                _renderActs.Add(_activeAct);
                _animationFinished = true;
            }
        }    
    }
    private bool CheckQueue()
    {
        if (_animationQueue.Count == 0 && _animationFinished)
            return false;
        else
            return true;
    }
    private void SetActiveElements()
    {
        if (_animationFinished)
        {
            _animationFinished = false;
            var item =_animationQueue.Dequeue(); 
            _activeEntity = item.Item1;
            _activeAct = item.Item2; 
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _activeAct?.Draw(spriteBatch);
        foreach(Act renderAct in _renderActs)
        {
            renderAct.Draw(spriteBatch);
        }
    }
    public bool AnimationFinished()
    {
        return _animationFinished;
    }
    public void ResetRenderActs()
    {
        _renderActs.Clear();
    }
}