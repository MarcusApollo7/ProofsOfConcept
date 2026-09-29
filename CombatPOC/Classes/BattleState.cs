using System.Collections.Generic;
using CombatPOC.Entities;
using CombatPOC.Enum;
using CombatPOC.Logic;
using CombatPOC.UI;
using CombatPOC.UI.Animation;

namespace CombatPOC.Classes;

public class BattleState
{
    public BattleStateEnum State {get; set;}
    public int TurnNum {get; }
    public List<ScreenElement> ScreenElements {get; set; }
    public List<CombatElement> CombatElements
    {
        get
        {
            List<CombatElement> output = [];
            foreach(ScreenElement element in ScreenElements)
            {
                if (element is CombatElement ce)
                    output.Add(ce);
            }
            return output;
        }
    }
    public List<BaseCombatant> Combatants {get
        {
            List<BaseCombatant> output = [];
            foreach(CombatElement combatElement in CombatElements)
            {
                if (combatElement is BaseCombatant combatant)
                {
                    output.Add(combatant);
                }
            }
            return output;
        }
    }
    public List<PlayerCombatant> PlayerTeam
    {
        get
        {
            List<PlayerCombatant> output = [];
            foreach(BaseCombatant combatant in Combatants)
            {
                if (combatant is PlayerCombatant player)
                    output.Add(player);
            }
            return output;
        }
    }
    public List<NonPlayerCombatant> EnemyTeam
    {
        get
        {
            List<NonPlayerCombatant> output = [];
            foreach(BaseCombatant combatant in Combatants)
            {
                if (combatant is NonPlayerCombatant enemy)
                    output.Add(enemy);
            }
            return output;
        }
    }
    public Act SelectedAct;
    public CombatElement SelectedElement;
    private bool? _selectedActConfirmed;
    public bool SelectedActConfirmed
    {
        get
        {
            if (_selectedActConfirmed == null || !_selectedActConfirmed == false)
                return false;
            else
                return true;
        }
        set
        {
            _selectedActConfirmed = value;
        }
    }
    public GameMap map;
    public Queue<(IAnimatable, List<Act>)> _AnimationActionQueue;
    public BattleState()
    {
        TurnNum = 0;
        ScreenElements = [];
        SelectedAct = null;
        SelectedElement = null;
        _selectedActConfirmed = null;
        _AnimationActionQueue = new();
        State = BattleStateEnum.EnemyTeam;
    }
    public BattleState(List<ScreenElement> elements): this()
    {
        ScreenElements = elements;
    }
}