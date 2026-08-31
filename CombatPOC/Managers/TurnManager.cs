using CombatPOC.Interfaces;
using CombatPOC.Classes;
using CombatPOC.Enum;
using System.Diagnostics;

namespace CombatPOC.Managers;

public sealed class TurnManager
{
    // Properties
    public ActionResolver actionResolver;
    public Resolutions CombatState = Resolutions.Undecided;
    public int TurnNum = 0;
    public TurnEnum TurnTeam = TurnEnum.Enemy;
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
    public TurnManager()
    {
        Party = null;
        Enemy = null;
        Ally = null;
    }
    // Methods
    public void ExectueTurn()
    /* 
    While the combat is ongoing, ExectueTurn() increments the TurnNum (first turn is TurnNum = 1)
    Waits for player input, then iterates through each team and acts for each character
    */
    {
        while (CombatState == Resolutions.Undecided)
        { // Open While Loop
            if (TurnTeam == TurnEnum.Player)
            {
                // ADD PLAYER CONTROL
            }
            else if (TurnTeam == TurnEnum.Enemy)
            {
                foreach (ICombatant e in Enemy)
                {
                    Debug.WriteLine("Enemy Turn");
                    IAction action = e.GetAction();
                    Act act = action.Execute(e, TurnNum);
                    actionResolver.ResolveAction(act, Party + Enemy + Ally);
                }
                TurnNum ++;
            }
            else if (TurnTeam == TurnEnum.Ally)
            {
                foreach (ICombatant a in Ally)
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
            case TurnEnum.Player:
                TurnTeam = TurnEnum.Enemy;
            break;
            case TurnEnum.Enemy:
                TurnTeam = TurnEnum.Ally;
            break;
            case TurnEnum.Ally:
                TurnTeam = TurnEnum.Player;
            break;
        }
    }
    public void AddParty(TurnEnum team, Party party)
    {
        switch (team)
        {
            case TurnEnum.Player:
                if (Party == null)
                    Party = party;
                else
                    return;
            break;
            case TurnEnum.Enemy:
                if (Enemy == null)
                    Party = party;
                else
                    return;
            break;
            case TurnEnum.Ally:
                if (Party == null)
                    Ally = party;
                else
                    return;
            break;
        }
    }
}
