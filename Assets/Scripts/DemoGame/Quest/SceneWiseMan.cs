using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWiseMan : DialogSceneData {

    public override int GetUId() {
        return (int)QuestGlobals.NpcEnum.WiseMan;   //Todo scene-ID?
    }

    public override void Setup() {
    }

    public override DialogTree GetDialog() {
        DialogTree.Say _Say = new DialogTree.Say();
        DialogTree.Choice _Choice = new DialogTree.Choice();
        int _choice = dlg.GetDialogResult();
        if (_choice > 0) m_State = _choice;
        else {
            QuestGlobals.getSingleton();    //Todo rebuild quest on Start/load
            int _GoogleQuestMile = QuestManager.getSingleton().GetQuestById((int)QuestGlobals.QuestEnum.QstWiseManGoogles).GetCurrMile().GetUId();
            if (_GoogleQuestMile >= (int)QstWiseManGoogles.MileEnum.HuntGoogleQuest1 && _GoogleQuestMile < (int)QstWiseManGoogles.MileEnum.HuntGoogleQuest2 )
                m_State = 10;
        }
        dlg.CreateDialogSetup();
        _Say.m_Who = "Wise Man";
        switch(m_State) {
            case 1:
                _Say.m_What = "You ponder your option";
                _Choice.m_Choice = new int[]{ 2,3};
                _Choice.m_Text = new string[] { "Who are you?", "Are you blind?" };
                dlg.AddElement(_Choice);
                dlg.AddElement(_Say);
                break;
            case 10:
                _Say.m_What = "Hello again. Did you find my googles?";
                _Choice.m_Choice = new int[] { 20, 30 };
                _Choice.m_Text = new string[] { "No", "Yes" };
                dlg.AddElement(_Choice);
                dlg.AddElement(_Say);
                break;
            case 20:
                _Say.m_What = "I cannot help you if you dont find them.";
                dlg.SetDone();
                break;
            case 2:
                _Say.m_What = "Iam old and know stuff.";
                m_State = 1;
                dlg.AddElement(_Say);
                break;
            case 3:
                _Say.m_What = "I lost my googles. Please find them.";
                m_State = 1;
                _Choice.m_Choice = new int[] { 4, 5 };
                _Choice.m_Text = new string[] { "Sure", "Nah" };
                dlg.AddElement(_Choice);
                dlg.AddElement(_Say);
                break;
            case 4:
                _Say.m_What = "You have got a quest.";
                m_State = 1;
                dlg.SetDone();
                QuestManager.getSingleton().GetQuestById((int)QuestGlobals.QuestEnum.QstWiseManGoogles).ActivateMileByID(
                    (int)QstWiseManGoogles.MileEnum.HuntGoogleQuest1);
                break;
            case 5:
                _Say.m_What = "Maybe later. Lets talk about other things.";
                m_State = 1;
                break;
            default:
                _Say.m_What = "Hello my friend. How may I help you?";
                dlg.AddElement(_Say);
                m_State = 1;
                break;
        } 
        return dlg;
    }
    private int m_State = 0;
    private DialogTree dlg = new DialogTree();
}
