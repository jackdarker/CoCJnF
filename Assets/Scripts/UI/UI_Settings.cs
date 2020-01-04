using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class UI_Settings : BasePanel {

    public Button m_BtApply;
    public Toggle m_MuteSound;
    public Slider m_BGMVolume;
    protected int m_Page = 0;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        m_Page = 0;
        m_BtApply.onClick.AddListener( OnApply);
    }

    
    protected void OnApply() {
        DataService.Instance.prefs.SetVolume(m_BGMVolume.value);
        DataService.Instance.prefs.SetMuted(m_MuteSound.isOn);
    }

    public override void Display() {
       
        m_BGMVolume.value = DataService.Instance.prefs.GetVolume();
        m_MuteSound.isOn = DataService.Instance.prefs.GetIsMuted();
        Show();
    }
}

