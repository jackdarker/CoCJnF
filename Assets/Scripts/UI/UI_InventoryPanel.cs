using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// script for the InventoryPanel
/// Used to manage a single Inventory
/// - use/consume Items
/// - equip/unequip items
/// - drop items
/// 
/// Be aware:
/// - only un-equipped items can be dropped
/// </summary>
public class UI_InventoryPanel : BasePanel {

    /// <summary>
    /// This is the text that will display when 
    /// </summary>
    private const string LOAD = "Load...";
    private const string SAVE = "Save...";

    public Button m_BtMode;
    public Button m_BtBack;
    public ButtonList m_SelButtons;
    public Transform m_ItemsAHolder;
    public Transform m_ItemsBHolder;
    public InfoBox m_Info;
    protected int m_Page = 0;
    protected SceneController sceneController;
    public BaseActor m_AActor;
    public Inventory m_AInv;
    public SimpleObjectPool buttonObjectPool;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        m_Page = 0;
        m_BtBack.onClick.AddListener(OnApply); 
        //m_BtMode.onClick.AddListener( OnMode);
    }
    protected void Setup() {
        sceneController = FindObjectOfType<SceneController>();
        if (m_AActor != null) {
            m_AInv= m_AActor.GetInventory();
        }
        ShowInventory(m_AInv, m_ItemsAHolder);
    }
    protected void OnApply() {
        Hide();
    }
    protected void ShowInventory(Inventory Inv, Transform Holder) {
        if (Inv == null) return; //todo??
        int Slots = Inv.GetSlotsUsed();
        while (Holder.childCount > 0) {
            GameObject toRemove = Holder.GetChild(Holder.childCount-1).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
        for (int i = 0; i < Slots; i++) {
            InventoryItem _item = Inv.GetItem(i);
            GameObject _bt = buttonObjectPool.GetObject();
            _bt.transform.SetParent(Holder);
            //Button _bt= MonoBehaviour.Instantiate<Button>(m_ItemPrefab, Holder);
            //_bt.GetComponentInChildren<Text>().text = _item.GetName() + " (" + _item.GetCount().ToString() + ")";
            ButtonWithIcon sampleButton = _bt.GetComponent<ButtonWithIcon>();
            sampleButton.ClickCallback = delegate { OnUse(_item); };
            sampleButton.SetText(_item.GetName() + " (" + _item.GetCount().ToString() + ")");
        }
    }

    protected void OnPressed(ButtonList.ButtonItem bt) {
        int Slot = int.Parse(bt._ID);
        InventoryItem Item = m_AInv.GetItem(Slot);
        this.m_Info.ShowText(Item.GetDescription(), Item.GetName());

    }
    protected void OnUse(InventoryItem item) {
        Debug.Log("OnTrade called:" + item.GetName()); //item.GetOwner().GetInventory()
    }
    protected void OnMode() {
    }

    public void SetMode(bool LoadMode) {
            m_BtMode.GetComponentInChildren<Text>().text = LOAD;
    }
    public override void Display() {
        Setup();
        //int k_PageSize = m_SelButtons.Bt.Length;
        //int index = m_Page * k_PageSize;
        //List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        
        //int i = index+1;
        //while (i<index+k_PageSize && i <= DataService.MAX_NUMBER_OF_PROFILES) {
        //    string file = DataService.Instance.GetSaveDataFilePath(i);
        //    if (File.Exists(file)) {
        //        //Todo file preview (savedate, PlayerLocation,...)
        //        BtList.Add(new ButtonList.ButtonItem(DataService.Instance.GetSaveDataName(i), "", null, OnPressed, i.ToString())); 
        //    } else {
        //        BtList.Add(new ButtonList.ButtonItem(EMPTY_SLOT, "", null, OnPressed, i.ToString()));
        //    }
            
        //    i++;
        //}
        //m_SelButtons.SetBtList(BtList);
        m_Info.ShowText("", "");
        Show();
    }
    public void Display(BaseActor Actor) {
        m_AActor = Actor;
        Display();
    }
    
}

