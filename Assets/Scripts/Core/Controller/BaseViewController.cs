using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseViewController : MonoBehaviour
{
    [SerializeField] protected BaseTransition transition;
    protected Game game { get { return DataController.instance.game; } }
   /* protected Board board
    {
        get { return DataController.instance.board; }
        set { DataController.instance.board = value; }
    }*/
    protected Battle battle { get { return DataController.instance.battle; } }
    protected Player currentPlayer { get { return game.CurrentPlayer; } }

    public virtual void Show(Action didShow = null)
    {
        gameObject.SetActive(true);
        if (transition != null)
        {
            transition.Show(delegate {
                DidShow(didShow);
            });
        }
        else
        {
            DidShow(didShow);
        }
    }

    protected virtual void DidShow(Action complete)
    {
        if (complete != null)
            complete();
    }

    public virtual void Hide(Action didHide = null)
    {
        if (transition != null)
        {
            transition.Hide(delegate {
                DidHide(didHide);
            });
        }
        else
        {
            DidHide(didHide);
        }
    }

    protected virtual void DidHide(Action complete)
    {
        gameObject.SetActive(false);
        if (complete != null)
            complete();
    }
}