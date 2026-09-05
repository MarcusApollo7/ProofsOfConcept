using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class CombatantSprite(Vector2 screenposition)
{
    public AnimatedSprite _animatedSprite;
    public Vector2 _screenPosition = screenposition;
    public Rectangle _spriteRectangle;
    // Methods
    public void Initialize()
    {
        
    }
    public void LoadContent(TextureAtlas atlas, string spritename, Vector2 scale)
    {
        _animatedSprite = atlas.CreateAnimatedSprite(spritename);
        _animatedSprite.Scale = scale;
    }
    public void Update(GameTime gameTime)
    {
        _animatedSprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _animatedSprite.Draw(spriteBatch, _screenPosition);
    }
}