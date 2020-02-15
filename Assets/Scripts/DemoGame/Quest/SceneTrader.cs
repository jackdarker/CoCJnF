using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrader : DialogSceneData {

    public UI_TradePanel TradePanel;

    public override int GetUId() {
        return (int)QuestGlobals.NpcEnum.Trader;   //Todo scene-ID?
    }

    public override void Setup() {
        m_State = 0;
    }

    public override DialogTree GetDialog() {
        DialogTree.Say _Say = new DialogTree.Say();
        DialogTree.Choice _Choice = new DialogTree.Choice();
        int _choice = dlg.GetDialogResult();
        if (_choice > 0) m_State = _choice;

        dlg.CreateDialogSetup();
        _Say.m_Who = "Trader";
        switch(m_State) {
            case 0:
                _Say.m_What = "Hello my friend. Do you want to trade?";
                _Choice.m_Choice = new int[] { 20, 10 };
                _Choice.m_Text = new string[] { "Yes", "No" };
                dlg.AddElement(_Say);
                dlg.AddElement(_Choice);
                break;
            case 10:
                _Say.m_What = "Good by then.";
                dlg.AddElement(_Say);
                m_State = 11;
                break;
            case 11:
                dlg.SetDone();
                break;
            case 20:
                _Say.m_What = "Check out my wonderful wares...";
                m_State = 30;
                dlg.AddElement(_Say);
                break;
            case 30:
                _Say.m_What = "[TradePanel]";
                dlg.AddElement(_Say);
                dlg.SetDone();
                ShowTradePanel();
                break;
            default:
                _Say.m_What = "Some suspicious looking persons are lurking other there?";
                dlg.AddElement(_Say);
                m_State = 0;
                break;
        } 
        return dlg;
    }
    private void ShowTradePanel() {
        TradePanel.m_AActor = DataController.instance.game.CurrentPlayer;
        TradePanel.m_BInv = new Inventory();
        TradePanel.m_BInv.AddItem(new LockPick(), 5);
        TradePanel.Display();
    }
    private int m_State = 0;
    private DialogTree dlg = new DialogTree();
}
