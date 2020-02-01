using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// connects the UI Prefabs to the game
/// </summary>
public class UI_Overworld : MonoBehaviour
{
    public GameObject Bt_Holder;
    public Button Bt_QuestLog;
    public Button Bt_Inventory;
    public Button Bt_SaveLoad;
    public Button Bt_Settings;

    public Clock Clock;
    public UI_Questlog QuestLog;
    public UI_SaveLoadPanel SaveLoadPanel;
    public UI_Settings Settings;
    public UI_TradePanel TradePanel;

    // Start is called before the first frame update
    void Start()
    {
        QuestManager.getSingleton().QuestUpdated += UI_Overworld_QuestUpdated;
        Bt_QuestLog.onClick.AddListener(delegate { showQuestLog(); });
        Bt_SaveLoad.onClick.AddListener(delegate { showSaveLoadPanel(); });
        Bt_Settings.onClick.AddListener(delegate { showSettings(); });
    }

    private void UI_Overworld_QuestUpdated() {
        Bt_QuestLog.GetComponentInChildren<ButtonWithIcon>(true).SetIcon(
            Resources.Load<Sprite>(ResourcesManager.IconsPaths + "UI_Icon_Aim")); 
    }

    protected void showQuestLog() {
        QuestLog.Display();
        Bt_QuestLog.GetComponentInChildren<ButtonWithIcon>(true).SetIcon(
            Resources.Load<Sprite>(ResourcesManager.IconsPaths + "UI_Icon_Menu"));
    }
    protected void showSaveLoadPanel() {
        SaveLoadPanel.Display();
    }
    protected void showSettings() {
        Settings.Display();
    }
    public void showTradePanel(BaseActor A, BaseActor B) {
        TradePanel.m_AActor = A;
        TradePanel.m_BActor = B;
        TradePanel.Display();
    }
}
