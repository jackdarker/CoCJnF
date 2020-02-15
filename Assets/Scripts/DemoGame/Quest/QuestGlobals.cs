using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGlobals 
{
    private static QuestGlobals singleton;
    public static QuestGlobals getSingleton() {
        if (singleton == null)
            singleton = new QuestGlobals();
        return singleton;
    }
    private QuestGlobals() {
        Setup();
    }
    public void Setup() {
        QuestManager.getSingleton().AddQuest(new QstWiseManGoogles().Setup());
    }
    public enum QuestEnum {
        QstWiseManGoogles = 10,
        QstFirstKeystone = 20
    }

    public enum NpcEnum {
         WiseMan = 1,
         BanditChief = 2,
         Trader = 3
    }
}
