using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo OrMilestone  a collection of milestone where at least one of them has to be fullfilled
//Todo AndMilestone a collection of milestone whose all have to be fullfilled

public class QuestMilestone 
{
    public Action EnterMilestone;
    public Action ExitMilestone;

    public QuestMilestone(int ID, string Name) {
        m_UId = ID;
        m_Name = Name;
        m_Hidden = (ID <= 0); // Automatical hide entry
    }
    public void SetHidden(bool hidden) {
        m_Hidden = hidden;
    }
    public bool GetHidden() {
        return m_Hidden;
    }
    public string GetName() {
		return m_Name;
	}
	public void SetDescription(string value){
		m_Description = value;
	}
	public string GetDescription(){
		return m_Description;
	}
	public void AddCondition(ICondition Cond, int NextID ) {
		m_Cond.Add(Cond,NextID);
	}
    //return value: 0 = condition nok, -1 = condition ok- quest finished, >0 condition ok & next MileID 
	public int EvaluateCondition() {
		int Next= 0;
        Dictionary<ICondition, int>.Enumerator iter = m_Cond.GetEnumerator();
        while(iter.MoveNext()) {
            if (iter.Current.Key.Evaluate())
                Next = iter.Current.Value;
        }
		return Next;
	}
	public int GetUId(){
		return m_UId;
	}
	private string m_Description;
	private string m_Name = "";
	private int m_UId = 0;
    private bool m_Hidden=false;
	private Dictionary<ICondition,int> m_Cond = new Dictionary<ICondition, int>();

}
