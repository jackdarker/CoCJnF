using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QstWiseManGoogles : MonoBehaviour {
    public enum MileEnum {
        Start = 0,
        FindHim = 10,
        BeginGoogleQuest = 20,
        HuntGoogleQuest1 = 30,
        HuntGoogleQuest2 = 40,
        EndGoogleQuest = 50
    }

    public Quest Setup() {

        Quest myQuest = new Quest((int)QuestGlobals.QuestEnum.QstWiseManGoogles, "Wise Man's Googles");
        myQuest.SetDescription("The old man needs his googles for reading");

        QuestMilestone _mile = new QuestMilestone((int)MileEnum.Start, "On visit");
        ICondition _Cond = new nVisitsNPC((int)QuestGlobals.NpcEnum.WiseMan, false);
        _mile.AddCondition(_Cond, (int)MileEnum.BeginGoogleQuest);
        myQuest.AddMileStone(_mile);

        _mile = new QuestMilestone((int)MileEnum.FindHim, "Find the wise man");
        _Cond = new nVisitsNPC((int)QuestGlobals.NpcEnum.WiseMan, false);
        _mile.AddCondition(_Cond, (int)MileEnum.BeginGoogleQuest);
        _mile.SetDescription("You need to find a wise man to get more info");
        myQuest.AddMileStone(_mile);

        _mile = new QuestMilestone((int)MileEnum.BeginGoogleQuest, "He might have a quest for you");
        //NoCondition ?
        _mile.SetDescription("Take the quest to find the wise mans googles.");
        myQuest.AddMileStone(_mile);

        _mile = new QuestMilestone((int)MileEnum.HuntGoogleQuest1, "Get his googles back");
        _Cond = new nVisitsNPC((int)QuestGlobals.NpcEnum.BanditChief, false);
        _mile.AddCondition(_Cond, (int)MileEnum.HuntGoogleQuest2);
        _mile.SetDescription("Talk to the grunts or just kill them all and search for the googles");
        myQuest.AddMileStone(_mile);

        _mile = new QuestMilestone((int)MileEnum.HuntGoogleQuest2, "You have the goggles, now bring them back.");
        _Cond = new nVisitsNPC((int)QuestGlobals.NpcEnum.WiseMan, false);
        _mile.AddCondition(_Cond, (int)MileEnum.EndGoogleQuest);
        _mile.SetDescription("You have the goggles, now bring them back.");
        //_mile.EnterMilestone = new Action(delegate { //add google quest item});
        myQuest.AddMileStone(_mile);

        _mile = new QuestMilestone((int)MileEnum.EndGoogleQuest, "You finished the task");
        //no Cond ??
        _mile.SetDescription("The old guy can read again");
        myQuest.AddMileStone(_mile);

            return myQuest;
        }
}
