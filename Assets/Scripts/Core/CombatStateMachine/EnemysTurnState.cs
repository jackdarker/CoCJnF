using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Enemy-AI executes actions and then ends the turn
/// </summary>
public class EnemysTurnState : StateMachine.State {

    CombatStatemachine m_Owner;
    public EnemysTurnState(CombatStatemachine Owner) {
        m_Owner = Owner;
        OnFinish = delegate {
            //TODO stateMachine.ChangeState(PlayerCountState);
        };
    }

    public override void Enter() {
        base.Enter();
        m_Owner.m_Message.text = "its enemys turn";
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        BtList.Add(new ButtonList.ButtonItem("End Turn", "", null, OnPressed, "EndTurn"));
        BtList.Add(new ButtonList.ButtonItem("Attack", "", null, OnPressed, "Attack"));
        m_Owner.m_SelButtons.SetBtList(BtList);
    }
    public override void Exit() {
        base.Exit();
        m_Owner.m_SelButtons.Hide();
    }
    public void OnPressed(ButtonList.ButtonItem bt) {
        switch (bt._ID) {
            case "EndTurn":
                m_Owner.ChangeState(new PlayersTurnState(m_Owner));
                break;
            case "Attack":
                m_Owner.m_Info.m_Text.text = "enemys attack failed";
                m_Owner.m_Info.enabled = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CalculateNextMove() {
    }
    private void Command(String CommandString) {
        switch (CommandString) {
            case "SkipTurn":
                
                break;
            default:
                break;
        }
        DoCommand(MyCommand, OnFinish);
    }
    //
    private Action OnFinish;
    private Action MyCommand;
    protected IEnumerator DoCommand(Action Command, Action OnFinish) {
        Command();
        yield return new WaitForSeconds(1f);
        if (OnFinish != null) OnFinish();
    }
}
