using System;
using UnityEngine;
    public class ItemContainer : BaseInteractable {
    Inventory Inventory = new Inventory();

    public override void OnStart() {
        //#Sample: a chest containing a Lockpick; onClick, an Interaction is triggered that move the lockpick to player-Inventory
        InventoryItem Item = new LockPick();
        Inventory.AddItem(Item, 1);

        rRemoveItem a = ScriptableObject.CreateInstance<rRemoveItem>();
        rAddItem b = ScriptableObject.CreateInstance<rAddItem>();
        a.Inventory = Inventory;
        a.Item = b.Item = Item;
        b.Inventory = DataController.instance.game.CurrentPlayer.GetInventory();
        ReactionCollection.reactions= new Reaction[] { a, b };
    }

    
}
