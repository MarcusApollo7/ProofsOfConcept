using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Managers;
using CombatPOC.Paths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;

namespace CombatPOC;

public class CombatPOC : Core
{
    public Texture2D _whiteRectangle;
    public static Vector2 _scale = new(4.0f, 4.0f);
    private CombatManager _combatManager = new();
    public CombatPOC() : base("CombatPOC", 1280, 720, false)
    {
        
    }

    protected override void Initialize()
    {
        // Init Base Class
        base.Initialize();
        _whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
        _whiteRectangle.SetData([Color.White]);
        // Initial slime position will be the center tile of the tile map
        
    }
    protected override void LoadContent()
    {

        // Init combatManger
        _combatManager.Initialize();
        _combatManager.LoadContent();
        

    }
    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        base.Update(gameTime);
        _combatManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin();

        _combatManager.Draw(SpriteBatch, _whiteRectangle);

        // Always end the sprite batch when finished.
        SpriteBatch.End();
    }

    
}
