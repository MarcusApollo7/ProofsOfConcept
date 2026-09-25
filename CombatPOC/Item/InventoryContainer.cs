using System.Collections.Generic;

namespace CombatPOC.Item;

public interface IInventoryContainer
{
    int? MaxVolume {get; set;}
    string Name {get; set;}
    string Description {get; set;}
    void AddItem(IInventoryItem item);
    void RemoveItem(IInventoryItem item);

}

public class InventoryBase: IInventoryContainer
{    
    public string Name { get; set; }
    public string Description { get; set; }
    // public Sprite InvSlotBox { get; set; }
    // public Vector2 Position { get; set; }
    // public Sprite InventoryContainer { get; set; }
    // public int invSize = 8;
    // public int invBox = 32;
    // public float invVPos = 0;
    // int maxLines = 0;
    // protected IInventoryItem mouseOver = null;
    // public IInventoryItem SelectedItem = null;
    // Sprite up, down;
    // Texture2D scrollBarRect;
    // public SpriteFont Font { get; set; }
    // public SpriteFont tinyFont { get; set; }
    public int? MaxVolume { get; set; }
    public List<IInventoryItem> Items { get; set; }
    public float TotalWeight { get; set; }
    // protected Sprite closeButton { get; set; }
    public bool IsShowing { get; set; }
    public InventoryBase()
    {
        Items = [];
    }
    public void AddItem(IInventoryItem item)
    {
        if (MaxVolume == null || Items.Count + 1 < MaxVolume.Value)
            Items.Add(item);
    }
    public void RemoveItem(IInventoryItem item)
    {
        Items.Remove(item);
    }   
}

public class PlayerInventory: InventoryBase
{
    
}