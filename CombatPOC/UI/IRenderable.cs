using CombatPOC.Interfaces;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Input;

namespace CombatPOC.UI;

public interface IRenderable
{
    void Draw(SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
}