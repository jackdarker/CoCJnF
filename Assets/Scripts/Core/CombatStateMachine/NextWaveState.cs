﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//default state when entering battle
//
public class NextWaveState : StateMachine.State{

    CombatStatemachine m_Owner;
    public NextWaveState(CombatStatemachine Owner)
    {
        m_Owner = Owner;
    }
    public override void Enter() {
        base.Enter();
        m_Owner.m_SelButtons.SetBtList(null);
        m_Owner.m_Info.Hide();
        m_Owner.m_Message.text = "get Ready for next wave";
        List<ButtonList.ButtonItem> BtList = new List<ButtonList.ButtonItem>();
        BtList.Add(new ButtonList.ButtonItem("Go on", "", null, OnPressed, "Go"));
        BtList.Add(new ButtonList.ButtonItem("Flee", "", null, OnPressed, "Flee"));
        m_Owner.m_SelButtons.SetBtList(BtList);
        m_Owner.SpawnEnemy();
    }
    public void OnPressed(ButtonList.ButtonItem bt) {
        switch (bt._ID) {
            case "Go":
                m_Owner.ChangeState(new PreWaveState(m_Owner));
                break;
            case "Flee":
                //TODO
                break;
            default:
                break;
        }
    }
    public override void OnSubmit() {

    }
}

