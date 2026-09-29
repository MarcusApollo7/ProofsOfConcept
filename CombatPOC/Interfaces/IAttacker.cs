using CombatPOC.Item;
using CombatPOC.Logic;
using CombatPOC.stats_skills;

namespace CombatPOC.Interfaces;

public interface IAttacker
{
    float AttackRating {get; }
}