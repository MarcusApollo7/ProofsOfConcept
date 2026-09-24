using System;
using CombatPOC.Entities;
using CombatPOC.Interfaces;

namespace CombatPOC.Logic;

public delegate void OnSetPathHandler<PathEventArgs>(IMoveable sender, PathEventArgs e);

public class PathEventArgs: EventArgs
{
    public IEntity Entity {get; set; }
    public Act PathAct {get; set;}
}

public interface IMoveable
{
    TileLocation TileLocation {get; set;}
    int TilesPerMove {get; set; }
    void Move(TileLocation newLocation);
    void MovePath(Act path);
}