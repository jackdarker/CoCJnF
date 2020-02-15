using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ButtonWithIcon : MonoBehaviour {

    protected Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public delegate void Clicked();
    public Clicked ClickCallback = null;
    void Start() {
        buttonComponent = this.GetComponent<Button>();
        buttonComponent.onClick.AddListener(HandleClick);
    }
    public void SetIcon(Sprite newIcon) {
        iconImage.sprite = newIcon;
    }
    public void SetText(string newText) {
        nameLabel.text = newText;
    }
    protected void HandleClick() {
        ClickCallback?.Invoke();
    }
}

