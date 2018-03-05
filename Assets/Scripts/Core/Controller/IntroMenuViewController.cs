using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IntroMenuViewController : BaseViewController
{

    public enum Exits
    {
        New,
        Load
    }

    public Action<Exits> didFinish;

    [SerializeField] Button loadButton;

    void OnEnable()
    {
        loadButton.interactable = DataController.instance.HasSavedGame();
    }

    public void OnCreateButton()
    {
        if (didFinish != null)
            didFinish(Exits.New);
    }

    public void OnLoadButton()
    {
        if (didFinish != null)
            didFinish(Exits.Load);
    }
}
