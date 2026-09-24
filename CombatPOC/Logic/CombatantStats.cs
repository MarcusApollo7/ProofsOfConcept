using System;
using System.Collections.Generic;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Managers;
using CombatPOC.Paths;
using CombatPOC.stats_skills;

namespace CombatPOC.Logic;
public record class CombatantStats: IComponent
{
    public int EntityID {get; }
    public int ComponentID {get; } = EntityManager.CreateNewComponentID();
    private readonly Stat Str;
    private readonly Stat Dex;
    private readonly Stat Eva;
    private readonly Stat Tgh;
    public Stat[] AttackStats {get => [Str, Dex]; }
    public Stat[] DefenseStats {get => [Eva, Tgh]; }
    public CombatantStats(int entityID , float str, float dex, float eva, float tgh)
    {
        EntityID = entityID;
        Str = new(str);
        Dex = new(dex);
        Eva = new(eva);
        Tgh = new(tgh);
    }
    
}