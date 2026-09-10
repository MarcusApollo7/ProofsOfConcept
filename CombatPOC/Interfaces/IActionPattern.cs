using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Enum;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;

namespace CombatPOC.Interfaces;

public interface IActionPattern
{
    TileLocation[] Pattern {get; }
    TileLocation[] RotatePattern(CharacterDirection characterDirection)
    {
        int cosAngle = 1;
        int sinAngle = 0;
        TileLocation[] output = new TileLocation[Pattern.Length];
        if (characterDirection == CharacterDirection.Right)
        {
            // signs of sin are flipped due to positive y direction being down
            cosAngle = 0;
            sinAngle = 1;
        }
        else if (characterDirection == CharacterDirection.Down)
        {
            cosAngle = -1;
            sinAngle = 0;
        }
        else if (characterDirection == CharacterDirection.Left)
        {
            // signs of sin are flipped due to positive y direction being down
            cosAngle = 0;
            sinAngle = -1;
        }
        for(int i = 0; i < Pattern.Length; i++)
        {
            output[i] = new(Pattern[i].X*cosAngle-Pattern[i].Y*sinAngle, 
                            Pattern[i].X*sinAngle+Pattern[i].Y*cosAngle);
        }

        return output;
    }
            
}
