using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;
using POCLibrary.Interfaces;

namespace CombatPOC.Managers;

public class UIManager
{
    public Texture2D _whiteRectangle;
    public static MovementManger _movementManger = new();
    private List<IRenderable> _renderables = [];
    public void Initialize(GraphicsDevice graphicsDevice)
    {
        _whiteRectangle = new Texture2D(graphicsDevice, 1, 1);
        _whiteRectangle.SetData([Color.White]);
    }
    public void AddRenderable(IRenderable renderable)
    {
        _renderables.Add(renderable);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(IRenderable renderable in _renderables)
        {
            renderable.Draw(spriteBatch);
        }
    }
    public void Update(GameTime gameTime, MouseInfo mouseInfo)
    {
        _movementManger.Update(gameTime);
        foreach(IRenderable renderable in _renderables)
        {
            renderable.Update(gameTime);
            renderable.CheckClickHover(mouseInfo);
        }
    }
    public void MoveRenderable(IRenderable renderable)
    {
        
    }
}