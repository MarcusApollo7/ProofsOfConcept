using System;
using Microsoft.Xna.Framework;

namespace CombatPOC.Managers;

public class Timer
{
    private float _timeRemaining;
    private readonly float _interval;
    private bool _isLooping;
    public bool IsRunning {get; private set; }
    public Timer(float intervalSeconds, bool isLooping = false)
    {
        if (intervalSeconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(intervalSeconds), "Interval must be positive.");
        _interval = intervalSeconds;
        _isLooping = isLooping;

    }
    public void Start()
    {
        _timeRemaining = _interval; 
        IsRunning = true;
    }
    public void Stop()
    {
        IsRunning = false;
    }
    
    public bool Update(GameTime gameTime)
    {
        if (!IsRunning) return false;
        _timeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeRemaining <= 0)
        {
            if (_isLooping)
                _timeRemaining += _interval;
            else
                IsRunning = false;
            return true;
        }
        return false;
    }
}
