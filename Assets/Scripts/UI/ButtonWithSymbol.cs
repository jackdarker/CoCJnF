using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonWithSymbol : MonoBehaviour
{

    public Button buttonComponent;
    public Text nameLabel;
    public Image iconImage;
    public Text priceText;


    private Item item;
    private ButtonListBuilder scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem, ButtonListBuilder currentScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        //iconImage.sprite = item.icon;
        //priceText.text = item.price.ToString();
        scrollList = currentScrollList;

    }

    public void HandleClick()
    {
        scrollList.DoAction(item);
    }
}