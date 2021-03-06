﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//
public class ButtonList : MonoBehaviour {
    public class ButtonItem {
        public ButtonItem(string text,string info, Sprite img, Action<ButtonItem> onPressed , string ID) {
            _Text = text;
                InfoText = info;
                _Image = img;
                OnPressed = onPressed;
            _ID = ID;
        }
        
        public string _Text;    // the text showed on the button
        public string InfoText; // the tooltip of the button
        public Sprite _Image;   // the image showed on the button
        public bool Disabled = false;
        public Action<ButtonItem> OnPressed;
        public string _ID;  //can be used for identification of the pressed button
    }
    private List<ButtonItem> m_BtList;
    private int m_Page;

    public EventHandler OnBackClicked; 
    //connect this to your UI element where this script is added
    public Button m_BtBack;
    public Button m_BtUp;
    public Button m_BtDown;
    public Button[] Bt;
    public Image m_Panel;

    public void Show() {
        m_Panel.gameObject.SetActive(true);
        m_Panel.enabled = true;
        if (m_BtBack != null) m_BtBack.onClick.AddListener(delegate { OnBack(); });
        if (m_BtUp != null) m_BtUp.onClick.AddListener(delegate { OnUp(); });
        if (m_BtDown != null) m_BtDown.onClick.AddListener(delegate { OnDown(); });
        for (int i =0; i< Bt.Length;i++) {
            Bt[i].onClick.RemoveAllListeners();
            int fixedi = i; //if we use i instead, all delegates will be called with the last value i was assigned by the loop !
            Bt[i].onClick.AddListener(delegate { onClick(fixedi); });
        }
    }
    public void SetBtList(List<ButtonItem> list) {
        if (list != null) {
            m_BtList = list;
            m_Page = 0;
            Display();
        } else {
            Hide();
        }
    }
    public void Display() {
        int k_PageSize = Bt.Length;
        int index=0;
        for (int i = 0; i < k_PageSize; i++) {
            index = (m_Page * k_PageSize) + i;
            if (index< m_BtList.Count) {
                Bt[i].GetComponentInChildren<Text>(true).text = m_BtList[index]._Text;
                if (m_BtList[index]._Image!=null)
                    Bt[i].GetComponentInChildren<Image>(true).sprite= m_BtList[index]._Image;
                Bt[i].enabled = true;
                Bt[i].gameObject.SetActive(true);
            } else {
                Bt[i].gameObject.SetActive( false);
            }
        }
        if (m_BtUp != null) m_BtUp.enabled = (m_Page>0);
        if (m_BtDown != null) m_BtDown.enabled = (m_BtList.Count >= index);
        Show();
    }
    public void Hide()
    {
        if (m_BtBack != null) m_BtBack.onClick.RemoveAllListeners();
        if (m_BtUp != null) m_BtUp.onClick.RemoveAllListeners();
        if (m_BtDown != null) m_BtDown.onClick.RemoveAllListeners();
        for (int i = 0; i < Bt.Length; i++) {
            Bt[i].onClick.RemoveAllListeners();
        }
        m_Panel.gameObject.SetActive(false);
        m_Panel.enabled = false;
    }
    public void onClick(int i) {
        int index = m_Page * Bt.Length + i;
        if (m_BtList[index].OnPressed != null) m_BtList[index].OnPressed(m_BtList[index]);
    }
    public void OnDown() {
        m_Page++;
        Display();
    }
    public void OnUp() {
        m_Page--;
        Display();
    }
    public void OnBack() {
        if (OnBackClicked != null) OnBackClicked(this, EventArgs.Empty);
    }
}
