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

    // Start is called before the first frame update
    void Start()
    {
        QuestManager.getSingleton().QuestUpdated += UI_Overworld_QuestUpdated;
        Bt_QuestLog.onClick.AddListener(delegate { showQuestLog(); });
        Bt_SaveLoad.onClick.AddListener(delegate { showSaveLoadPanel(); });
        Bt_Settings.onClick.AddListener(delegate { showSettings(); });
    }

    private void UI_Overworld_QuestUpdated() {
        Bt_QuestLog.GetComponentInChildren<Image>(true).sprite = Resources.Load<Sprite>(ResourcesManager.IconsPaths + "UI_Icon_Aim.png"); 
    }

    protected void showQuestLog() {
        QuestLog.Display();
        Bt_QuestLog.GetComponentInChildren<Image>(true).sprite = Resources.Load<Sprite>(ResourcesManager.IconsPaths + "UI_Icon_Menu.png");
    }
    protected void showSaveLoadPanel() {
        SaveLoadPanel.Display();
    }
    protected void showSettings() {
        Settings.Display();
    }
}
