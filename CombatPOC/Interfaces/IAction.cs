namespace CombatPOC.Interfaces;

public interface IAction
{
    // Properties
    string ActionName {get; }
    double BaseStrength {get; set; }
    // Methods
    void Execute(ICombatant source, ICombatant target);

}