/* using System.Collections.Generic;
using CombatPOC.Enum;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class BasicAttack: IAction
{
    public string ActionName {get; }
    public float BaseStrength {get; set;}
    public IActionPattern ActionPattern {get; } = new BasicActionPattern();
    public List<ActionEffect> ActionEffects {get; } = [ActionEffect.physical];
    public Act Execute(Combatant source, int turnnum)
    {
        List<PositionComponent> positionsactedupon = [];
        foreach(PositionComponent position in ActionPattern.Pattern)
        {
            positionsactedupon.Add(source.CombatantStats.CombatantPosition+position);
        }
        return new(this, source, positionsactedupon, turnnum);
    }

} */