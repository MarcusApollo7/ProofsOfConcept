
using System;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class GruntActor: IActor
{
    // Properties
    Random random = new();
    public IAction ChooseAction(IAction[] actions)
    {
        return actions[random.Next(0, actions.Length)];
    }
}