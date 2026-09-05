using CombatPOC.Interfaces;
using CombatPOC.Classes;
using CombatPOC.Enum;
using System.Diagnostics;

namespace CombatPOC.Managers;

// TurnManger handles all the logic of the Combat System
public sealed class TurnManager
{
    // Properties
    public ActionResolver actionResolver;
    public Resolutions CombatState = Resolutions.Undecided;
    public int TurnNum = 0;
    public TeamEnum TurnTeam = TeamEnum.Enemy;
    private Party Party;
    private Party Enemy;
    private Party Ally;
    // Constructor
    public TurnManager(Party PlayerParty, Party Enemies, Party Allies)
    {
        Party = PlayerParty;
        Enemy = Enemies;
        Ally = Allies;
    }
    // Methods
    public void ExectueTurn()
    /* 
    While the combat is ongoing, ExectueTurn() increments the TurnNum (first turn is TurnNum = 1)
    */
    {
        while (CombatState == Resolutions.Undecided)
        { // Open While Loop
            if (TurnTeam == TeamEnum.Player)
            {
                // ADD PLAYER CONTROL
            }
            else if (TurnTeam == TeamEnum.Enemy)
            {
                foreach (Combatant e in Enemy)
                {
                    Debug.WriteLine("Enemy Turn");
                    IAction action = e.GetAction();
                    Act act = action.Execute(e, TurnNum);
                    actionResolver.ResolveAction(act, Party + Enemy + Ally);
                }
                TurnNum ++;
            }
            else if (TurnTeam == TeamEnum.Ally)
            {
                foreach (Combatant a in Ally)
                {
                    Debug.WriteLine("Ally Turn");
                    IAction action = a.GetAction();
                    Act act = action.Execute(a, TurnNum);
                    actionResolver.ResolveAction(act, Party + Enemy + Ally);
                }
                TurnNum ++;
            }
            CombatEnded();
        } // Close While Loop
    }
    public void LoadBattleState(BattleState state)
    {
        CombatState = state.Resolution;
        TurnNum = state.TurnNum;
        Party = state.Party;
        Enemy = state.Enemy;
        Ally = state.Ally;
    }
    public void CombatEnded()
    {
        if (Party.IsEveryoneDowned() == true)
        {
            CombatState = Resolutions.PartyLoses;
        }
        if (Enemy.IsEveryoneDowned() == true)
        {
            CombatState = Resolutions.PartyWins;
        }
    }
    public void NextPartyTurn() // Cycles the TurnTeam property
    {
        switch (TurnTeam)
        {
            case TeamEnum.Player:
                TurnTeam = TeamEnum.Enemy;
            break;
            case TeamEnum.Enemy:
                TurnTeam = TeamEnum.Ally;
            break;
            case TeamEnum.Ally:
                TurnTeam = TeamEnum.Player;
            break;
        }
    }
    public void AddParty(TeamEnum team, Party party)
    {
        switch (team)
        {
            case TeamEnum.Player:
                if (Party == null)
                    Party = party;
                else
                    return;
            break;
            case TeamEnum.Enemy:
                if (Enemy == null)
                    Party = party;
                else
                    return;
            break;
            case TeamEnum.Ally:
                if (Party == null)
                    Ally = party;
                else
                    return;
            break;
        }
    }
}
