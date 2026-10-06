using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CombatPOC;


public class Constants
{
    public const string Str = "str";
    public const string Dex = "dex";
    public const int EliteMaxMoves = 3;
    public const int GruntMaxMoves = 2;
    public const int PlayerMaxMoves = 3;
    public static Color MoveColor => Color.Green;
    public static Color AttackColor => Color.Red;
    public const float BasicAttackStrength = 1;
    public const float BasicAttackCost = 1;
    public static Dictionary<string, float> BasicSwordStats  => new() { { Str, 1.75f }, { Dex, 1.75f } };
    public const float HeavyAttackCost = 2;
    public const float HeavyAttackStrength = 1.5f;
    public const float ActDepth = .1f;
    public const float CombatDepth = 1f;
    public const float PhantomDepth = .99f;
    public const float MapDepth = 0f;

}