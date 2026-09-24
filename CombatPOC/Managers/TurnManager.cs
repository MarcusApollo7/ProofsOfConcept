using CombatPOC.Classes;
using CombatPOC.Entities;
using System.Collections.Generic;
using CombatPOC.Logic;
using CombatPOC.UI;
using Microsoft.Xna.Framework;

namespace CombatPOC.Managers;

// TurnManger handles all the logic of the Combat System
public sealed class TurnManager
{
    private DamageCalculator _calculator = new();
    internal static int TurnNum = 0;
    private TeamEnum TurnTeam = TeamEnum.Enemy;
    private Resolutions CombatState = Resolutions.Undecided;
    private Act PlayerAct = null;
    private readonly Queue<List<Act>> _ActionQueue = new();
    private readonly List<Combatant> _actors = [];
    private readonly List<Combatant> _foes = [];
    private readonly List<Combatant> _friends = [];

    // Constructor
    public TurnManager()
    {
        
    }
    // Methods
    public void Reset()
    {
        TurnTeam = TeamEnum.Enemy;
    }
    public void AddCombatant(Combatant actor)
    {
        _actors.Add(actor);
        if (actor.Team == TeamEnum.Enemy)
            _foes.Add(actor);
        else
            _friends.Add(actor);
    }
    private void GetNonPlayerActs()
    {
        BattleState state = GetBattleState();
        foreach(Combatant combatant in _actors)
        {
            if (combatant.Team != TeamEnum.Player)
            {
                List<Act> turnAct = combatant.TakeTurn(state);
                _ActionQueue.Enqueue(turnAct);
            }
        }
        TurnTeam = TeamEnum.Player;
    }
    private void GetPlayerActs()
    {
        if (CombatManager._inputHandler.ActiveEntity is Combatant player && player.ActorRoutine is PlayerActor)
        {
            player.TakeTurn(GetBattleState());
        }
    }
    private BattleState GetBattleState()
    {
        return new(CombatState, TurnNum, _friends, _foes);
    }
    public void Update(GameTime gameTime)
    {
        if (TurnTeam != TeamEnum.Player)
        {
            GetNonPlayerActs();
        }
        else
        {
            GetPlayerActs();
        }
        if (_ActionQueue.Count >= 1)
        {
            List<Act> acts = _ActionQueue.Dequeue();
            ImplementAction(acts);
        }
    }
    private void ImplementAction(List<Act> acts)
    {
        foreach(Act act in acts)
        {
            IAction action = act.Action;
            Combatant actor = act.ActorCombatant;
            List<TileLocation> locations = act.TilesActedUpon;
            if (action is MoveAction)
            {
                actor.MovePath(act);
            }
            if (action is Attack)
            {
                actor.DoAttack(act);
                Combatant[] defenders = GetCombatantsByTiles(locations);
                foreach(Combatant defender in defenders)
                {
                    _calculator.DealDamageToDefender(actor, act, defender);
                }
            }
        }
    }
    private Combatant[] GetCombatantsByTiles(List<TileLocation> locations)
    {
        List<Combatant> output = [];
        foreach(Combatant combatant in _actors)
        {
            if (locations.Contains(combatant.TileLocation))
                output.Add(combatant);
        }
        return [.. output];
    }
    public List<IRenderable> GetRenderables()
    {
        List<IRenderable> output = [];
        foreach(Combatant combatant in _actors)
        {
            output.Add(combatant);
        }
        if (PlayerAct != null)
        {
            output.Add(PlayerAct);
        }
        return output;
    }
    
    public void SetPlayerAct(Act act)
    {
        if (PlayerAct == act) return;
        PlayerAct = act;
    }
    public Act ReturnPlayerAct()
    {
        return PlayerAct;
    }
}
