
using CombatPOC.Logic;

namespace CombatPOC.Interfaces;

public interface IActor
{
    void JumpToNewPosition(TileLocation tileLocation);
}

public interface IActorRotuine
{
    int ParentID {get; }
    IAction[] Actions {get; }
    // Methods
    IAction ChooseAction();
}