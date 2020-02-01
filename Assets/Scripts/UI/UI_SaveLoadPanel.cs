using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class UI_SaveLoadPanel : BasePanel {

    /// <summary>
    /// This is the text that will display when 
    /// </summary>
    private const string EMPTY_SLOT = "New Game";
    private const string USED_SLOT = "Load Save ";
    private const string LOAD = "Load...";
    private const string SAVE = "Save...";

    public Button m_BtMode;
    public ButtonList m_SelButtons;
    public InfoBox m_Info;
    protected int m_Page = 0;
    protected bool m_LoadMode;
    protected SceneController sceneController;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        m_Page = 0;
        m_SelButtons.OnBackClicked += OnCancle;
        m_BtMode.onClick.AddListener( OnMode);
        sceneController = FindObjectOfType<SceneController>();
        SetMode(true);
    }

    protected void OnPressed(ButtonList.ButtonItem bt) {
        int Slot = int.Parse(bt._ID);
        if (m_LoadMode) {
            // Load the save data file
            DataService.Instance.LoadSaveData(Slot);
            // Load the last level the player was in
            sceneController.FadeAndLoadScene(DataService.Instance.SaveDatas.lastLevel);
            //SceneManager.LoadScene(DataService.Instance.SaveDatas.lastLevel);
        } else {
            DataService.Instance.WriteSaveData(Slot);
        }
    }
    protected void OnMode() {
        SetMode(!m_LoadMode);
    }

    public void SetMode(bool LoadMode) {
        m_LoadMode = LoadMode;

        if (m_LoadMode) {
            m_BtMode.GetComponentInChildren<Text>().text = LOAD;
        } else {
            m_BtMode.GetComponentInChildren<Text>().text = SAVE;
        }
    }
    public override void Display() {
        int k_PageSize = m_SelButtons.Bt.Length;
        int index = m_Page * k_PageSize;
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        
        int i = index+1;
        while (i<index+k_PageSize && i <= DataService.MAX_NUMBER_OF_PROFILES) {
            string file = DataService.Instance.GetSaveDataFilePath(i);
            if (File.Exists(file)) {
                //Todo file preview (savedate, PlayerLocation,...)
                BtList.Add(new ButtonList.ButtonItem(DataService.Instance.GetSaveDataName(i), "", null, OnPressed, i.ToString())); 
            } else {
                BtList.Add(new ButtonList.ButtonItem(EMPTY_SLOT, "", null, OnPressed, i.ToString()));
            }
            
            i++;
        }
        m_SelButtons.SetBtList(BtList);
        m_Info.ShowText("", "");
        Show();
    }
}

