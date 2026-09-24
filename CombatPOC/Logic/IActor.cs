using System;
using CombatPOC.Entities;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.stats_skills;

namespace CombatPOC.Logic;


public delegate void OnAttackEventHandler<AttackEventArgs>(IActor sender, AttackEventArgs e);
public class AttackEventArgs : EventArgs
{
    public IEntity Entity {get; init; } 
    public Act Attack {get; init; }
}

public interface IActor
{
    CharacterDirection ActorDirection {get; set; }
    Stat[] AttackStats {get;}
    Stat[] DefenseStats {get;}
    void DoAttack(Act attack);
}


