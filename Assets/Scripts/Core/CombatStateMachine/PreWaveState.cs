using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//called at start of wave
//place the combatatns on the board
public class PreWaveState : StateMachine.State {
    CombatStatemachine m_Owner;
    public PreWaveState(CombatStatemachine Owner) {
        m_Owner = Owner;
    }
    public override void Enter() {
        base.Enter();
        m_Owner.m_SelButtons.SetBtList(null);
        m_Owner.m_Info.Hide();
        m_Owner.ChangeState(new PreTurnState(m_Owner));
    }
}

