using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour {

    public Text m_Header;
    public Text m_Text;
    public Image m_Panel;

    public void ShowText(string Header, string Text) {
        m_Header.text = Header;
        m_Text.text = Text;
    }
    public void Show() {
        if (!m_Panel.isActiveAndEnabled) {
            m_Panel.gameObject.SetActive(true);
            m_Panel.enabled = true;
        }
    }
    public void Hide() {
        m_Panel.gameObject.SetActive(false);
        m_Panel.enabled = false;
    }
}
