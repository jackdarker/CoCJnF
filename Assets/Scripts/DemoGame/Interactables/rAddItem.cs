

public class rAddItem : Reaction {
    public InventoryItem Item;
    public Inventory Inventory;

    protected override void ImmediateReaction() {
        Inventory.AddItem(Item);
    }
}
public class rRemoveItem : Reaction {
    public InventoryItem Item;
    public Inventory Inventory;

    protected override void ImmediateReaction() {
        Inventory.RemoveItem(Item);
    }
}
