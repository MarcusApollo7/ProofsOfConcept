using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Logic;
using CombatPOC.Managers;
using CombatPOC.UI;
using CombatPOC.UI.Basics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary;

namespace CombatPOC;

public class CombatPOC : Core
{
    internal const float MapScreenFraction = 0.7f;
    private const int MapTileSize = 20;
    internal const int MapColumns = 16;
    internal const int MapRows = 9;

    private CombatManager _combatManager = new();
    private GameUIManager _UIManager;
    public CombatPOC() : base("CombatPOC", 1280, 720, false)
    {
        Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
        Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        Graphics.ApplyChanges();
        Helper.Scale = GetMapScale(Window.ClientBounds.Width, Window.ClientBounds.Height);
        _combatManager.map?.SetScale(Helper.Scale);
        foreach (ScreenElement element in _combatManager._currentState.ScreenElements)
        {
            element.Sprite.Scale = Helper.Scale;
            if (element is PhantomElement phantomElement)
            {
                phantomElement.ResetScreenPosition();
            }
            if (element is CombatElement combatElement)
            {
                combatElement.ResetScreenPosition();
            }
        } 
    }
    private static Vector2 GetMapScale(int screenWidth, int screenHeight)
    {
        return new(
            screenWidth * MapScreenFraction / (MapTileSize * MapColumns),
            screenHeight * MapScreenFraction / (MapTileSize * MapRows));
    }

    protected override void Initialize()
    {
        // Init Base Class
        base.Initialize();
        _UIManager = new(this);
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
    }
    protected override void LoadContent()
    {
        // Init combatManger
        _combatManager.Initialize(Graphics.GraphicsDevice);
        _combatManager.LoadContent();
    }
    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        base.Update(gameTime);
        _combatManager.Update(gameTime);
        _UIManager.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // Begin the sprite batch to prepare for rendering.
        SpriteBatch.Begin(SpriteSortMode.FrontToBack);
        
        _combatManager.Draw(SpriteBatch);
        // Always end the sprite batch when finished.
        SpriteBatch.End();
        
        _UIManager.Draw();
    }   
    void Window_ClientSizeChanged(object sender, EventArgs e)
    {
        // Update backbuffer or layout here
        Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
        Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        Graphics.ApplyChanges();
        Helper.Scale = GetMapScale(Window.ClientBounds.Width, Window.ClientBounds.Height);
        _combatManager.map?.SetScale(Helper.Scale);
        foreach (ScreenElement element in _combatManager._currentState.ScreenElements)
        {
            element.Sprite.Scale = Helper.Scale;
            if (element is BaseCombatant combatant)
            {
                combatant.Phantom.Sprite.Scale = Helper.Scale;
                combatant.Phantom.ResetScreenPosition();
                combatant.ResetScreenPosition();
            }
            else if (element is CombatElement combatElement)
            {
                combatElement.ResetScreenPosition();
            }
        }
    }
}
