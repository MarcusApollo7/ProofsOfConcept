
namespace CombatPOC.Interfaces;

public interface IActor
{
    
}

public interface IActorRotuine
{
    int ParentID {get; }
    IAction[] Actions {get; }
    // Methods
    IAction ChooseAction();
}