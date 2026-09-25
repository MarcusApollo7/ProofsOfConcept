using System;
using System.Collections.Generic;

namespace CombatPOC.Item;

public interface IInventoryItem
{
    string Name {get; set; }
    string Description {get; set; }
    decimal MoneyValue {get; set; }
    float Weight {get; set; }
    float Condition {get; set; }
    List<string> Mods {get; set; }
    EquipableLocation EquipableLocation {get; set;}
}

public class ItemBase: IInventoryItem
{
    public string Name {get; set; }
    public string Description {get; set; }
    public decimal MoneyValue {get; set; }
    public float Weight {get; set; }
    public float Condition {get; set; }
    public List<string> Mods {get; set; }
    public EquipableLocation EquipableLocation {get; set;}
    public ItemBase()
    {
        Weight = 1;
    }
}
