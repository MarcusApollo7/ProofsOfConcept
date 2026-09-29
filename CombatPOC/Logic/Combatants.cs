using CombatPOC.Interfaces;
using CombatPOC.Enum;
using CombatPOC.Logic;
using System.Collections.Generic;
using System;
using CombatPOC.stats_skills;
using CombatPOC.Item;
using CombatPOC.UI.Animation;
using CombatPOC.UI;
using CombatPOC.Entities;
using CombatPOC.Logic.Paths;
using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using static POCLibrary.Core;

namespace CombatPOC.Logic;

public abstract class BaseCombatant: CombatElement, IAttacker, IAnimatable
{
    /// <summary>
    /// Name of BaseCombatant
    /// </summary>
    public string Name {get; set; }
    /// <summary>
    /// Team of the BaseCombatant
    /// </summary>
    public TeamEnum Team {get; set; }
    private readonly Stat Str;
    private readonly Stat Dex;
    private readonly Stat Eva;
    private readonly Stat Tgh;
    private InventoryBase Inventory {get; } = new();
    public CharacterDirection ActorDirection {get; set; }
    public float AttackRating
    {
        get
        {
            if (Equipped[EquipableLocation.Right_Hand] != null && Equipped[EquipableLocation.Right_Hand] is Weapon weapon)
            {
                float output = 0f;
                foreach(KeyValuePair<string, float> keyValue in weapon.WeaponStats)
                {
                    if (keyValue.Key == Constants.Str)
                    {
                        output += keyValue.Value * Str.Value;
                    }
                    if (keyValue.Key == Constants.Dex)
                    {
                        output += keyValue.Value * Dex.Value;
                    }
                }
                return output;
            }
            return 0;
        }
    }
    public AnimatedSprite AnimatedSprite {get
        {
            if (Sprite is AnimatedSprite animatedSprite)
                return animatedSprite;
            else
                throw new ArgumentException("Combatant Sprite must be Animated");
        } 
    }
    public override float DefenseRating {get; }
    private Dictionary<EquipableLocation, IInventoryItem> Equipped = new ()
    {
        {  EquipableLocation.Head, null },
        {  EquipableLocation.Neck, null },
        {  EquipableLocation.Body, null },
        {  EquipableLocation.Left_Arm, null },
        {  EquipableLocation.Right_Arm, null },
        {  EquipableLocation.Left_Leg, null },
        {  EquipableLocation.Right_Leg, null },
        {  EquipableLocation.Feet, null },
        {  EquipableLocation.TwoHanded, null },
        {  EquipableLocation.Left_Hand, null },
        {  EquipableLocation.Right_Hand, null }
    };
    public int TilesPerMove {get; set; }
    public abstract MoveAction Move {get; }
    internal IPathfinder Pathfinder {get; }
    public BaseCombatant(string SpriteName, string atlasName, float health, float str, float dex, float eva, float tgh, TileLocation tileLocation, IPathfinder pathfinder): base(SpriteName, atlasName, health, tileLocation)
    {
        Str = new(str, Constants.Str);
        Dex = new(dex, Constants.Dex);
        Eva = new(eva);
        Tgh = new(tgh);
        Pathfinder = pathfinder;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        AnimatedSprite.Update(gameTime);
    }
    public MoveAct GetFullMove()
    {
        var result = new List<TileLocation>();
        var visited = new List<TileLocation>();
        var queue = new Queue<(TileLocation pos, int dist)>();

        // Start BFS
        queue.Enqueue((TileLocation, 0));
        visited.Add(TileLocation);
        result.Add(TileLocation);

        // Directions: up, down, left, right
        int[,] directions = { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } };

        while (queue.Count > 0)
        {
            var (current, dist) = queue.Dequeue();
            result.Add(current);

            if (dist > TilesPerMove) continue;

            for (int i = 0; i < 4; i++)
            {
                int nx = current.X + directions[i, 0];
                int ny = current.Y + directions[i, 1];

            }
        }

        return new(Move, this, result, Color.Green);
    }
    public override void OnHover()
    {
        if (SpriteRectangle.Contains(Input.Mouse.Position))
            Sprite.Color = Color.Green;
        else
            Sprite.Color = Color.White;
    }
}

public class PlayerCombatant: BaseCombatant
{
    public List<Attack> Attacks {get; }
    public override MoveAction Move { get; }
    public bool Moved {get; set; }= false;
    public bool Attacked {get; set; }= false;
    public PlayerCombatant(string SpriteName, string atlasName, float health, float str, float dex, float eva, float tgh, TileLocation tileLocation, IPathfinder pathfinder): base(SpriteName, atlasName, health, str, dex, eva, tgh, tileLocation, pathfinder)
    {
        Team = TeamEnum.Player;
        Attacks = [new BasicAttack(), new SwordHeavyAttack()];
        Move = new MoveAction(Constants.PlayerMaxMoves);
    }
}

public abstract class NonPlayerCombatant: BaseCombatant
{
    public IActorRoutine ActorRoutine;
    public NonPlayerCombatant(string SpriteName, string atlasName, float health, float str, float dex, float eva, float tgh, TileLocation tileLocation, IPathfinder pathfinder): base(SpriteName, atlasName, health, str, dex, eva, tgh, tileLocation, pathfinder)
    {
        
    }
    public List<Act> TakeTurn(List<PlayerCombatant> possibleTargets)
    {
        return ActorRoutine.TakeTurn(possibleTargets);
    }
}
public class BasicEnemy: NonPlayerCombatant
{
    public override MoveAction Move { get => ActorRoutine.Move;}
    public BasicEnemy(string SpriteName, string atlasName, float health, float str, float dex, float eva, float tgh, TileLocation tileLocation, IPathfinder pathfinder): base(SpriteName, atlasName, health, str, dex, eva, tgh, tileLocation, pathfinder)
    {
        Team = TeamEnum.Enemy;
        ActorRoutine = new BasicEnemyActor(this, Constants.GruntMaxMoves);
    }
    public void MoveTowardTarget(CombatElement target)
    {
        
    }
}
