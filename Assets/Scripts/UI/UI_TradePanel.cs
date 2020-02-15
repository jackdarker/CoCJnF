using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// script for the TradePanel
/// Used to exchange Items between 2 Inventorys (called A and B).
/// - Buy/Sell items between Player & shopkeeper
/// - transfer items between player & chests
/// - transfer items between player & NPC
/// 
/// Be aware:
/// - only un-equipped items can be traded
/// - buying/selling requires additional handling (override ???)
/// - transfer might be oneway, f.e. player can take only from lootchest but may not put anything inside
/// - the NPC will only accept/give certain item-types/amount
/// - 
/// </summary>
public class UI_TradePanel : BasePanel {

    /// <summary>
    /// This is the text that will display when 
    /// </summary>
    private const string EMPTY_SLOT = "New Game";
    private const string USED_SLOT = "Load Save ";
    private const string LOAD = "Load...";
    private const string SAVE = "Save...";

    public Button m_BtMode;
    public Button m_BtBack;
    public ButtonList m_SelButtons;
    public Transform m_ItemsAHolder;
    public Transform m_ItemsBHolder;
    public InfoBox m_Info;
    protected int m_Page = 0;
    protected bool m_AInvCanReceive = true;
    protected bool m_BInvCanReceive = false;
    public BaseActor m_AActor;
    public BaseActor m_BActor;
    public Inventory m_AInv;
    public Inventory m_BInv;
    public SimpleObjectPool buttonObjectPool;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        m_Page = 0;
        m_BtBack.onClick.AddListener (Hide);
        m_BtMode.onClick.AddListener( OnMode);
        
    }
    protected void Setup() {
        if (m_AActor != null) {
            m_AInv= m_AActor.GetInventory();
        }
        if (m_BActor != null) {
            m_BInv = m_BActor.GetInventory();
        }
        ShowInventory(m_AInv, m_ItemsAHolder);
        ShowInventory(m_BInv, m_ItemsBHolder);
    }

    protected void ShowInventory(Inventory Inv, Transform Holder) {
        int Slots = Inv.GetSlotsUsed();
        while (Holder.childCount > 0) {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
        for (int i = 0; i < Slots; i++) {
            InventoryItem _item = Inv.GetItem(i);
            GameObject _bt = buttonObjectPool.GetObject();
            _bt.transform.SetParent(Holder);
            //Button _bt= MonoBehaviour.Instantiate<Button>(m_ItemPrefab, Holder);
            //_bt.GetComponentInChildren<Text>().text = _item.GetName() + " (" + _item.GetCount().ToString() + ")";
            ButtonWithIcon sampleButton = _bt.GetComponent<ButtonWithIcon>();
            sampleButton.ClickCallback = delegate { OnTrade(_item); };
            sampleButton.SetText(_item.GetName() + " (" + _item.GetCount().ToString() + ")");
        }
    }

    protected void OnPressed(ButtonList.ButtonItem bt) {
        int Slot = int.Parse(bt._ID);

    }
    protected void OnTrade(InventoryItem item) {
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

    
}

