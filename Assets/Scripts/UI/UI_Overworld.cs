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
    public Button Bt_System;

    public Clock Clock;
    public UI_Questlog QuestLog;


    // Start is called before the first frame update
    void Start()
    {
        Bt_QuestLog.onClick.AddListener(delegate { showQuestLog(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void showQuestLog() {
        QuestLog.Display();
    }
}
