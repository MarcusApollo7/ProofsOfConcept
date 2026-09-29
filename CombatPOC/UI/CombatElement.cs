using System;
using System.Diagnostics;
using CombatPOC.Logic;
using CombatPOC.Managers;
using CombatPOC.stats_skills;
using Microsoft.Xna.Framework;
using POCLibrary.Input;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public interface ICombatElement
{
    Stat MaxHealth {get; }
    float CurHealth {get; set; }
    float DefenseRating {get; }
    bool IsDowned {get;}
    TileLocation TileLocation {get; set; }
    void ChangeHealth(float dmg);
}
public delegate void OnSelectEventHandler<SelectEventArgs>(CombatElement sender, SelectEventArgs e);
public class SelectEventArgs(string message) : EventArgs
{
    public string Message {get; init; } = message;
}
public abstract class CombatElement: ScreenElement, ICombatElement
{
    public Stat MaxHealth {get; }
    public float CurHealth {get; set; }
    public abstract float DefenseRating {get; }
    public bool IsDowned {get  => CurHealth > 0;}
    public TileLocation TileLocation {get; set; }
    public CombatElement(string name, string atlasName, float health, TileLocation tileLocation): base(tileLocation.ToScreenPosition(), name, atlasName)
    {
        MaxHealth = new(health);
        CurHealth = health;
        TileLocation = tileLocation;
        OnSelect += CombatManager._inputHandler.SubscribeToSelect;
    }
    public void ChangeHealth(float dmg)
    {
        Debug.WriteLine($"Oh No! Element has been hit for {dmg}");
        CurHealth += dmg;
    }
    public void MoveToNewLocation(TileLocation newTileLocation)
    {
        if (newTileLocation != null)
        {
            TileLocation = newTileLocation;
            ScreenPosition = newTileLocation.ToScreenPosition();
        }
    }
    public event OnSelectEventHandler<SelectEventArgs> OnSelect;
    public void Select()
    {
        if (SpriteRectangle.Contains(Input.Mouse.Position) && Input.Mouse.WasButtonJustPressed(MouseButton.Left))
        {
            SelectEventArgs args = new("Hell0");
            OnSelect?.Invoke(this, args);
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Select();
    }
}

