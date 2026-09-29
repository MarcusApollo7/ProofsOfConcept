using CombatPOC.Logic;

namespace CombatPOC.UI.Animation;

public interface IAnimatable
{
    void MoveToNewLocation(TileLocation tileLocation);
}