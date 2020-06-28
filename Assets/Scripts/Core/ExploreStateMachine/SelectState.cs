using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Selecting actions with Buttonlist
public class SelectState : StateMachine.State {

    ExploreStatemachine m_Owner;
    /* protected virtual void Awake()
     {
         owner = GetComponent<ExploreStatemachine>();
     }*/
    public SelectState(ExploreStatemachine Owner) {
        m_Owner = Owner;
    }
    public override void Enter() {
        base.Enter();
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>() ;
        BtList.Add(new ButtonList.ButtonItem("Rest", "Rest for some time", null, OnPressed,"Rest"));
        BtList.Add(new ButtonList.ButtonItem("Inventory", "", null, OnPressed, "Inventory"));
        m_Owner.m_SelButtons.SetBtList(BtList);
    }
    public void OnPressed(ButtonList.ButtonItem bt) {
        switch (bt._ID) {
            case "Rest":
                DataController.instance.game.AddTimeToClock(7);
                break;
            default:
                break;
        }
    }
    public override void OnCancle() {
        m_Owner.ChangeState(new ExploreState(m_Owner));
    }
    public override void OnSubmit() {
    }
    protected void ShowText1(object sender, EventArgs e) {
        if (m_Owner.m_Info.isActiveAndEnabled) {
            m_Owner.m_Info.ShowText("clicked 1", "");
        }
    }
    protected void OnCancle(object sender, EventArgs e) {
        OnCancle();
    }
    protected override void AddListeners() {
        m_Owner.m_SelButtons.OnBackClicked += OnCancle;
    }

    protected override void RemoveListeners() {
        m_Owner.m_SelButtons.OnBackClicked -= OnCancle;
    }
    public override void MoveRelative(Vector3 direction, Action didFinish) {
        //m_Owner.m_Mover.AttemptMove(direction);
    }
}
