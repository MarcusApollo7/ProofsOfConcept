using CombatPOC.Item;
using CombatPOC.Logic;
using CombatPOC.stats_skills;

namespace CombatPOC.Interfaces;

public interface IAttacker
{
    float AttackRating {get; set; }
    ItemBase RightItem {get; set; }
    Stat MaxHealth {get;}
    float CurHealth {get; set; }
    bool IsDowned {get => CurHealth > 0;}
}