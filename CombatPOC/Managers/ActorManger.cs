using System.Collections.Generic;
using CombatPOC.Interfaces;

namespace CombatPOC.Managers;

public class ActorManager
{
    static List<IActor> _actors = [];
    static int _actorNumber = 0;
    public static int CreateNewActorID()
    {
        _actorNumber++;
        return _actorNumber - 1;
    }
    static public IActor GetActor(int actorID)
    {
        return _actors[actorID];
    }
    static public void AddActor(IActor actor)
    {
        _actors.Add(actor);
    }


}