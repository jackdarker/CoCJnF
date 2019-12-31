using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base-class for all creatures
/// </summary>
public class BaseActor : MonoBehaviour {
    public BaseActor() {

        this.m_Stats.SetValue(StatTypes.HP, 20, false);
    }
    public bool IsControlledByPlayer() {
        return false;
    }
    public readonly Stats m_Stats = new Stats();

    public void ConsumeItem(InventoryItem item) {
        //m_ConsumeInv.RemoveItem(item, -1);//move the item to Consume-storage, replacing older items
        //m_ConsumeInv.AddItem(item);
    }
    public void AddItem(InventoryItem item) {
        //Todo
    }
    protected Inventory m_Inventory = new Inventory();
    protected Inventory m_ConsumInv = new Inventory();  //consumables will be moved from Inventory to ConsumInv on consumption because we need to track them but they should not appear in Inventory anymore
}

