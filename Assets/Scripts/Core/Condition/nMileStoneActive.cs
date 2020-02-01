using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers when a certain milestone is active
/// </summary>
public class nMileStoneActive : ICondition {
    private static string Name = "nMileStoneActive";
	public nMileStoneActive(int quest, int mile) {
        m_QuestId = quest;
        m_MileId = mile;
    }
    public string GetName(){
			return Name;
	}
    public string GetText() {
        return Name;    //Todo
    }
    public bool Evaluate(){
        bool ret = false;
        Quest quest = QuestManager.getSingleton().GetQuestById(m_QuestId);
        if (quest != null) {
            QuestMilestone mile = quest.GetCurrMile();
            if (mile != null) {
                ret = (mile.GetUId()== m_MileId);
            }
        }
        return ret;
    }
		private int m_QuestId;
		private int m_MileId;
		
}
