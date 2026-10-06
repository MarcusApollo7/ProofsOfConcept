using CombatPOC.Logic;
using Microsoft.Xna.Framework;

namespace CombatPOC.UI.Basics;

public class PhantomElement: ScreenElement
{
    public TileLocation TileLocation {get; set; }
    public PhantomElement(TileLocation tileLocation, string name, string atlasName): base(tileLocation.ToScreenPosition(), name, atlasName)
    {
        TileLocation = tileLocation;
        Sprite.Color = Color.White * 0.5f;
        Sprite.LayerDepth = Constants.PhantomDepth;
    }
    public void ResetScreenPosition()
    {
        ScreenPosition = TileLocation.ToScreenPosition();
    }
}