using CombatPOC.Logic;
using CombatPOC.stats_skills;

namespace CombatPOC.Interfaces;

public interface IDefender
{
    float CurHealth {get; set; }
    float DefenseRating {get; set; }
    TileLocation TileLocation {get; set; }
    void ChangeHealth(float dmg);
}