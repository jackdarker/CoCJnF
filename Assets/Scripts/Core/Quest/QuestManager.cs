using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager 
{
    private static QuestManager singleton;
    public static QuestManager getSingleton() {
        if (singleton == null)
            singleton = new QuestManager();
        return singleton;
    }
    public event Action QuestUpdated;
    private QuestManager() {

    }
    public int GetQuestCount(){
			return m_Quests.Count;
	}
    public Dictionary<int, Quest>.Enumerator GetIterator() {
        return m_Quests.GetEnumerator();
    }
    public Quest GetQuestById(int Id){
        if (m_Quests.ContainsKey(Id)) {
            return m_Quests[Id];
        }
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    public void Tick() {
        Dictionary<int, Quest>.Enumerator it =GetIterator();
        while (it.MoveNext()) {
            it.Current.Value.EvaluateCondition();
        }
    }
	public void AddQuest(Quest value){
        value.QuestUpdated += QuestUpdated;
        m_Quests.Add(value.GetUId(), value);    //Todo update 
        
	}
    public void RemoveQuest(int Id) {
        if (m_Quests.ContainsKey(Id)) {
            m_Quests.Remove(Id);
        }
	}
	private Dictionary<int,Quest> m_Quests = new Dictionary<int, Quest>();
}
