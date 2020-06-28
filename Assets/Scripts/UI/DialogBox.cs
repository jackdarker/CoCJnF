using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//
public class DialogBox : MonoBehaviour {

    //connect this to your UI element where this script is added
    public Button[] Bt;
    public Text m_Header;
    public Text m_Text;
    public Image m_Panel;
    public DialogSceneData m_Data;

    public void Show() {
        m_Panel.gameObject.SetActive(true);
        m_Panel.enabled = true;
    }
    public void Display(DialogSceneData Data) {
        m_Data = Data;
        Data.Setup();
        Display();
    }
    public void Display() {
        for (int i = 0; i < Bt.Length; i++) {
            Bt[i].onClick.RemoveAllListeners();
        }
        DialogTree.DialogSetup _dlg = m_Data.GetDialog().getDialog();
        if (_dlg.m_Done) {
            Hide();
            return;
        } 
        IEnumerator<DialogTree.DlgElement> it= _dlg.m_Elements.GetEnumerator();
        while(it.MoveNext()) {
            this.Add(it.Current);
        }
        
        Show();
    }

    protected void Add(DialogTree.DlgElement data) {
        if (data is DialogTree.Say) {
            DialogTree.Say _data = (DialogTree.Say)data;
            m_Header.text = _data.m_Who;
            m_Text.text = _data.m_What;
        } else if (data is DialogTree.Choice) {
            DialogTree.Choice _data = (DialogTree.Choice)data;
            int k_PageSize = Bt.Length;
            int index = 0;
            for (int i = 0; i < k_PageSize; i++) {
                index = /*(m_Page * k_PageSize)+*/ i;
                if (index < _data.m_Choice.Length) {
                    Bt[i].GetComponentInChildren<Text>(true).text = _data.m_Text[index];
                    int fixedi = _data.m_Choice[index]; //if we use i instead, all delegates will be called with the last value i was assigned by the loop !
                    Bt[i].onClick.AddListener(delegate { onClick(fixedi); });
                    Bt[i].enabled = true;
                    Bt[i].gameObject.SetActive(true);
                } else {
                    Bt[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void Hide()
    {
        for (int i = 0; i < Bt.Length; i++) {
            Bt[i].onClick.RemoveAllListeners();
        }
        m_Panel.gameObject.SetActive(false);
        m_Panel.enabled = false;
    }
    public void onClick(int i) {
        m_Data.SetDialogResult(i);
        //if (m_BtList[i].OnPressed != null) m_BtList[i].OnPressed(m_BtList[i]);
        Display();
    }

}
