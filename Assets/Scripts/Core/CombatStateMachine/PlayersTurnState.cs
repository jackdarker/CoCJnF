using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player executes actions until he quits the turn
/// </summary>
public class PlayersTurnState : StateMachine.State {

    CombatStatemachine m_Owner;
    public PlayersTurnState(CombatStatemachine Owner) {
        m_Owner = Owner;
    }

    public override void Enter() {
        base.Enter();
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        BtList.Add(new ButtonList.ButtonItem("End Turn", "", null, OnPressed, "EndTurn"));
        BtList.Add(new ButtonList.ButtonItem("Flee", "", null, OnPressed, "Flee"));
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
                m_Owner.ChangeState(new EnemysTurnState(m_Owner));
                break;
            case "Attack":
                
                break;
            default:
                break;
        }
    }
}
