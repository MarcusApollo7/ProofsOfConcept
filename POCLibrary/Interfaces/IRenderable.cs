using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Input;

namespace POCLibrary.Interfaces;

public interface IRenderable
{
    string Name {get; }
    void Draw(SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
    void CheckClickHover(MouseInfo mouseInfo);
}