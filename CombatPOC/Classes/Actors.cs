
using System;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public abstract class CombatantActorRoutine(int parentId): IActorRotuine
{
    public int ParentID {get; } = parentId;
    public abstract IAction[] Actions {get; }
    public abstract IAction ChooseAction();
}

public class PlayerActor:  CombatantActorRoutine
{
    public override IAction[] Actions {get; } = [new DiagonalAttack()];
    public PlayerActor(int parentId) : base(parentId)
    {
        _selectedActionIndex = 0;
    }
    // Properties
    int _selectedActionIndex;
    public override IAction ChooseAction()
    {
        return Actions[_selectedActionIndex];    
    }
}
public class GruntActor: CombatantActorRoutine
{
    public override IAction[] Actions {get; } = [new BasicAttack()];
    public GruntActor(int parentId) : base(parentId)
    {
        
    }
    // Properties
    readonly Random random = new();
    // Methods
    public override IAction ChooseAction() // Just picks a random action
    {
        return Actions[random.Next(0, Actions.Length)];
    }
}