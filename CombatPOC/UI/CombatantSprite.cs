using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using CombatPOC.Managers;
using static POCLibrary.Core;
using CombatPOC.Interfaces;
using POCLibrary.Input;
using CombatPOC.Enum;
using CombatPOC.Entities;
using System.Diagnostics;
using CombatPOC.Logic;

namespace CombatPOC.UI;

public class CombatantSprite: IRenderable, IHoverable, ISelectable, IComponent
{
    public int EntityID {get; }
    public int ComponentID {get; } = EntityManager.CreateNewComponentID();
    public Vector2 RenderablePosition {get; set; }
    public string RenderableName {get; }
    public AnimatedSprite _animatedSprite;
    public Rectangle _spriteRectangle {get => new((int)RenderablePosition.X, (int)RenderablePosition.Y, Helper.TileWidth, Helper.TileHeight ); }
    public Color _selectedColor = Color.Green;
    public bool Hovered {get; set; } = false;
    public CombatantSprite(int entityID, string spritename, Vector2 spritePosition)
    {
        EntityID = entityID;
        RenderableName = spritename;
        RenderablePosition = spritePosition;
    }
    // Methods
    public event OnSelectEventHandler<SelectEventArgs> OnSelect;
    public void Select()
    {
        if (_spriteRectangle.Contains(Input.Mouse.Position) && Input.Mouse.WasButtonJustPressed(MouseButton.Left))
        {
            IEntity entity = CombatManager._EntityManager.GetEntity(EntityID);
            SelectEventArgs args = new(entity);
            OnSelect?.Invoke(this, args);
        }
        
    }
    public void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        _animatedSprite = atlas.CreateAnimatedSprite(RenderableName);
        _animatedSprite.Scale = Helper.Scale;
    }
    public void Update(GameTime gameTime)
    {
        _animatedSprite.Update(gameTime);
        OnHover();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _animatedSprite.Draw(spriteBatch, RenderablePosition);
    }
    public void OnHover()
    {
        if(_spriteRectangle.Contains(Input.Mouse.CurrentState.Position))
        {
            Hovered = true;
            _animatedSprite.Color = _selectedColor;
        }
        else
        {
            Hovered = false;
            _animatedSprite.Color = Color.White;
        }
    }
    public void ClearHover()
    {
        Hovered = false;
        _animatedSprite.Color = Color.White;
    }
    public void UpdatePosition(TileLocation newPosition)
    {
        RenderablePosition = newPosition.ToScreenPosition();
    }
}