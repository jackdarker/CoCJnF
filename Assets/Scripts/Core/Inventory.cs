using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// storage for Items (Player - Inventory, Treasure-Chests,...)
/// </summary>
public class Inventory : MonoBehaviour {
	public Inventory() 		{		}
	public void SetSlotCount(int value) {
		m_SlotCount = value;
	}
	public int GetSlotsMax() {
		return m_SlotCount;
	}
	public int GetSlotsUsed() {
		Cleanup();
		int Count = 0;
		for (int i = 0; i < m_Slots.Count; i++) 
		{
			if (m_Slots[i] != null) {
				Count++;
			}
		}
		return Count;
	}
	public InventoryItem GetItem(int Slot) {
		return m_Slots[Slot] as InventoryItem;
	}
	//-1 removes all
	public void RemoveItem(InventoryItem item,int Count = 1) {
		int index = FindItem(item.GetUId(),item.IsEquipped()>0);
		if (index < 0) return;
		if (GetItem(index) != null) {
			int i = (Count < 0) ? GetItem(index).GetCount() : Math.Max(0, (GetItem(index).GetCount() - Count));
			GetItem(index).SetCount(i);
		}
		Cleanup();
	}
	public bool AddItem(InventoryItem NewItem, int Count = 1) {
		int index = FindNextFreeSlot(NewItem);
		if (index < 0) return false;
			
		if (GetItem(index) != null) {
			return ReplaceItem(NewItem , index, GetItem(index).GetCount() + Count);
		} else {
			return ReplaceItem(NewItem , index,  Count);
		}			
	}
	public bool IsOverloaded() {
		return GetSlotsUsed() > GetSlotsMax();  //Todo use Item wheight instead slot count
	}
	private bool ReplaceItem(InventoryItem NewItem, int Slot, int Count= 1) {
		NewItem.SetCount(Count);
		m_Slots[Slot] = NewItem;
		Cleanup();
		return true;
	}
	public int FindItem(string ItemId, bool Equipped) {
		int Index= -1;
		InventoryItem item = null;
		// find slot with similiar item
		for (int i = 0; i < m_Slots.Count; i++) 
		{
			if (m_Slots[i] != null) {
				item = m_Slots[i];
				if (item.GetUId() == ItemId && 
					(((item.IsEquipped()>0)&& Equipped) || (!(item.IsEquipped()>0)&& !Equipped))) {
					Index = i;
					break;
				}
			}
		}
		return Index;
	}
	public int FindNextFreeSlot(InventoryItem item) {
		Cleanup();
		int Index = -1;
		Index = FindItem(item.GetUId(), item.IsEquipped() > 0);
			
		if (Index >= 0) {
			if ((GetItem(Index).GetCount() + 1) * GetItem(Index).GetWeight() > 100)
				Index = -1;
			else
				return Index;
		}
		//...or find free slot
		for (int i = 0; i < m_Slots.Count; i++) 
		{
			if (m_Slots[i] == null) {
				Index = i;
				break;
			}
		}
		if (Index == -1 ) Index = m_Slots.Count;
		return Index;
	}
	public void Cleanup() {
		InventoryItem item = null;
		int i;
		for ( i= 0; i < m_Slots.Count; i++) 
		{	//remove elements where count=0;
			if (m_Slots[i] != null) {
				item = m_Slots[i];
				if (item.GetCount() <= 0) m_Slots.RemoveAt(i);
			}
		}
		m_Slots.Sort(sortOnName);
	}

	private int m_SlotCount = 1;
	private List<InventoryItem> m_Slots = new List<InventoryItem>();
    private static SortOnName sortOnName = new SortOnName();

    //sorter for Inventory
    private class SortOnName : IComparer<InventoryItem> {
        int IComparer<InventoryItem>.Compare(InventoryItem x, InventoryItem y) {
            return x.GetName().CompareTo(y.GetName());
        }
    }
}
