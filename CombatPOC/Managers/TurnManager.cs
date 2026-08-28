using CombatPOC.Interfaces;
using CombatPOC.Classes;
using CombatPOC.Enum;

namespace CombatPOC.Managers;

public sealed class TurnManager
{
    // Properties
    public ActionResolver actionResolver;
    public Resolutions CombatState = Resolutions.Undecided;
    public int TurnNum = 0;
    public TurnEnum TurnTeam = TurnEnum.Enemy;
    private static Party Party;
    private static Party Enemy;
    private static Party Ally;
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
    Waits for player input, then iterates through each team and acts for each character
    */
    {
        while (CombatState == Resolutions.Undecided)
        { // Open While Loop
            TurnNum ++;
            if (TurnTeam == TurnEnum.Player)
            {
                // ADD PLAYER CONTROL
            }
            else if (TurnTeam == TurnEnum.Enemy)
            {
                foreach (ICombatant e in Enemy)
                {
                    IAction action = e.GetAction();
                    Act act = action.Execute(e, TurnNum);
                    actionResolver.ResolveAction(act);
                }
            }
            else if (TurnTeam == TurnEnum.Ally)
            {
                foreach (ICombatant a in Ally)
                {
                    IAction action = a.GetAction();
                    Act act = action.Execute(a, TurnNum);
                    actionResolver.ResolveAction(act);
                }
            }
        } // Close While Loop
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
    public static Party Combatants() // Gets all ICombatants as a Party
    {
        return Party + Ally + Enemy;
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

}
