using Microsoft.Xna.Framework;
using POCLibrary.Input;

namespace POCLibrary.Interfaces;

public interface IClickable
{
    Rectangle _spriteRectangle {get; set;}
    bool Selected {get; set; }
    void OnClick(MouseInfo mouseInfo);
    void ClearSelect();
}