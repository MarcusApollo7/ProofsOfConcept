using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Entities;
using CombatPOC.Managers;
using CombatPOC.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Input;

namespace CombatPOC.UI;

public class UIManager
{
    public Texture2D _whiteRectangle;
    public AnimationManager _animationManager = new();
    public void Initialize(GraphicsDevice graphicsDevice)
    {
        _whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
        _whiteRectangle.SetData([Color.White]);
    }
    public void Draw(SpriteBatch spriteBatch, List<IRenderable> renderables)
    {
        foreach(IRenderable renderable in renderables)
        {
            renderable.Draw(spriteBatch);
        }
        _animationManager.Draw(spriteBatch);
    }
    public void Update(GameTime gameTime)
    {
        _animationManager.Update(gameTime);
    }
}