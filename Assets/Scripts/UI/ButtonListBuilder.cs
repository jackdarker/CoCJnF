﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public float price = 1;
}
public interface IListFilter {
    List<Item> GetList();
    List<Item> GetList(int page,int pagesize, out int totalcount);
}

public class ButtonListBuilder : MonoBehaviour
{

    public List<Item> itemList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    public int NumberOfButtons = 6;
    private int ActualPage=0;

    // Use this for initialization
    void Start()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            ButtonWithSymbol sampleButton = newButton.GetComponent<ButtonWithSymbol>();
            sampleButton.Setup(item, this);
        }
    }
    public void NavigateUp() {
        if (ActualPage > 0) {

        };
    }
    public void NavigateDown() {
    }
    public void ConnectToList(IListFilter Filter) {
        itemList=Filter.GetList();
        ActualPage = 0;
        RefreshDisplay();
    }
    public void DoAction(Item action)
    {

    }

   /* public void TryTransferItemToOtherShop(Item item)
    {
        if (otherShop.gold >= item.price)
        {
            gold += item.price;
            otherShop.gold -= item.price;

            AddItem(item, otherShop);
            RemoveItem(item, this);

            RefreshDisplay();
            otherShop.RefreshDisplay();
            Debug.Log("enough gold");

        }
        Debug.Log("attempted");
    }*/
/*
    void AddItem(Item itemToAdd, ShopScrollList shopList)
    {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ShopScrollList shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemList[i] == itemToRemove)
            {
                shopList.itemList.RemoveAt(i);
            }
        }
    }*/
}