using UnityEngine;
using UnityEngine.UI;

//updates the clock in the UI
public class Clock : MonoBehaviour {
    public Text m_Clock;
    public Text m_Days;
    public Image m_Panel;

    public void Show() {
        if (!m_Panel.isActiveAndEnabled) {
            m_Panel.gameObject.SetActive(true);
            m_Panel.enabled = true;
        }
        UpdateClock();
    }
    public void UpdateClock() {
        m_Clock.text = DataController.instance.game.GetTimeAsString();
        m_Days.text = DataController.instance.game.GetDaysAsString();
    }
    public void Hide() {
        m_Panel.gameObject.SetActive(false);
        m_Panel.enabled = false;
    }
}
