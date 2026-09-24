using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.UI;

public interface IRenderable
{
    void Draw(SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
}