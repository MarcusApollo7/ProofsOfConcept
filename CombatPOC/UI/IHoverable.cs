using Microsoft.Xna.Framework;
using POCLibrary.Input;

namespace CombatPOC.UI;

public interface IHoverable
{
    Rectangle _spriteRectangle {get; }
    bool Hovered {get; set; }
    void OnHover(MouseInfo mouseInfo)
    {
        if(_spriteRectangle.Contains(mouseInfo.CurrentState.Position))
            Hovered = true;
    }
    void ClearHover()
    {
        Hovered = false;
    }
}