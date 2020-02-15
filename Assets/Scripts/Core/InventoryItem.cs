using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Baseclass for anything that can be stored in Inventory
/// </summary>
public class InventoryItem 
{
	public InventoryItem(string UID) 
	{
        m_UId = UID;
	}
	public InventoryItem Clone() {
        InventoryItem New = new InventoryItem(this.GetUId());   //Todo thats not working - how to call specific constructor
		New.Copy(this);
		return New;
	}
	protected void Copy(InventoryItem source) {
		this.m_ConsumeEffectActive = source.m_ConsumeEffectActive;
		this.m_Count = 1;
		this.m_EquippLoc = 0;
		this.m_Name = source.m_Name;
		this.m_EffectDuration = source.m_EffectDuration;
	}
	public void SetName(string Name) {
		m_Name = Name;
	}
	public string GetName() {
		return m_Name;
	}
	public int GetWeight() {
		return 100;
	}
	public void SetUId(string value) {
		m_UId = value;
	}
	public string GetUId() {
		return m_UId;
	}
	private string m_UId = "";
	public int GetPrice() {
		return -1;//	-1 => cannot be selled
	}
	public void SetDescription(string value) {
		m_Description = value;
	}
	public string GetDescription() {
		return m_Description;
	}
	private string m_Description;
		
	public virtual bool CanBeConsumed(BaseActor Target) {
		return false;
	}
	protected void OnConsumeChanged() {
	}
	public void Consume(BaseActor Target) {
		InventoryItem item= this.Clone();
		SetCount(GetCount() - 1);
		item.SetOwner(Target);
		item.m_ConsumeEffectActive = true;
		Target.ConsumeItem(item);
		item.OnConsumeChanged();
	}
    /// <summary>
    /// QuestItems cannot be sold or dropped by player, only by the system.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsQuestItem() {
        return false;
    }
    public bool IsConsumeEffectActive() 
	{
		return m_ConsumeEffectActive;
	}
	public virtual bool CanBeUsedInFight() {
		return false;
	}
	public virtual bool CanBeEquipped(BaseActor Target, int EquippLoc) {
		return false;
	}
	public void SetOwner(BaseActor Target) {
		if (!IsOwnedBy(Target)) {
			m_Target = Target;
		}
	}
    public BaseActor GetOwner() {
       return m_Target ;
    }
    private bool IsOwnedBy(BaseActor Target) {
		return (m_Target == Target);    //Todo just compare name ?
	}
	protected void OnEquippChanged() {
	}
	public void Equippe(BaseActor Target, int EquippLoc) {

		if (!IsOwnedBy(Target)) {
			InventoryItem NewItem= this.Clone();
			this.SetCount(GetCount() - 1);
			NewItem.SetOwner(Target);
			Target.AddItem(NewItem);
			NewItem.Equippe(Target, EquippLoc);
		} else {
			m_EquippLoc = EquippLoc;
			OnEquippChanged();
		}
	}
	public int IsEquipped() {
		return m_EquippLoc;
	}
	public void Tick(int Time){
		//remove consumed items from ConsumeInv??
	}
	public virtual bool CanBeCombined(InventoryItem item) {
		return false;
	}
	public InventoryItem Combine(InventoryItem item) {
		return item.Clone();	//Todo crafting in separate Craft-class
	}
		
	public void SetCount(int value) {
		m_Count = value;    //Todo = -> remove Item ?
	}
	public int GetCount(){
		return m_Count;
	}
	public string toString() {
		return m_Name;
	}
	protected int m_EquippLoc = 0;
	private int m_Count = 1;
	private BaseActor m_Target = null;
	public string m_Name= "";
	public bool m_ConsumeEffectActive= false;
	public int m_EffectDuration = 0;
		
}