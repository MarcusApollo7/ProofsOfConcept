using CombatPOC.Interfaces;
using CombatPOC.Classes;
using CombatPOC.Entities;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Managers;

// TurnManger handles all the logic of the Combat System
public sealed class TurnManager
{
    // Properties
    public ActionResolver _actionResolver;
    public Resolutions CombatState = Resolutions.Undecided;
    public static int TurnNum = 0;
    public TeamEnum TurnTeam = TeamEnum.Enemy;
    // Constructor
    public TurnManager()
    {
        
    }
    // Methods
    public void ExectueTurn()
    /* 
    
    */
    {
        while (CombatState == Resolutions.Undecided)
        { // Open While Loop
            if (TurnTeam == TeamEnum.Enemy)
            {
                foreach(Combatant combatant in CombatManager._combatants)
                {
                    if (combatant._stats._team == TeamEnum.Enemy)
                    {
                        Combatant target = combatant.FindTarget();
                        combatant.GetMovesFromPathfinder(target);
                    }
                }
            }
        } // Close While Loop
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
    
}
