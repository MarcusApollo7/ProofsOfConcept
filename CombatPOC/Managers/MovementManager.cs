using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Entities;
using CombatPOC.Logic;
using CombatPOC.Paths;
using Microsoft.Xna.Framework;

namespace CombatPOC.Managers;

public class MovementManger
{
    private Timer _movementTimer = new(1f);
    private List<TileLocation> Path {get; set;}
    private Combatant MovedObject {get; set;}
    public void SetObjectPath(Combatant movedObject, List<TileLocation> locations)
    {
        MovedObject = movedObject;
        Path = locations;
        _movementTimer.Start();
    }
    public void Update(GameTime gameTime)
    {
        if (Path.Count >= 1)
        {
            bool timeEnded = _movementTimer.Update(gameTime);
            if (timeEnded)
            {
                MovedObject.JumpToNewPosition(Path[0]);
                Path.RemoveAt(0);
                _movementTimer.Start();
            }
        } 
        if (Path.Count == 0)
        {
            _movementTimer.Stop();
        }
    }

}