using CombatPOC.Interfaces;
using CombatPOC.Enum;
using CombatPOC.UI;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;
using CombatPOC.Managers;
using System.Collections.Generic;
using System;
using CombatPOC.Classes;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.stats_skills;
using System.Diagnostics;

namespace CombatPOC.Entities;

public class Combatant: IEntity, IActor, IRenderable, IMoveable, ISelectable, IAttacker, IDefender
{
    public int EntityID {get; }
    public TeamEnum Team;
    public List<IComponent> Components {get;} = [];
    private CombatantStats Actor {get => ((IEntity)this).GetComponent<CombatantStats>();}
    private CombatantSprite Sprite {get => ((IEntity)this).GetComponent<CombatantSprite>();}
    private CombatantMoveable Moveable {get => ((IEntity)this).GetComponent<CombatantMoveable>();}
    public IActorRoutine ActorRoutine {get => ((IEntity)this).GetComponent<IActorRoutine>();}
    public TileLocation TileLocation {get => Moveable.TileLocation; set => Moveable.TileLocation = value; }
    public Stat[] AttackStats {get => Actor.AttackStats;}
    public Stat[] DefenseStats {get => Actor.DefenseStats;}
    public Stat MaxHealth {get;}
    public float CurHealth {get; set; }
    public bool IsDowned {get => CurHealth > 0;}
    private CharacterDirection actorDirection;
    public CharacterDirection ActorDirection {get => actorDirection; set=> actorDirection = value; }
    public IWeapon RightItem {get; set; }
    public int TilesPerMove {get => Moveable.TilesPerMove; set => Moveable.TilesPerMove = value; }
    private float _attackRating;
    public float AttackRating {get => _attackRating; set => _attackRating = value;}
    private float _defenseRating;
    public float DefenseRating {get => _defenseRating; set => _defenseRating = value; }
    public Combatant(float str, float dex, float eva, float tgh, float health, TileLocation tileLocation, TeamEnum team, string spritename)
    {        
        EntityID = EntityManager.CreateNewEntityID();
        CombatManager._EntityManager.AddEntity(this);
        Team = team;
        CombatantStats stats = new(EntityID, str, dex, eva, tgh);
        MaxHealth = new(health);
        CurHealth = health;
        Components.Add(stats);
        CombatantSprite sprite = new(EntityID, spritename, tileLocation.ToScreenPosition());
        sprite.LoadContent();
        Components.Add(sprite);
        CombatantMoveable moveable = new(EntityID, tileLocation); 
        Components.Add(moveable);
        if (Team == TeamEnum.Player)
        {
            Components.Add(new PlayerActor(EntityID));
        }
        else
            Components.Add(new BasicEnemyActor(EntityID));
        CombatManager._selectionManger.AddSelectable(this);
        CombatManager._EntityManager.AddEntity(this);
        sprite.OnSelect += CombatManager._selectionManger.DoOnSelect;
        moveable.OnSetPath += CombatManager._UIManager._animationManager.AddToMoveQueue;
        OnAttack += CombatManager._UIManager._animationManager.AddToAttackQueue;
    }
    // basic methods
    public List<Act> TakeTurn(BattleState state)
    {
        return ActorRoutine.TakeTurn(this, state.GetOpposingSide(Team));
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch);
    }
    public void Move(TileLocation newTileLocation)
    {
        if (newTileLocation != null)
        {
            Moveable.TileLocation = newTileLocation;
            Sprite.RenderablePosition = newTileLocation.ToScreenPosition();
        }
    }
    public void LoadContent()
    {
        Sprite.LoadContent();
    }
    public event OnAttackEventHandler<AttackEventArgs> OnAttack;
    public void DoAttack(Act attack)
    {
        IEntity entity = CombatManager._EntityManager.GetEntity(EntityID);
        OnAttack?.Invoke(this, new() { Entity = entity, Attack = attack});
    }
    public void Update(GameTime gameTime)
    {
        Sprite.Update(gameTime);
    }
    public void MovePath(Act path)
    {
        Moveable.MovePath(path);
    }
    public void Select()
    {
        Sprite.Select();
    }
    public void UpdatePosition(TileLocation newLocation)
    {
        Sprite.RenderablePosition = newLocation.ToScreenPosition();
    }
    public void ChangeHealth(float dmg)
    {
        Debug.WriteLine($"Oh No! {Sprite.RenderableName} has been hit for {Math.Abs(dmg)}!");
        CurHealth += dmg;
    }
}

