
using System;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;


public class PlayerActor: IActor
{
    // Properties
    Random random = new();
    public IAction ChooseAction(IAction[] actions)
    {
        return actions[random.Next(0, actions.Length)];
    }
}

public class GruntActor: IActor
{
    // Properties
    Random random = new();
    // Methods
    public IAction ChooseAction(IAction[] actions) // Just picks a random action
    {
        return actions[random.Next(0, actions.Length)];
    }
}