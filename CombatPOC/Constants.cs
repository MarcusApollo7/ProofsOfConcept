using Microsoft.Xna.Framework;

namespace CombatPOC;


public class Constants
{
    public static int EliteMaxMoves {get; } = 3;
    public static int GruntMaxMoves {get; } = 2;
    public static int PlayerMaxMoves {get; } = 3;
    public static Color MoveColor {get; } = Color.Green;
    public static Color AttackColor {get; } = Color.Red;
    public static float BasicAttackStrength {get; } = 1;
    public static float BasicAttackCost {get; } = 1;
    public static float[] BasicSwordStats {get; } = [1.1f, 1.1f];
    public static float HeavyAttackCost {get; } = 2;
    public static float HeavyAttackStrength {get; } = 1.5f;

}