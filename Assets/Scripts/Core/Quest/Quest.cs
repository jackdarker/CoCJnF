﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    public Quest(int ID, string Name) {
        m_UId = ID;
        m_Name = Name;
        m_Mile = null;
    }

    public string GetName(){
			return m_Name;
		}
	public void SetDescription(string value){
		m_Description = value;
	}
	public string GetDescription(){
		return m_Description;
	}
    public void SetHidden(bool hidden) {
        m_Hidden = hidden;
    }
    public bool GetHidden() {
        return m_Hidden;
    }
    public int GetUId() {
        return m_UId;
    }
	public QuestMilestone GetCurrMile() {
		return m_Mile;
	}
    //returns the Quest & Milestone dexription for Quest-Log
	public string GetLogDescription(){
        if (GetHidden()) return string.Empty;
		string log= GetDescription();
        log += "\n";
		if(GetCurrMile()!=null && !GetCurrMile().GetHidden()) log+= GetCurrMile().GetDescription();
		return log;
	}
	public void ActivateMileByID(int ID){
        QuestMilestone mile = GetMileByID(ID);
        if (mile != null) {
            if (m_Mile != null && m_Mile.ExitMilestone!=null) {
                m_Mile.ExitMilestone();
            }
            m_Mile = mile;
            if (m_Mile != null && m_Mile.EnterMilestone != null) {
                m_Mile.EnterMilestone();
            }
        }
	}
	public void AddMileStone(QuestMilestone Mile) {
        if (Mile != null) {
            m_MileStones.Add(Mile.GetUId(), Mile);
        }
        if (m_Mile == null) {
            m_Mile = Mile; //automatical activate entry milestone
            m_Hidden = Mile.GetHidden();
        }

    }
    // check here if progress to next milestone can be made (Condition of current MS met)
    // if there is no curent MS set, this quest is not started; 
    // the condition of the MS is the EXIT-condition
    public void EvaluateCondition(){

        if (m_Finished ) {
            return;
        }
        int Next = -1;
		if(m_Mile != null) {
			Next = m_Mile.EvaluateCondition();
		} 
		if (Next == -1) {
			m_Finished = true;
		}else if (Next > 0) {
			ActivateMileByID(Next);
            m_Hidden = false;
		}
	}
	public QuestMilestone GetMileByID(int ID) {
        if (m_MileStones.ContainsKey(ID))
            return m_MileStones[ID];

		return null;
	}
	private string m_Description= "";
	private string m_Name= "";
	private int m_UId = 0;
	private QuestMilestone m_Mile;
	private Dictionary<int, QuestMilestone> m_MileStones = new Dictionary<int, QuestMilestone>();
	public bool m_Finished = false;
	public bool m_Hidden = false;
}
