using CombatPOC.Classes;
using CombatPOC.Entities;
using System.Collections.Generic;
using CombatPOC.Logic;
using CombatPOC.UI;
using System.Diagnostics;

namespace CombatPOC.Managers;

// TurnManger handles all the logic of the Combat System
public sealed class TurnManager
{
    private DamageCalculator _calculator = new();
    private BattleState _currentState;
    private List<CombatElement> CombatElements {get => _currentState.CombatElements;}
    private List<PlayerCombatant> PlayerTeam {get => _currentState.PlayerTeam;}
    private List<NonPlayerCombatant> EnemyTeam {get => _currentState.EnemyTeam;}

    // Constructor
    public TurnManager()
    {
        
    }
    // Public Methods
    public BattleState UpdateState(BattleState state)
    {
        _currentState = state;
        if (_currentState.State == Enum.BattleStateEnum.EnemyTeam)
        {
            foreach(NonPlayerCombatant npc in EnemyTeam)
            {
                List<Act> acts = npc.TakeTurn(PlayerTeam);
                _currentState._AnimationActionQueue.Enqueue((npc, acts));
                ImplementAction(acts);
            }
            _currentState.State = Enum.BattleStateEnum.ExecutingEnemyTurn;
        }
        if (_currentState.State == Enum.BattleStateEnum.WaitingForInput)
        {
            if (_currentState.SelectedActConfirmed == true)
            {
                ImplementAction(_currentState.SelectedAct);
                _currentState.SelectedAct = null;
                _currentState.SelectedActConfirmed = false;

            }
        }
        int downedEnemies = 0;
        foreach (NonPlayerCombatant enemy in EnemyTeam)
        {
            if (enemy.IsDowned)
            {
                Debug.WriteLine("Enemy downed");
                downedEnemies += 1;
            }
        }
        if (downedEnemies == EnemyTeam.Count)
        {
            _currentState.State = Enum.BattleStateEnum.BattleOver;
        }
        int downedPlayers = 0;
        foreach (PlayerCombatant player in PlayerTeam)
        {
            if (player.IsDowned)
            {
                downedPlayers += 1;
            }
        }
        if (downedPlayers == PlayerTeam.Count)
        {
            _currentState.State = Enum.BattleStateEnum.BattleOver;
        }
        return _currentState;
    }
    // Private Methods
    private void ImplementAction(List<Act> acts)
    {
        foreach(Act act in acts)
        {
            ImplementAction(act);
        }
    }
    private void ImplementAction(Act act)
    {
        BaseCombatant actor = act.Actor;
        List<TileLocation> locations = act.TilesActedUpon;
        if (act is AttackAct)
        {
            CombatElement[] defenders = GetCombatElementsByTiles(locations);
            foreach(CombatElement defender in defenders)
            {
                _calculator.DealDamageToDefender(actor, act, defender);
            }
        }
    }
    private CombatElement[] GetCombatElementsByTiles(List<TileLocation> locations)
    {
        List<CombatElement> output = [];
        foreach(CombatElement combatElement in CombatElements)
        {
            if (locations.Contains(combatElement.TileLocation))
                output.Add(combatElement);
        }
        return [.. output];
    }
}
