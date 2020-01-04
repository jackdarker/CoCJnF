using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Questlog : BasePanel
{
    public ButtonList m_SelButtons;
    public InfoBox m_Info;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_Page = 0;
        m_SelButtons.OnBackClicked += OnCancle;
    }

    public void OnPressed(ButtonList.ButtonItem bt) {
        Quest q = QuestManager.getSingleton().GetQuestById(int.Parse(bt._ID));
        m_Info.ShowText(q.GetName(), q.GetLogDescription());
    }

    public override void Display() {
        int k_PageSize = m_SelButtons.Bt.Length;
        int index=m_Page* k_PageSize;
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        Dictionary<int, Quest>.Enumerator it =QuestManager.getSingleton().GetIterator();
        int i = 0;
        while (it.MoveNext()) {
            if(index<=i && index + k_PageSize > i) {
                BtList.Add(new ButtonList.ButtonItem(it.Current.Value.GetName(), "", null, OnPressed, it.Current.Key.ToString()));
            }
        }
        m_SelButtons.SetBtList(BtList);
        m_Info.ShowText("", "");
        Show();
    }
    protected int m_Page = 0;
}
