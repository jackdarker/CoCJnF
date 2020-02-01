using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBandit : DialogSceneData {

    public override int GetUId() {
        return (int)QuestGlobals.NpcEnum.BanditChief;   //Todo scene-ID?
    }

    public override void Setup() {
    }

    public override DialogTree GetDialog() {
        DialogTree.Say _Say = new DialogTree.Say();
        DialogTree.Choice _Choice = new DialogTree.Choice();
        int _choice = dlg.GetDialogResult();
        if (_choice > 0) m_State = _choice;
        if (m_State ==0 ) {
            QuestGlobals.getSingleton();    //Todo rebuild quest on Start/load
            int _GoogleQuestMile = QuestManager.getSingleton().GetQuestById((int)QuestGlobals.QuestEnum.QstWiseManGoogles).GetCurrMile().GetUId();
            if (_GoogleQuestMile >= (int)QstWiseManGoogles.MileEnum.HuntGoogleQuest1 && _GoogleQuestMile < (int)QstWiseManGoogles.MileEnum.HuntGoogleQuest2 )
                m_State = 10;
        }
        dlg.CreateDialogSetup();
        _Say.m_Who = "Bandits";
        switch(m_State) {
            case 1:
                _Say.m_What = "Better dont hassle with them.";
                dlg.SetDone();
                break;
            case 10:
                _Say.m_What = "You still have to get those googles. How are you approaching this situation?";
                _Choice.m_Choice = new int[] { 20, 30, 40 };
                _Choice.m_Text = new string[] { "Ask them.", "Sneak and steal", "Attack them" };
                dlg.AddElement(_Choice);
                dlg.AddElement(_Say);
                break;
            case 20:
                _Say.m_What = "Hello sir can you please hand over the googles.";
                dlg.AddElement(_Say);
                m_State = 21;
                break;
            case 21:
                _Say.m_What = "Piss off";
                dlg.AddElement(_Say);
                m_State = 22;
                break;
            case 22:
                dlg.SetDone();
                break;
            case 30:
                _Say.m_What = "You sneak around their backside and see the googles right there in a basket. You grab them.";
                QuestManager.getSingleton().GetQuestById((int)QuestGlobals.QuestEnum.QstWiseManGoogles).ActivateMileByID(
                    (int)QstWiseManGoogles.MileEnum.HuntGoogleQuest2);
                m_State = 31;
                break;
            case 31:
                dlg.SetDone();
                break;
            case 40:
                _Say.m_What = "You are not fit enough for this.";
                dlg.SetDone();
                break;
            default:
                _Say.m_What = "Some suspicious looking persons are lurking other there?";
                dlg.AddElement(_Say);
                m_State = 1;
                break;
        } 
        return dlg;
    }
    private int m_State = 0;
    private DialogTree dlg = new DialogTree();
}
