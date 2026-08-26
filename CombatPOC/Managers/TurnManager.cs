using CombatPOC.Interfaces;
using CombatPOC.Classes;

namespace CombatPOC.Managers;

public class TurnManager
{
    // Properties
    public Resolutions combatState = Resolutions.Undecided;
    public TurnEnum turn;
    public Party party;
    public Party enemy;
    public Party ally;
    // Constructor
    public TurnManager(Party PlayerParty, Party Enemies, Party Allies)
    {
        party = PlayerParty;
        enemy = Enemies;
        ally = Allies;
    }
    // Methods
    public void ExectueTurn()
    {
        while (combatState == Resolutions.Undecided)
        {
            if (turn == TurnEnum.Player)
            {
                // ADD PLAYER CONTROL
            }
            else
            {
                if (turn == TurnEnum.Enemy)
                {
                    foreach (ICombatant e in enemy)
                    {
                        e.GetAction();
                    }
                }
                if (turn == TurnEnum.Ally)
                {
                    foreach (ICombatant a in ally)
                    {
                        a.GetAction();
                    }
                }
            }
        }
    }
    public void CombatEnded()
    {
        if (party.IsEveryoneDowned() == true)
        {
            combatState = Resolutions.PartyLoses;
        }
        if (enemy.IsEveryoneDowned() == true)
        {
            combatState = Resolutions.PartyWins;
        }
    }

}

public enum TurnEnum
{
    Player = 0,
    Enemy = 1,
    Ally = 2
}

public enum Resolutions
{
    PartyWins,
    PartyLoses,
    Undecided
}
