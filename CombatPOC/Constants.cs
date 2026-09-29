using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CombatPOC;


public class Constants
{
    public static string Str {get; } = "str";
    public static string Dex {get; } = "dex";
    public static int EliteMaxMoves {get; } = 3;
    public static int GruntMaxMoves {get; } = 2;
    public static int PlayerMaxMoves {get; } = 3;
    public static Color MoveColor {get; } = Color.Green;
    public static Color AttackColor {get; } = Color.Red;
    public static float BasicAttackStrength {get; } = 1;
    public static float BasicAttackCost {get; } = 1;
    public static Dictionary<string, float> BasicSwordStats { get; } = new Dictionary<string, float> { { Str, 1.75f }, { Dex, 1.75f } };
    public static float HeavyAttackCost {get; } = 2;
    public static float HeavyAttackStrength {get; } = 1.5f;

}