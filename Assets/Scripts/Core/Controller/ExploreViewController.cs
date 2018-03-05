using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExploreViewController : BaseViewController
{

    public enum Exits
    {
        Move,
        Inventory
    }

    public Action<Exits> didFinish;

   // [SerializeField] Button loadButton;

    void OnEnable()
    {
        //loadButton.interactable = DataController.instance.HasSavedGame();
    }

    public void OnMoveButton()  // Todo 
    {
        if (didFinish != null)
            didFinish(Exits.Move);
    }

    public void OnInventoryButton()
    {
        if (didFinish != null)
            didFinish(Exits.Inventory);
    }
}
