
using System;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class PlayerActor: IActor
{
    public IAction ChooseAction(IAction[] actions)
    {
        return new BasicAttack();
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