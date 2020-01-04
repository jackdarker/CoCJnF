using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers when PC meets an NPC. ( Only when visit starts )
/// </summary>
public class nVisitsNPC :  ICondition {
    private static string Name = "nVisitsNPC";
	public nVisitsNPC(int NPCId, bool bContinuously) {
        m_NPCId = NPCId;
        bContinuously = false;
    }
    public string GetName(){
			return Name;
		}
    public bool Evaluate(){
        bool ret = false;
       //Todo  
        return ret;
    }
	private int m_NPCId;
    private bool m_bContinuously = false;
}
