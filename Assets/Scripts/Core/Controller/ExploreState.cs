using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//default state when entering overworld
//show Nav-Panel, Infopanel
public class ExploreState : StateMachine.State {

    ExploreStatemachine m_Owner;
    /* protected virtual void Awake()
     {
         owner = GetComponent<ExploreStatemachine>();
     }*/
    public ExploreState(ExploreStatemachine Owner) {
        m_Owner = Owner;
    }
    public override void Enter()
    {
        base.Enter();
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        BtList.Add(new ButtonList.ButtonItem("N", "", null, OnNav, "N"));
        BtList.Add(new ButtonList.ButtonItem("E", "", null, OnNav, "E"));
        BtList.Add(new ButtonList.ButtonItem("S", "", null, OnNav, "S"));
        BtList.Add(new ButtonList.ButtonItem("W", "", null, OnNav, "W"));
        m_Owner.m_NavButtons.SetBtList(BtList);
        m_Owner.m_SelButtons.Hide();
        m_Owner.m_Clock.Show();
    }
    public override void Exit() {
        base.Enter();
        m_Owner.m_SelButtons.Hide();
        m_Owner.m_NavButtons.Hide();    //Todo this causes flicker if switching states 
    }
    public override void OnCancle() {
        m_Owner.m_Info.Hide();
    }
    public override void OnSubmit() {
        if (false ){//TODO m_Owner.m_Mover.isPlayerSelected) {
            m_Owner.ChangeState(new SelectState(m_Owner));
        } else {
            m_Owner.m_Info.Show();
        }   
    }
    public void OnNav(ButtonList.ButtonItem bt) {
        Vector3 direction = Vector3.zero;   //x=horizontal,z=vertical
        switch (bt._ID) {
            case "N":
                direction = new Vector3(0, 0, 1);
                break;
            case "E":
                direction = new Vector3(1, 0,0);
                break;
            case "S":
                direction = new Vector3(0, 0, -1);
                break;
            case "W":
                direction = new Vector3(-1, 0, 0);
                break;
            default:
                break;
        }
        if (direction != Vector3.zero) {
            MoveRelative(direction, null);
            m_Owner.m_Clock.UpdateClock();  //Todo connect the clock to the events for automatic update
        }
    }
    protected void ShowText1(object sender, EventArgs e) {
        if (m_Owner.m_Info.isActiveAndEnabled) {
            m_Owner.m_Info.ShowText("clicked 1", "");
        }
    }
    protected void ShowText2(object sender, EventArgs e) {
        if (m_Owner.m_Info.isActiveAndEnabled) {
            m_Owner.m_Info.ShowText("clicked 2", "");
        }
    }

    public override void MoveRelative(Vector3 direction, Action didFinish) {
       // m_Owner.m_Mover.AttemptMove(direction); //Todo runs animation asynchron; wait until finished
    }
    /* protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
     {

     }*/
}
/*
public partial class FlowController : MonoBehaviour
{
    StateMachine.State ExploreState
    {
        get
        {
            if (_ExploreState == null)
                _ExploreState = new StateMachine.State(OnEnterExploreState, OnExitExploreState, "Intro");
            return _ExploreState;
        }
    }
    StateMachine.State _ExploreState;

    void OnEnterExploreState()
    {
        //??musicController.PlayIntro();
        //??introLogoViewController.gameObject.SetActive(true);
        exploreViewController.gameObject.SetActive(true);
       /* exploreViewController.didFinish = delegate {  //Todo where to go
            stateMachine.ChangeState(SetupState);
        };*/
  /*  }
  
    void OnExitExploreState()
    {
        exploreViewController.didFinish = null;
        exploreViewController.gameObject.SetActive(false);
    }
}*/