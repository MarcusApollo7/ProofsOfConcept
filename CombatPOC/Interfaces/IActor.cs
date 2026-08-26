
namespace CombatPOC.Interfaces;

public interface IActor
{
    // Methods
    IAction ChooseAction(IAction[] actions);
}